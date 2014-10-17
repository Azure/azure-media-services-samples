using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;

namespace TimedText.Informatics
{
    public class MetadataInformation
    {
        public string Role
        {
            get;
            set;
        }

        public string Agent
        {
            get;
            set;
        }

        public string NameType
        {
            get;
            set;
        }

        public string Name
        {
            get;
            set;
        }

        Collection<string> m_Actors = new Collection<string>();
        public Collection<string> Actor
        {
            get { return m_Actors; }
        }
    }
}
