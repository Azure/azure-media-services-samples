using System.Xml;
using System.Collections.Generic;

namespace System.Xml
{
    internal static class XmlReaderExtensions
    {

        public static bool GoToElement(this XmlReader reader)
        {
            do
            {
                if (reader.NodeType == XmlNodeType.Element)
                    return true;
            } while (reader.Read());
            return false;
        }

        public static bool GoToElement(this XmlReader reader, string ElementName)
        {
            do
            {
                if (reader.NodeType == XmlNodeType.Element && reader.Name == ElementName)
                    return true;
            } while (reader.Read());
            return false;
        }
        
        public static bool GoToSibling(this XmlReader reader)
        {
            do
            {
                if (reader.NodeType == XmlNodeType.Element)
                    return true;
                else if (reader.NodeType == XmlNodeType.EndElement)
                    return false;
            } while (reader.Read());
            return false;
        }

        public static bool GoToSibling(this XmlReader reader, string ElementName)
        {
            do
            {
                switch (reader.NodeType)
                {
                    case XmlNodeType.Element:
                        if (reader.Name == ElementName)
                            return true;
                        else if (reader.IsStartElement())
                            reader.Skip();
                        else
                            if (!reader.Read()) return false;
                        break;
                    case XmlNodeType.EndElement:
                        return false;
                    default:
                        if (!reader.Read()) return false;
                        break;
                }
            } while (true);
        }
    }
}
