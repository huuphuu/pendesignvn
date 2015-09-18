using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PenDesign.Core.ViewModel
{
    public class AdminMenuViewModel
    {
        private bool _Available = true;

        public string Name { get; set; }
        public string Url { get; set; }
        public string CssIcon { get; set; }
        public string LabelCss { get; set; }
        public string Type { get; set; }
        public int Parent { get; set; }
        public int Order { get; set; }
        public bool Available
        {
            get { return _Available; }
            set { _Available = value; }
        }

        public List<AdminMenuViewModel> Childs { get; set; }
    }
}
