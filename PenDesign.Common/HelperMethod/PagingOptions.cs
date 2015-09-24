using System;
using System.Collections.Generic;

namespace PenDesign.Common.HelperMethod
{
    public class PagingOptions
    {
        /// <summary>
        /// Hàm tạo với totalPage là tổng số trang cần hiển thị
        /// </summary>
        public PagingOptions(int totalPage)
        {
            this.TotalPage = totalPage;
            CreateWrapDiv = true;
            PageSegmentName = "page";
            IncludeQueryStringForPage1 = false;
            CurrentPage = NotSetCurrentPage;
            PageLinkHtmlAttributes = null;
            FirstLastLinkHtmlAttributes = null;
            CurrentPageLinkHtmlAttributes = new { @class = "selected" };
            DivHtmlAttributes = new { @class = "pager" };
            AllowGoToPage = true;
            TextBoxHtmlAttributes = new { style = "width:35px;", id = (string)null };
            GoButtonHtmlAttributes = new { style = "width:35px;" };
            MaxPageDisplay = 5;
            GoButtonTitle = "Go";
            FirstButtonTitle = "Đầu";
            LastButtonTitle = "Cuối";
        }

        /// <summary>
        /// Tổng sốt trang
        /// </summary>
        public int TotalPage { get; set; }

        /// <summary>
        /// tên của page segment trong Querystring. Mặc định là page
        /// </summary>
        public string PageSegmentName { get; set; }

        public bool IncludeQueryStringForPage1 { get; set; }

        private const int NotSetCurrentPage = int.MinValue;
        public int CurrentPage { get; set; }
        public bool IsNotSetCurrentPage()
        {
            return CurrentPage == NotSetCurrentPage;
        }

        /// <summary>
        /// Qui định có tạo ra cặp thẻ div bao ngoài hay ko, mặc định là true
        /// </summary>
        public bool CreateWrapDiv { get; set; }

        public object DivHtmlAttributes { get; set; }

        public object PageLinkHtmlAttributes { get; set; }

        public object FirstLastLinkHtmlAttributes { get; set; }

        public object CurrentPageLinkHtmlAttributes { get; set; }

        public bool AllowGoToPage { get; set; }

        public object TextBoxHtmlAttributes { get; set; }

        public object GoButtonHtmlAttributes { get; set; }

        public int MaxPageDisplay { get; set; }

        public string GoButtonTitle { get; set; }

        public string FirstButtonTitle { get; set; }
        public string LastButtonTitle { get; set; }

        public void RecaculatePagingInfo()
        {
            if (TotalPage < 1) TotalPage = 1;
            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > TotalPage) CurrentPage = TotalPage;
        }
    }
}
