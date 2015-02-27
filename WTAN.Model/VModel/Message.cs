using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WTAN.CommonUtility;
using System.Web;

namespace WTAN.Model.VModel
{
    public class Message
    {
        public MessageState State { get; set; }
        public String TipContent { get; set; }
        public Message(MessageState state, String tipContent)
        {
            this.State = state;
            this.TipContent = tipContent;
        }

        public HtmlString GetMessageInfo()
        {
            StringBuilder info = new StringBuilder();
            info.AppendFormat("<div class=\"alert alert-{0}\">",this.State.ToString());
            info.Append("<button class=\"close\" data-dismiss=\"alert\" type=\"button\">×</button>");
            info.Append(this.TipContent);
            info.Append("</div>");
            return new HtmlString(info.ToString());
        }
    }
}
