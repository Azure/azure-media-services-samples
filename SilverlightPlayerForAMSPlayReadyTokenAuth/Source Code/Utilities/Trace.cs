using System;
using System.Diagnostics;
using System.Linq;

namespace Microsoft.SilverlightMediaFramework.Utilities
{
    public static class Trace
    {
        [Conditional("DEBUG")]
        public static void WriteLine(string line)
        {
            Console.WriteLine(line);
            Debug.WriteLine(line);
        }

        [Conditional("DEBUG")]
        public static void WriteLine(string format, params object[] args)
        {
            WriteLine(string.Format(format, args));
        }

        [Conditional("DEBUG")]
        public static void WriteObject(object obj)
        {
            var props = obj
                .GetType()
                .GetProperties()
                .OrderBy(p => p.Name);

            WriteLine("[" + obj.GetType().Name + "]:");

            foreach (var prop in props)
            {
                WriteLine("{0}: {1}{2}",
                    prop.Name,
                    prop.GetValue(obj, null));
            }

            WriteLine(string.Empty);
        }
    }
}
