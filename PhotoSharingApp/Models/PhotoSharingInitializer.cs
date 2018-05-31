using PhotoSharingApp.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
           List<Photo> photos = new List<Photo>();
            Photo photo = new Photo();
                photo.CreatedDate = DateTime.Now;
          
            photo.Title = "Test Photo"; 
         photo.Description="Desc";
      photo.UserName = "NaokiSato";
            photo.PhotoFile = System.IO.File.ReadAllBytes("\\Users\\khouloud\\photoSharingApplication\\PhotoSharingApp\\NewFolder1\\a.jpg");
            photo.ImageMimeType =
                     "image/jpeg";
            foreach (var p in photos)
                           {
                context.Photos.Add(p);
                           }
            context.SaveChanges();

            List<Commentaire> commentaire = new List<Commentaire>();

            Commentaire comment = new Commentaire();
                comment.PhotoID = 1;
            comment.UserName = "NaokiSato";
           comment.Subject = "Test Comment";
            comment.Body = "This comment should apprear in photo 1";



            foreach (var c in commentaire)
            {
                context.Commentaire.Add(c);
            }
            context.SaveChanges();


        }

       
    }

   
   
}