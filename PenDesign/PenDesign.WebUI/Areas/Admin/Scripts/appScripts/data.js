var $adminCMS = {};
$adminCMS.data = {
    "navigation": {
        "headerNav": [
            //{
            //    "name": "email",
            //    "url": "#",
            //    "imgIcon": "",
            //    "cssIcon": "fa fa-envelope-o",
            //    "labelType": "success",
            //    "dropdownClass": "dropdown messages-menu",
            //    "type": 1,
            //    "footerMessage": {
            //        "text": "Xem tất cả tin nhắn",
            //        "url": "#"
            //    }
            //},
            {
                "name": "Thông báo",
                "url": "",
                "imgIcon": "",
                "cssIcon": "fa fa-bell-o",
                "labelType": "warning",
                "dropdownClass": "dropdown notifications-menu",
                "type": 2,
                "footerMessage": {
                    "text": "Xem tất cả thông báo",
                    "url": "controlPanel.notification"
                }
            },
            //{
            //    "name": "task",
            //    "url": "#",
            //    "imgIcon": "",
            //    "cssIcon": "fa fa-flag-o",
            //    "labelType": "danger",
            //    "dropdownClass": "dropdown tasks-menu",
            //    "type": 3,
            //    "footerMessage": {
            //        "text": "Xem việc phải làm hôm nay",
            //        "url": "#"
            //    }
            //},
            {
                "name": "user",
                "url": "#",
                "imgIcon": "Areas/Admin/Content/Images/icons/user2-160x160.jpg",
                "cssIcon": "",
                "labelType": "",
                "dropdownClass": "dropdown user user-menu",
                "type": 4
            }
        ],

        "sidebarNav": [
            {
                "name": "Thông báo",
                "url": "controlPanel.notification",
                "cssIcon": "fa fa-bell-o",
                "labelCss": "",
                "childs": null,
                "type": "notification"
            },
            {
                "name": "Thư viện",
                "url": "#",
                "cssIcon": "fa fa-folder-open",
                "labelCss": "fa fa-angle-left",
                "childs": [
                    { "name": "Quản lý hình ảnh", "url": "controlPanel.gallery", "cssIcon": "fa fa-cloud-upload", "childs": "" },
                    { "name": "Quản lý Video Clip", "url": "controlPanel.gallery", "cssIcon": "fa fa-cloud-upload", "childs": "" },
                ]
            },
            {
                "name": "Banner",
                "url": "controlPanel.banner",
                "cssIcon": "fa fa-folder-open",
                "labelCss": "fa fa-angle-left",
                "childs": [
                    { "name": "Thêm Banner", "url": "controlPanel.addBanner", "cssIcon": "fa fa-users", "childs": "" },
                    { "name": "Danh sách Banner", "url": "controlPanel.bannerList", "cssIcon": "fa fa-user", "childs": "" }
                ]
            },
            {
                "name": "Nhóm/Thành viên",
                "url": "#",
                "cssIcon": "fa fa-user-md",
                "labelCss": "fa fa-angle-left",
                "childs": [
                    { "name": "Danh sách Nhóm", "url": "controlPanel.userGroupsList", "cssIcon": "fa fa-users", "childs": "" },
                    { "name": "Danh sách Thành viên", "url": "controlPanel.userList", "cssIcon": "fa fa-user", "childs": "" }
                ]
            },
            {
                "name": "Khóa học/Học viên",
                "url": "#",
                "cssIcon": "fa fa-folder",
                "labelCss": "fa fa-angle-left",
                "childs": [
                    { "name": "Thêm khóa học", "url": "controlPanel.addCourse", "cssIcon": "fa fa-plus-circle", "childs": "" },
                    { "name": "Danh sách khóa học", "url": "controlPanel.coursesList", "cssIcon": "fa fa-list-ol", "childs": "" },
                    { "name": "Thêm học viên", "url": "controlPanel.studentsList", "cssIcon": "fa fa-plus-circle", "childs": "" },
                    { "name": "Danh sách học viên", "url": "controlPanel.studentsList", "cssIcon": "fa fa-list-ol", "childs": "" }
                ]
            },
             {
                 "name": "Dịch vụ",
                 "url": "#",
                 "cssIcon": "fa fa-rocket",
                 "labelCss": "fa fa-angle-left",
                 "childs": [
                     { "name": "Thêm bài mới", "url": "controlPanel.addNews({newsCategoryId: 1})", "cssIcon": "fa fa-plus-circle", "childs": "" },
                     { "name": "Danh sách Bài viết", "url": "controlPanel.newsList({newsCategoryId: 1})", "cssIcon": "fa fa-list-ol", "childs": "" },
                 ]
             },
            {
                "name": "Tin tức",
                "url": "#",
                "cssIcon": "fa fa-file-text",
                "labelCss": "fa fa-angle-left",
                "childs": [
                    { "name": "Thêm bài mới", "url": "controlPanel.addNews({newsCategoryId: 3})", "cssIcon": "fa fa-plus-circle", "childs": "" },
                    { "name": "Danh sách Bài viết", "url": "controlPanel.newsList({newsCategoryId: 3})", "cssIcon": "fa fa-list-ol", "childs": "" },
                ]
            },
            {
                "name": "Sự kiện",
                "url": "#",
                "cssIcon": "fa fa-glass",
                "labelCss": "fa fa-angle-left",
                "childs": [
                    { "name": "Thêm sự kiện mới", "url": "controlPanel.addNews({newsCategoryId: 2})", "cssIcon": "fa fa-plus-circle", "childs": "" },
                    { "name": "Danh sách sự kiện", "url": "controlPanel.newsList({newsCategoryId: 2})", "cssIcon": "fa fa-list-ol", "childs": "" },
                ]
            },
            {
                "name": "Đối tác",
                "url": "#",
                "cssIcon": "fa fa-group",
                "labelCss": "fa fa-angle-left",
                "childs": [
                    { "name": "Thêm đối tác", "url": "controlPanel.addNews({newsCategoryId: 4})", "cssIcon": "fa fa-plus-circle", "childs": "" },
                    { "name": "Danh sách đối tác", "url": "controlPanel.newsList({newsCategoryId: 4})", "cssIcon": "fa fa-list-ol", "childs": "" },
                ]
            },
            {
                "name": "Email",
                "url": "#",
                "cssIcon": "fa fa-glass",
                "labelCss": "fa fa-angle-left",
                "childs": [
                    { "name": "Email đăng ký", "url": "controlPanel.email", "cssIcon": "fa fa-plus-circle", "childs": "" },
                    { "name": "Email liên hệ", "url": "controlPanel.contactEmail", "cssIcon": "fa fa-list-ol", "childs": "" },
                ]
            },
            {
                "name": "Cấu hình",
                "url": "#",
                "cssIcon": "fa fa-wrench",
                "labelCss": "fa fa-angle-left",
                "childs": [
                    { "name": "Tên công ty", "url": "controlPanel.configs", "cssIcon": "fa fa-flag-o", "childs": "" },
                    { "name": "Logo", "url": "controlPanel.configs", "cssIcon": "fa fa-flag-o", "childs": "" },
                    { "name": "Giới thiệu", "url": "controlPanel.configs", "cssIcon": "fa fa-flag-o", "childs": "" },
                    { "name": "Email", "url": "controlPanel.configs", "cssIcon": "fa fa-flag-o", "childs": "" },
                    { "name": "Địa chỉ", "url": "controlPanel.configs", "cssIcon": "fa fa-flag-o", "childs": "" },
                    { "name": "Điện thoại", "url": "controlPanel.configs", "cssIcon": "fa fa-flag-o", "childs": "" },
                    { "name": "Thông tin liên hệ", "url": "controlPanel.configs", "cssIcon": "fa fa-flag-o", "childs": "" },
                    { "name": "Thông tin đăng ký khóa học", "url": "controlPanel.configs", "cssIcon": "fa fa-flag-o", "childs": "" },
                    { "name": "Tài khoản mạng xã hội", "url": "controlPanel.configs", "cssIcon": "fa fa-flag-o", "childs": "" },
                ]
            }

        ]
    },

    "currentUser": {
        //"userName": "mrThanh",
        //"email": [
        //    { "title": "Support Team 1", "image": "Areas/Admin/Content/Images/icons/user2-160x160.jpg", "cssIcon": "fa fa-clock-o", "receivedAt": "Fri May 14 2015 01:19:48 GMT+0700 (SE Asia Standard Time)", "message": "Why not buy a new awesome theme?", "url": "#" },
        //    { "title": "Support Team 2", "image": "Areas/Admin/Content/Images/icons/user3-128x128.jpg", "cssIcon": "fa fa-clock-o", "receivedAt": "Fri May 12 2015 02:19:48 GMT+0700 (SE Asia Standard Time)", "message": "Why not buy a new awesome theme?", "url": "#" },
        //    { "title": "Support Team 3", "image": "Areas/Admin/Content/Images/icons/user4-128x128.jpg", "cssIcon": "fa fa-clock-o", "receivedAt": "Fri May 15 2015 07:15:48 GMT+0700 (SE Asia Standard Time)", "message": "Why not buy a new awesome theme?", "url": "#" },
        //    { "title": "Support Team 4", "image": "Areas/Admin/Content/Images/icons/user2-160x160.jpg", "cssIcon": "fa fa-clock-o", "receivedAt": "Fri May 15 2015 05:19:48 GMT+0700 (SE Asia Standard Time)", "message": "Why not buy a new awesome theme?", "url": "#" },
        //    { "title": "Support Team 5", "image": "Areas/Admin/Content/Images/icons/user2-160x160.jpg", "cssIcon": "fa fa-clock-o", "receivedAt": "Fri May 15 2015 03:19:48 GMT+0700 (SE Asia Standard Time)", "message": "Why not buy a new awesome theme?", "url": "#" },
        //    { "title": "Support Team 6", "image": "Areas/Admin/Content/Images/icons/user2-160x160.jpg", "cssIcon": "fa fa-clock-o", "receivedAt": "Fri May 15 2015 07:00:48 GMT+0700 (SE Asia Standard Time)", "message": "Why not buy a new awesome theme?", "url": "#" }
        //],
        "notification": [
            { "cssIcon": "fa fa-users text-aqua", "message": "5 new members joined today", "url": "#" },
            { "cssIcon": "fa fa-warning text-yellow", "message": "Very long description here that may not fit into the page and may cause design problems", "url": "#" },
            { "cssIcon": "fa fa-users text-red", "message": "5 new members joined", "url": "#" },
            { "cssIcon": "fa fa-shopping-cart text-green", "message": "25 sales made", "url": "#" },
            { "cssIcon": "fa fa-user text-red", "message": "You changed your username", "url": "#" }
        ],
        //"task": [
        //    { "title": "Design some buttons", "completePercent": 20, "url": "#" },
        //    { "title": "Create a nice theme", "completePercent": 40, "url": "#" },
        //    { "title": "Some task I need to do", "completePercent": 60, "url": "#" },
        //    { "title": "Make beautiful transitions", "completePercent": 80, "url": "#" }
        //],
        "userInformation": {
            "userName": "Thanh Ly",
            "userInfo": {
                "avatar": "Images/icons/user2-160x160.jpg",
                "email": "mrthanh.kientao@gmail.com"
            }
            ,
        }
    }
};
