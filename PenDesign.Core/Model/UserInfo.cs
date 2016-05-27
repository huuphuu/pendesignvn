using Newtonsoft.Json;
using PenDesign.Core.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;
using PenDesign.Core.Model.BaseClass;

namespace PenDesign.Core.Model
{
    [DataContract(IsReference = true)]
    public class UserInfo: EditableEntity
    {
        //[Key]
        //[DatabaseGeneratedAttribute(DatabaseGeneratedOption.Identity)]
        [Key]
        [DataMember]
//        public int UserInfoId { get; set; }
        public string AspNetUser_Id { get; set; }

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
        public virtual ApplicationUser AspNetUser { get; set; }
    }
}