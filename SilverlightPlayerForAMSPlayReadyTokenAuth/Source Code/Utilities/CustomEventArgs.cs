using System;

namespace Microsoft.SilverlightMediaFramework.Core
{
    public class CustomEventArgs<T> : EventArgs
    {
        private readonly T m_value;

        public CustomEventArgs(T value)
        {
            m_value = value;
        }

        public T Value
        {
            get { return m_value; }
        }
    }


}
