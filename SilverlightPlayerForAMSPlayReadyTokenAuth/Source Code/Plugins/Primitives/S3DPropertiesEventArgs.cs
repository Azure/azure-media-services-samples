using System;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives
{
    public class S3DPropertiesEventArgs<S3DProperties> : EventArgs
    {
		private readonly S3DProperties m_value;

		public S3DPropertiesEventArgs(S3DProperties value)
        {
            m_value = value;
        }

		public S3DProperties Value
        {
            get { return m_value; }
        }
    }
}
