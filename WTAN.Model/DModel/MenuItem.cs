using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace WTAN.Model.DModel
{
    [Serializable]
    public class MenuItem
    {
        [XmlAttribute]
        public String MenuName
        {
            get;
            set;
        }

        [XmlAttribute]
        public String IcoName
        {
            get;
            set;
        }

        #region property MenuID
        [XmlAttribute]
        public int ID
        {
            get;
            set;
        }
        #endregion

        #region property ChildItems
        public List<ChildItem> ChildItems
        {
            get;
            set;
        }
        #endregion
    }
}
