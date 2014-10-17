using System;
using System.Globalization;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Microsoft.SilverlightMediaFramework.Utilities.Offline
{
    public static class OfflineExtensions
    {
        public const string IsolatedStorageScheme = "is";
        public const string IsolatedStorageUriPrefix = IsolatedStorageScheme + "://";

        public static string GetIsolatedStorageFilename(this Uri offlineUri)
        {
            string filename = offlineUri.ToString().Replace(IsolatedStorageUriPrefix, string.Empty);

            if (filename.Last() == '/')
            {
                filename = filename.Substring(0, filename.Length - 1);
            }

            return filename;
        }

        public static string ComputeOfflineFilename(this Uri onlineResourceLocation)
        {
            string strUrl = onlineResourceLocation.ToString();
            var hash = new SHA1Managed();
            byte[] rgb = hash.ComputeHash(Encoding.Unicode.GetBytes(strUrl));
            var sbEncode = new StringBuilder();

            var reversedUrl = new string(strUrl.Reverse().ToArray());
            sbEncode.AppendFormat(CultureInfo.InvariantCulture, "{0:X8}{1:X8}-", strUrl.GetHashCode(), reversedUrl.GetHashCode());
            foreach (byte by in rgb)
            {
                sbEncode.AppendFormat(CultureInfo.InvariantCulture, "{0:X2}", by);
            }
            return sbEncode.ToString();
        }
    }
}
