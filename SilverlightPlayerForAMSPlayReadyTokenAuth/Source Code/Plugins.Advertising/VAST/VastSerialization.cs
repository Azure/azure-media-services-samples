using Microsoft.SilverlightMediaFramework.Plugins.Advertising.Helpers;

namespace Microsoft.SilverlightMediaFramework.Plugins.Advertising.VAST
{
    public partial class VAST
    {
        private static System.Xml.Serialization.XmlSerializer serializer;
        private static System.Xml.Serialization.XmlSerializer Serializer
        {
            get
            {
                if ((serializer == null))
                {
                    serializer = new System.Xml.Serialization.XmlSerializer(typeof(VAST));
                }
                return serializer;
            }
        }
        
        /// <summary>
        /// Deserializes workflow markup into an VAST object
        /// </summary>
        /// <param name="xml">string workflow markup to deserialize</param>
        /// <param name="obj">Output VAST object</param>
        /// <param name="exception">output Exception value if deserialize failed</param>
        /// <returns>true if this XmlSerializer can deserialize the object; otherwise, false</returns>
        public static bool Deserialize(string xml, out VAST obj, out System.Exception exception)
        {
            exception = null;
            obj = default(VAST);
            try
            {
                obj = Deserialize(xml);
                return true;
            }
            catch (System.Exception ex)
            {
                exception = ex;
                return false;
            }
        }

        public static bool Deserialize(string xml, out VAST obj)
        {
            System.Exception exception = null;
            return Deserialize(xml, out obj, out exception);
        }

        public static VAST Deserialize(string xml)
        {
            //for now hack out the extensions - we aren't using them, and the parser doesn't like them.
            xml = xml.SectionsReplace("<Extensions>", "</Extensions>", "");

            System.IO.StringReader stringReader = null;
            try
            {
                stringReader = new System.IO.StringReader(xml);
                return ((VAST)(Serializer.Deserialize(System.Xml.XmlReader.Create(stringReader))));
            }
            finally
            {
                if ((stringReader != null))
                {
                    stringReader.Dispose();
                }
            }
        }
    }
}