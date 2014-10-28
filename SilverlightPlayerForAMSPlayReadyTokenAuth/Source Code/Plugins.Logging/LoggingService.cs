using System;
using System.Collections.Generic;
using Microsoft.SilverlightMediaFramework.Logging.Logs;
using Microsoft.SilverlightMediaFramework.Logging.Data;
using System.Linq;
#if !PROGRAMMATICCOMPOSITION
using System.ComponentModel.Composition;
#endif

namespace Microsoft.SilverlightMediaFramework.Logging
{
    /// <summary>
    /// The main logging gateway. All logs are passed to this module, where they are then filtered and distributed to subsequent logging agents.
    /// </summary>
    public class LoggingService
    {
        /// <summary>
        /// Enables or disables the logging service.
        /// </summary>
        public bool IsEnabled { get; set; }

        /// <summary>
        /// Indicates he session has started.
        /// </summary>
        public event EventHandler StartingSession;

        /// <summary>
        /// Fired whenever a new log is received.
        /// </summary>
        public event EventHandler<LogReceivedEventArgs> LogReceived;

        LoggingConfig configuration;
        /// <summary>
        /// Configuration required for the logging service to operate.
        /// </summary>
        public LoggingConfig Configuration
        {
            get { return configuration; }
            set
            {
                configuration = value;
#if SILVERLIGHT && !OOB
            try
            {
                if (Configuration.QueryStringParam != null)
                {
                    if (System.Windows.Browser.HtmlPage.Document.QueryString.ContainsKey(Configuration.QueryStringParam))
                    {
                        StartupParam = System.Windows.Browser.HtmlPage.Document.QueryString[Configuration.QueryStringParam];
                    }
                }
            }
            catch { /* ignore */ }
#endif
            }
        }

        /// <summary>
        /// Contains a value that will automatically be added to each log. The value is taken from the querystring.
        /// </summary>
        public string StartupParam { get; set; }

        static LoggingService current;
        /// <summary>
        /// The current instance of the logging service. This is the only way to create and access the logging service to ensure only one exists.
        /// </summary>
        public static LoggingService Current
        {
            get
            {
                if (current == null)
                    current = new LoggingService();
                return current;
            }
        }

#if SILVERLIGHT
        private LoggingService()
#else
        public LoggingService()
#endif
        {
            IsEnabled = true;
#if !PROGRAMMATICCOMPOSITION
            CompositionInitializer.SatisfyImports(this);
#else
            Agents = Enumerable.Empty<ILogAgent>();
#endif

            // start the session on the next spot in the dispatcher queue so we can trap the SessionStarted event.
#if SILVERLIGHT
            System.Windows.Deployment.Current.Dispatcher.BeginInvoke(() => StartSession());
#else
            System.Windows.Threading.Dispatcher.CurrentDispatcher.BeginInvoke(new Action(() => StartSession()));
#endif
        }

        /// <summary>
        /// The collection of logging agents that are responsible for actually handling the individual logs.
        /// </summary>
#if !PROGRAMMATICCOMPOSITION
        [ImportMany]
#endif
        public IEnumerable<ILogAgent> Agents { get; set; }

        /// <summary>
        /// Logs a log object by sending it onto the appropriate log agents. 
        /// Each agent can specify filtering rules to help determine if it is interested in the specified log. 
        /// </summary>
        /// <param name="log"></param>
        public void Log(Log log)
        {
            if (IsEnabled)
            {
                if (!isSessionStarted)
                {
                    if (StartSession())
                        throw new Exception("The logging service was unable to start");
                    Log(log);
                }
                else
                {
                    // always add the startup param to each log
                    if (!string.IsNullOrEmpty(StartupParam))
                        log.StartupParam = StartupParam;

                    foreach (ILogAgent agent in Agents)
                    {
                        ILogFilter filter = agent.Filter;
                        if (filter != null)
                        {
                            if (filter.IncludeLog(log))
                                agent.Log(log);
                            else { /* dropping log */ }
                        }
                        else
                            agent.Log(log);
                    }

                    if (LogReceived != null)
                        LogReceived(this, new LogReceivedEventArgs(log));
                }
            }
        }

        /// <summary>
        /// Flag indicating that the logging session has started.
        /// </summary>
        public bool IsSessionStarted
        {
            get
            {
                return isSessionStarted;
            }
        }

        bool isSessionStarted;
        /// <summary>
        /// Method used to start the logging instance. This will get called automatically when the logging service is created and only needs to be called again if the session was stopped.
        /// </summary>
        /// <returns>Indicates success</returns>
        public bool StartSession()
        {
            if (!isSessionStarted && IsEnabled)
            {
                if (StartingSession != null)
                    StartingSession(this, EventArgs.Empty);

                isSessionStarted = true;
                bool result = true;
                foreach (ILogAgent agent in Agents)
                    result = result && agent.StartSession();

#if SILVERLIGHT
                // catch all UEs
                System.Windows.Application.Current.UnhandledException += Current_UnhandledException;
#endif

                // Create ApplicationStartLog
                SessionStartLog log = new SessionStartLog();
                Log(log);

                return result;
            }
            else
                return false;
        }

        /// <summary>
        /// Stops the logging session.
        /// </summary>
        public void StopSession()
        {
            isSessionStarted = false;

            foreach (ILogAgent agent in Agents)
                agent.StopSession();

#if SILVERLIGHT
            // catch all UEs
            System.Windows.Application.Current.UnhandledException -= Current_UnhandledException;
#endif
        }

        /// <summary>
        /// Broadcasts an exception. This is passed onto each log agent and but is not necessarily turned into a log object.
        /// </summary>
        public void BroadcastException(Exception exception)
        {
            foreach (ILogAgent agent in Agents)
                agent.BroadcastException(exception);
        }

        /// <summary>
        /// Logs an exception. If UEs are to be logged, this will automatically be called internally.
        /// </summary>
        /// <param name="exception">The exception object to be logged</param>
        /// <param name="ApplicationArea">The area of the application that the exception occurred in.</param>
        public void LogException(Exception exception, string ApplicationArea)
        {
            // log UEs
            ApplicationErrorLog log = new ApplicationErrorLog(ApplicationArea, Configuration.MaxExceptionLength);
            log.Message = exception.ToString();
            Log(log);
        }

#if SILVERLIGHT
        void Current_UnhandledException(object sender, System.Windows.ApplicationUnhandledExceptionEventArgs e)
        {
            if (Configuration != null)
            {
                if (Configuration.LogUnhandledExceptions)
                {
                    // log UEs
                    LogException(e.ExceptionObject, "UnhandledException");
                }
                e.Handled = e.Handled || Configuration.PreventUnhandledExceptions;
            }
        }
#endif
    }
}
