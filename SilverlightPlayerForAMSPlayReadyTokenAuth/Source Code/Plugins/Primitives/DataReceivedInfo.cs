using System;
using System.Collections.Generic;

namespace Microsoft.SilverlightMediaFramework.Plugins.Primitives
{
    public class DataReceivedInfo : EventArgs
    {
        public DataReceivedInfo(byte[] Data, IDataChunk DataChunk, IDictionary<string, string> StreamAttributes) 
        {
            this.Data = Data;
            this.DataChunk = DataChunk;
            this.StreamAttributes = StreamAttributes;
        }

        public byte[] Data { get; private set; }
        public IDataChunk DataChunk { get; private set; }
        public IDictionary<string, string> StreamAttributes { get; private set; }
    }
}
