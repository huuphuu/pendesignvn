using PenDesign.Core.Model.BaseClass;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PenDesign.Core.Model
{
    public partial class Config: EditableEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string CompanyName { get; set; }
        public string Slogan { get; set; }
        public string About { get; set; }

        public string Email { get; set; }
        public string EmailPassword { get; set; }
        public int EmailPort { get; set; }
        public string EmailSignature { get; set; }

        public string GoogleAnalytics { get; set; }
        public string Address { get; set; }
        public string GoogleMap { get; set; }
        public string GoogleWebMaster { get; set; }
        public string FacebookRetager { get; set; }

        public string Phone { get; set; }
        public string LogoUrl { get; set; }

        public string ContactForm { get; set; }
        public string FooterContent { get; set; }

        public string Yahoo { get; set; }
        public string Skype { get; set; }
        public string FacebookSocial { get; set; }
        public string GoogleSocial { get; set; }
        public string TwitterSocial { get; set; }
        public string PicasaSocial { get; set; }
        public string Youtube { get; set; }
        public string Instagram { get; set; }
    }
}