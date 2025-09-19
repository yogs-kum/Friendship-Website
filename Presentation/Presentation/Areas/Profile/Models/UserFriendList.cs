using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Presentation.Areas.Profile.Models
{
	public class UserFriendList
	{
		public int Id { get; set; }
		public int UserId { get; set; }
		public int RequestedUserId { get; set; }
		public string UserName { get; set; }
		public string PicName { get; set; }
		public string ProfilePhotoID { get; set; }
		public int Status { get; set; }
		public int IsGroup { get; set; }
		public int ChatRoomId { get; set; }
		public string ChatRoomName { get; set; }
	}
}