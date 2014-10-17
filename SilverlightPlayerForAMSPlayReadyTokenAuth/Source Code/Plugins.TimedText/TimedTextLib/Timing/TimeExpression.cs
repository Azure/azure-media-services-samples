// <copyright file="TimeExpression.cs" company="Microsoft Corporation">
// ===============================================================================
// MICROSOFT CONFIDENTIAL
// Microsoft Accessibility Business Unite
// Incubation Lab
// Project: Timed Text Library
// ===============================================================================
// Copyright 2009  Microsoft Corporation.  All rights reserved.
// THIS CODE AND INFORMATION IS PROVIDED "AS IS" WITHOUT WARRANTY
// OF ANY KIND, EITHER EXPRESSED OR IMPLIED, INCLUDING BUT NOT
// LIMITED TO THE IMPLIED WARRANTIES OF MERCHANTABILITY AND
// FITNESS FOR A PARTICULAR PURPOSE.
// ===============================================================================
// </copyright>

using System;
using System.Text.RegularExpressions;
using System.Globalization;
using Microsoft.SilverlightMediaFramework.Core.Media;

/*
 * 
 * Syntax Representation – <timeExpression>
 
        <timeExpression>
          : clock-time
          | offset-time
        clock-time
          : hours ":" minutes ":" seconds ( fraction | ":" frames ( "." sub-frames )? )?
       offset-time
          : time-count fraction? metric
        hours
          : <digit> <digit>
          | <digit> <digit> <digit>+
        minutes | seconds
          : <digit> <digit>
        frames
          : <digit> <digit>
          | <digit> <digit> <digit>+
        sub-frames
          : <digit>+
        fraction
          : "." <digit>+
        time-count
          : <digit>+
        metric
          : "h"                 // hours
          | "m"                 // minutes
          | "s"                 // seconds
          | "ms"                // milliseconds
          | "f"                 // frames
          | "t"                 // ticks 
*/
namespace TimedText.Timing
{
    /// <summary>
    /// Timed text timebase to use
    /// </summary>
    public enum TimeBase
    {
        Media,
        Smpte,
        Clock,
    }

    /// <summary>
    /// Timed text clock mode to use
    /// </summary>
    public enum ClockMode
    {
        Local,
        Gps,
        Utc,
    }

    /// <summary>
    /// Which flavour of Smpte timecode to use
    /// </summary>
    public enum SmpteMode
    {
        DropNtsc,
        DropPal,
        NonDrop,
    }

    /// <summary>
    /// Units that time is measured in
    /// </summary>
    enum Metric
    {
        Hours,
        Minutes,
        Seconds,
        Milliseconds,
        Frames,
        Ticks
    };

    /// <summary>
    /// Represents a timed text time expression
    /// </summary>
    public sealed class TimeExpression
    {

        #region Variables and Properties

        //clock-time
        //   : hours ":" minutes ":" seconds ( fraction | ":" frames ( "." sub-frames )? )?
        private const string clocktime = @"^(?<hrs>\d\d+):(?<mins>\d\d):(?<secs>\d\d)((?<subsecs>\.\d+)|:(?<frames>\d\d+(?<subframes>\.\d+)?))?$";

        //offset-time
        //   : time-count fraction? metric
        private const string offsettime = @"^(?<value>\d+(\.\d+)?)(?<units>ms|[hmsft])$";

        static Regex clockTimeRegex = new Regex(clocktime);
        static Regex offsetTimeRegex = new Regex(offsettime);
        /// <summary>
        /// Global clock mode for the current parse session
        /// </summary>
        public static ClockMode CurrentClockMode
        {
            get;
            set;
        }

        /// <summary>
        /// Global time base for the current parse session.
        /// </summary>
        /// 

        static TimeBase _currentTimeBase;
        public static TimeBase CurrentTimeBase
        {
            get { return _currentTimeBase; }
            set
            {
                _currentTimeBase = value;
            }
        }

        /// <summary>
        /// Global smpte mode for the current parse session.
        /// </summary>
        public static SmpteMode CurrentSmpteMode
        {
            get;
            set;
        }

        /// <summary>
        /// Global framerate for the current parse session
        /// </summary>
        public static int CurrentFrameRate
        {
            get;
            set;
        }

        /// <summary>
        /// Global sub frame rate for the current parse session
        /// </summary>
        public static int CurrentSubFrameRate
        {
            get;
            set;
        }

        /// <summary>
        /// Global frame rate nominator for the current parse session
        /// </summary>
        public static int CurrentFrameRateNominator
        {
            get;
            set;
        }

        /// <summary>
        /// Global frame rate denominator for the current parse session
        /// </summary>
        public static int CurrentFrameRateDenominator
        {
            get;
            set;
        }

        /// <summary>
        /// Global Smpte timecode to use for the current parse session
        /// </summary>
        public static SmpteFrameRate CurrentSmpteFrameRate
        {
            get
            {
                if (CurrentFrameRateDenominator == 0) CurrentFrameRateDenominator = 1;
                decimal framerate = (decimal)CurrentFrameRate * ((decimal)CurrentFrameRateNominator / (decimal)CurrentFrameRateDenominator);
                TimeCode.ParseFramerate((double)framerate);
                switch (CurrentTimeBase)
                {
                    case TimeBase.Smpte:
                        switch (CurrentSmpteMode)
                        {
                            case SmpteMode.DropNtsc:
                            case SmpteMode.DropPal:
                                return SmpteFrameRate.Smpte2997Drop;
                            case SmpteMode.NonDrop:
                                return TimeCode.ParseFramerate((double)framerate);
                        }
                        break;
                    case TimeBase.Media:
                        {
                            return SmpteFrameRate.Smpte30;
                        }
                    case TimeBase.Clock:
                        {
                            return SmpteFrameRate.Unknown;
                        }
                }
                return SmpteFrameRate.Unknown;
            }
        }

        /// <summary>
        /// Global tick rate for the current parse session
        /// </summary>
        public static int CurrentTickRate
        {
            get;
            set;
        }
        #endregion

        #region Time Expression Semantics
        /// <summary>
        /// Private constructor - create these using the Parse
        /// static function.
        /// </summary>
        private TimeExpression()
        {
        }

        /// <summary>
        /// Initialize global parameters for the next parse session
        /// </summary>
        public static void InitializeParameters()
        {
            TimeExpression.CurrentTimeBase = TimeBase.Media;
            TimeExpression.CurrentTickRate = 1;
            TimeExpression.CurrentSubFrameRate = 1;
            TimeExpression.CurrentSmpteMode = SmpteMode.NonDrop;
            TimeExpression.CurrentFrameRate = 30;
            TimeExpression.CurrentFrameRateDenominator = 1;
            TimeExpression.CurrentFrameRateNominator = 1;
            TimeExpression.CurrentClockMode = ClockMode.Local;
        }

        /// <summary>
        /// Convert a timed text time expression into a TimeCode. 
        /// Where the current global framerate and tickrate apply
        /// </summary>
        /// <param name="timeExpression">Time expression</param>
        /// <returns>TimeSpan duration equal to time expression (ignored)</returns>
        public static TimeCode Parse(string timeExpression)
        {
            int hours = 0,
                minutes = 0,
                seconds = 0,
                frames = 0;
            double
                // subframes = 0,
                subseconds = 0;

            string input = timeExpression.Trim();
            decimal framerate = (decimal)CurrentFrameRate * ((decimal)CurrentFrameRateNominator / (decimal)CurrentFrameRateDenominator);
            // Validate <timeexpression> before attempting to extract values 
            // (allows us to cut corners later on).
            bool validTimeExpression = clockTimeRegex.IsMatch(input);
            validTimeExpression |= offsetTimeRegex.IsMatch(input);
            if (!validTimeExpression)
            {
                throw new TimedTextException("Invalid time expression " + timeExpression);
            }

            if (input.Contains(":"))
            {
                // its a clock-time
                Match m = clockTimeRegex.Match(input);

                hours = Int32.Parse(m.Groups["hrs"].Value, CultureInfo.InvariantCulture);
                minutes = Int32.Parse(m.Groups["mins"].Value, CultureInfo.InvariantCulture);
                seconds = Int32.Parse(m.Groups["secs"].Value, CultureInfo.InvariantCulture);
                if (!string.IsNullOrEmpty(m.Groups["subsecs"].Value))
                {
                    subseconds = Double.Parse("0" + (m.Groups["subsecs"].Value), CultureInfo.InvariantCulture);
                    frames = (int)Math.Floor((double)framerate * subseconds);
                }
                if (!string.IsNullOrEmpty(m.Groups["frames"].Value))
                {
                    frames = (int)Math.Floor(Double.Parse(m.Groups["frames"].Value, CultureInfo.InvariantCulture));
                    // ignore subframes for now.
                    //if (m.Groups["subframes"].Value != "")
                    //{
                    //    subframes = Double.Parse("0" + (m.Groups["subframes"].Value), CultureInfo.InvariantCulture);
                    //}
                }
                if (CurrentSmpteFrameRate != SmpteFrameRate.Unknown)
                {
                    return new TimeCode(hours, minutes, seconds, frames, CurrentSmpteFrameRate);
                }
                else
                {
                    return new TimeCode(hours, minutes, seconds, frames, framerate);
                }
            }
            else
            {
                // its a clock-time
                Match m = offsetTimeRegex.Match(input);
                // work out hours etc..
                double multiplier = GetMetricMultiplier(m.Groups["units"].Value, (double)framerate, CurrentTickRate) * 10000;
                double value = Double.Parse(m.Groups["value"].Value, CultureInfo.InvariantCulture);

                // A tick is 100 nanosec (10^-7) of a second.
                // A millisecond is 1000 of a sec (10^-3)
                // A millisecond is 10000 ticks.
                long ms = (long)Math.Floor(value * multiplier);
                //ms += (long)Math.Floor(subdigits * multiplier);
                //var newtime = new TimeSpan(ms);
                var newtime = new TimeCode(new TimeSpan(ms), framerate);
                return newtime;
            }
        }

        /// <summary>
        /// returns the number needed to convert a time expression to milliseconds
        /// if the time expression is defined in times of a time metric
        /// </summary>
        /// <param name="timeexpression"></param>
        /// <param name="framerate"></param>
        /// <param name="tickrate"></param>
        /// <returns></returns>
        private static double GetMetricMultiplier(string timeexpression, double framerate, double tickrate)
        {
            if (timeexpression.Contains("h"))
                return 1000 * 60 * 60;
            if (timeexpression.Contains("ms"))
                return 1;
            if (timeexpression.Contains("m"))
                return 1000 * 60;
            if (timeexpression.Contains("s"))
                return 1000;
            if (timeexpression.Contains("f"))
                return 1000 / framerate;
            if (timeexpression.Contains("t"))
                return 1000 / tickrate;
            return 0;
        }

        #endregion

        #region Testing
        /// <summary>
        /// Test the time parser. Not comprehensive at this point
        /// </summary>
        /// <returns></returns>
        public static bool UnitTests()
        {
            // reference is a 60 second timespan.
            // parse applies a default tickrate and framerate of 30 unless
            // otherwise specified.
            TimeSpan reference = new TimeSpan(0, 1, 00);
            bool pass = true;

            //pass &= Parse("60s").Ticks == reference.Ticks;
            //pass &= Parse("1m").Ticks == reference.Ticks;
            //pass &= Parse("60000ms").Ticks == reference.Ticks;
            //pass &= Parse("1800f").Ticks == reference.Ticks;
            //pass &= Parse("600f",10).Ticks == reference.Ticks;
            //pass &= Parse("30t",0,0.5).Ticks == reference.Ticks;
            //pass &= Parse("00:01:00").Ticks == reference.Ticks;
            //pass &= Parse("00:00:60:00").Ticks == reference.Ticks;
            //pass &= Parse("00:00:59:25",25).Ticks == reference.Ticks;

            //reference = new TimeSpan(1, 0, 0);
            //pass &= Parse("3600s", 0, 0.5).Ticks == reference.Ticks;
            //pass &= Parse("01:00:00").Ticks == reference.Ticks;
            //pass &= Parse("00:59:60:00").Ticks == reference.Ticks;
            //pass &= Parse("00:59:59:25", 25).Ticks == reference.Ticks;

            return pass;
        }
        #endregion

        #region Attic
        /// <summary>
        /// Convert a timed text time expression into a TimeSpan. Where the
        /// given framerate and tickrate apply
        /// </summary>
        /// <param name="s">time expression</param>
        /// <returns>TimeSpan duration equal to time expression</returns>
        // public static TimeSpan Parse(string s)
        //{
        //    return Parse(s, 30);
        //}

        /// <summary>
        /// Convert a timed text time expression into a TimeSpan. Where the
        /// given framerate and tickrate apply
        /// </summary>
        /// <param name="s">time expression</param>
        /// <param name="framerate">applicable framerate</param>
        /// <returns>TimeSpan duration equal to time expression</returns>
        // public static TimeSpan Parse(string s, double framerate)
        //{
        //    return Parse(s, framerate, framerate, SmpteMode.NonDrop);
        //}

        /// <summary>
        /// Convert a timed text time expression into a TimeSpan. Where the
        /// given framerate and tickrate apply
        /// </summary>
        /// <param name="s">time expression</param>
        /// <param name="framerate">applicable framerate</param>
        /// <returns>TimeSpan duration equal to time expression</returns>
        //public static TimeSpan Parse(string s, double framerate, double tickrate)
        //{
        //    return Parse(s, framerate, tickrate, SmpteMode.NonDrop);
        //}

        //  private const string clocktime = @"^\d\d+:\d\d:\d\d(\.\d+|:\d\d+(\.\d+)?)?$";

        #endregion

    }
}
