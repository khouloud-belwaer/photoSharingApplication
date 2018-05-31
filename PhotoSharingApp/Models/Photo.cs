using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoSharingApp.Models
{
    public class Photo
    {
        public int PhotoID { get; set; }
        [Required]
        public String Title { get; set; }
        [DisplayName("Description")]
        [DataType(DataType.MultilineText)]
        public String Description { get; set; }
        [DataType(DataType.DateTime)]
        [DisplayName("Created Date")]
        [DisplayFormat(DataFormatString="{0:MM/dd/yy}",ApplyFormatInEditMode = true)]
        public DateTime CreatedDate { get; set; }
        public String UserName { get; set; }
        public byte [] PhotoFile{ get; set; }
        [DataType(DataType.MultilineText)]
        public virtual ICollection<Commentaire> Commentaires { get; set; }
        public string ImageMimeType { get; set; }







    }
}