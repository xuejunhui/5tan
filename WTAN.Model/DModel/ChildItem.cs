using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTAN.Model.DModel
{
    [Serializable]
    public class ChildItem
    {
        public String IcoName { get; set; }

        #region property ID
        public int ID
        {
            get;
            set;
        }
        #endregion

        #region property FatherID
        public int FatherID
        {
            get;
            set;
        }
        #endregion

        #region property ActionName
        public String ActionName
        {
            get;
            set;
        }
        #endregion

        #region property ControllerName
        public String ControllerName
        {
            get;
            set;
        }
        #endregion

        #region property MenuName
        public String MenuName
        {
            get;
            set;
        }
        #endregion

        /// <summary>
        /// 对应资料库中分类的ID 多个用，分割
        /// </summary>
        public String CategoryIDS
        {
            get;
            set;
        }

        #region property Url
        public String Url
        {
            get;
            set;
        }
        #endregion

        #region property Visible
        public int Visible
        {
            get;
            set;
        }
        #endregion
    }
}
