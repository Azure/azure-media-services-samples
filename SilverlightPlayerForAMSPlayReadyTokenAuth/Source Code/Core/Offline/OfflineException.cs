using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace Microsoft.SilverlightMediaFramework.Core.Offline
{
    public class OfflineException : Exception
    {
        public OfflineException(string message)
            : base(message)
        {
        }

        public OfflineException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
