using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Areas.Profile.Models
{
    public class UserInfo
    {
        public string CustomConnectionId { get; set; }
        public string ConnectionId { get; set; }
        public string UserName { get; set; }
        public int UserID { get; set; }
        public string CurrentUserName { get; set; }
        public int CurrentUserID { get; set; }
        public string UserGroup { get; set; }

        //if freeflag==0 ==> Busy
        //if freeflag==1 ==> Free
        public string freeflag { get; set; }

        //if tpflag==2 ==> User Admin
        //if tpflag==0 ==> User Member
        //if tpflag==1 ==> Admin

        public string tpflag { get; set; }
        public int AdminID { get; set; }
    }
    public class MessageInfo
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public int ChatUserID { get; set; }
        public string ChatUserName { get; set; }
        public string Message { get; set; }

        public string UserGroup { get; set; }

        public string StartTime { get; set; }

        public string EndTime { get; set; }

        public string MsgDate { get; set; }
    }
    public class UserList
    {
        public string ConnectionId { get; set; }
        public int UserID { get; set; }
        public string UserName { get; set; }
        public string UserGroup { get; set; }
        public string freeflag { get; set; }
        public string tpflag { get; set; }
    }
    public class MessageDetails
    {
        public int UserId { get; set; }
        public int SendUserId { get; set; }

        public string Message { get; set; }
        public string MessageHtml { get; set; }
        public string ChatUserName { get; set; }
        public int ChatRoomId { get; set; }


    }

}