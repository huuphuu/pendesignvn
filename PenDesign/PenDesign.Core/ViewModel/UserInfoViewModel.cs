using PenDesign.Core.Model;
using System;
using System.Collections.Generic;

namespace PenDesign.Core.ViewModel
{
    public class UserInfoViewModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public UserInfo UserInfo { get; set; }
        public List<UserNotificationViewModel> UserNotifications { get; set; }
        public string Email { get; set; }
    }
}
