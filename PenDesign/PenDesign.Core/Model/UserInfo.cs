using Newtonsoft.Json;
using PenDesign.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace PenDesign.Core.Model
{
    [DataContract(IsReference = true)]
    public class UserInfo
    {
        //[Key]
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        [DataMember]
        public int UserInfoId { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Avatar { get; set; }

        [DataMember]
        public string Address { get; set; }

        [DataMember]
        public string Skype { get; set; }

        [DataMember]
        public string Yahoo { get; set; }

        [DataMember]
        public string Facebook { get; set; }

        [DataMember]
        public bool Available { get; set; }

        //public int AspNetUserId { get; set; }
        //[ForeignKey("AspNetUserId")]
        [Required]
        [DataMember]
        public ApplicationUser AspNetUser { get; set; }

        [JsonIgnore]
        [IgnoreDataMember]
        public virtual List<News> News { get; set; }
    }
}