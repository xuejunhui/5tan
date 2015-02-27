using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WTAN.BLL
{
    public class CurrentBLL
    {
        private static ContentBLL _CBLL = null;
        /// <summary>
        /// Content BLL
        /// </summary>
        public static ContentBLL CBLL
        {
            get {
                if (_CBLL == null)
                    _CBLL = new ContentBLL();
                return _CBLL;
            }
        }

        private static CategoryBLL _CAbll = null;
        public static CategoryBLL CAbll
        {
            get
            {
                if (_CAbll == null)
                    _CAbll = new CategoryBLL();
                return _CAbll;
            }
        }

        private static GuideBLL _GBLL = null;
        public static GuideBLL GBLL
        {
            get {
                if (_GBLL == null)
                    _GBLL = new GuideBLL();
                return _GBLL;
            }
        }

        private static ConfigBLL _ConfigBLL = null;
        public static ConfigBLL Config
        {
            get
            {
                if (_ConfigBLL == null)
                    _ConfigBLL = new ConfigBLL();
                return _ConfigBLL;
            }
        }

        private static FriendLinkBLL _FBLL = null;
        public static FriendLinkBLL FBLL
        {
            get
            {
                if (_FBLL == null)
                    _FBLL = new FriendLinkBLL();
                return _FBLL;
            }
        }
 
    }
}
