using System;
using PenDesign.Core.Model;

namespace PenDesign.Core.ViewModel.BannerViewModel

{
    public class BannerVM
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LanguageId { get; set; }
        public int Type { get; set; }
        public int Position { get; set; }
        public int MediaType { get; set; }
        public string MediaUrl { get; set; }
        public string LinkUrl { get; set; }
        public int ZOrder { get; set; }
    }
}