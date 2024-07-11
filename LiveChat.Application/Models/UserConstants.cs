using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiveChat.Application.Models
{
    public class UserConstants
    {
        public static List<UserModel> Users = new List<UserModel>()
        {
            new UserModel() { Name = "LiveChatAdmin", Email = "livechat@admin.com", Password = "LiveChatAdmin", Role = "admin"}
        };
    }
}
