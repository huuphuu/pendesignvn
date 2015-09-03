using PenDesign.Core.Model.BaseClass;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace PenDesign.Core.Model
{
    public partial class NewsDraft: EditableEntity
    {
        [Key]
        [DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [DataMember]
        public int? NewsId { get; set; }
        [DataMember]
        public int? CourseId { get; set; }
        [DataMember]
        public int? GalleryId { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Keyword { get; set; }
        [DataMember]
        public string Description { get; set; }
        [DataMember]
        public int Order { get; set; }
        [DataMember]
        public string PreviewImage { get; set; }
        [DataMember]
        public string Url { get; set; }
        [DataMember]
        public string HomeBannerImageUrl { get; set; }
        [DataMember]
        public string BannerImageIntro { get; set; }
        [DataMember]
        public string SubBannerImageUrl { get; set; }
        [DataMember]
        public string ContentIntro { get; set; }
        [DataMember]
        public string ContentLeft { get; set; }
        [DataMember]
        public string ContentRight { get; set; }
        [DataMember]
        public DateTime CreateTime { get; set; }
        [DataMember]
        public string DraftType { get; set; }
        [DataMember]
        public string EditDeleteBy { get; set; }
        [DataMember]
        public bool AdminChecked { get; set; }

    }
}
