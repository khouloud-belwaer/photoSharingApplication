using PhotoSharingApp.Model;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;

namespace PhotoSharingApp.Models
{
    public class PhotoSharingInitializer : DropCreateDatabaseAlways<PhotoSharingContext> 
    {
        protected override void Seed(PhotoSharingContext context)

        {
            var photos = new List<Photo>
{
   new Photo {
      Title = "Test Photo",
Description = "Description", 
      UserName = "NaokiSato",
PhotoFile = System.IO.File.ReadAllBytes("\\Images\\flower.jpg"),
ImageMimeType =
         "image/jpeg",
      CreatedDate = DateTime.Today
   }
};
            photos.ForEach(s =>
   context.Photos.Add(s));
            context.SaveChanges();
            var commentaire = new List<Commentaire>
{
   new Commentaire {
      PhotoID = 1,
      UserName = "NaokiSato",
      Subject = "Test Comment",
      Body = "This comment " +
         "should appear in " +
         "photo 1"
   }
};
            commentaire.ForEach(s =>
   context.Commentaire.Add(s));
            context.SaveChanges();




        }

       
    }

   
   
}