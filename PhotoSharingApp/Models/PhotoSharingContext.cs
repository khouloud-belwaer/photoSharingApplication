using PhotoSharingApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhotoSharingApp.Models
{
    public class PhotoSharingContext : System.Data.Entity.DbContext
    {
        public DbSet<Photo> Photos { get; set; }

        public DbSet<Commentaire> Comments { get; set; }

    }
}