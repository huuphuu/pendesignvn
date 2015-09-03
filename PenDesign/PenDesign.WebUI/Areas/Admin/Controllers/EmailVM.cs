using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace thapsangthuonghieu.vn.Areas.Admin.Controllers
{
    public class EmailVM
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
