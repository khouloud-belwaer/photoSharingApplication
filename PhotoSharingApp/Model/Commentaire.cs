using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PhotoSharingApp.Model
{
    public class Commentaire
    {
        public int CommentaireID { get; set; }
        public String User { get; set; }
        [Required]
        [StringLength(250)]
        public String Subject { get; set; }
        [DataType(DataType.MultilineText)]
        public String Body { get; set; }
        public int PhotosID { get; set; }
        public virtual Photo Photo { get; set; }
        public int PhotoID { get;  set; }
        public string UserName { get; set; }
    }
}