
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PhotoSharingApp.Models
{
    public class PhotoSharingContext : DbContext, IPhotoSharingContext
    {
        public DbSet<Photo> Photos
        { get; set; }
        public DbSet<Commentaire> Commentaire
        { get; set; }
        IQueryable<Photo> IPhotoSharingContext.Photos
        {
            get { return Photos; }
        }
        IQueryable<Commentaire> IPhotoSharingContext.Commentaire
        {
            get { return Commentaire; }
        }
        int IPhotoSharingContext.SaveChanges()
        {
            return SaveChanges();
        }
        T IPhotoSharingContext.Add<T>(T entity)
        {
            return Set<T>().Add(entity);
        }
        Photo IPhotoSharingContext.FindPhotoById(int ID)
        {
            return Set<Photo>().Find(ID);
        }
        Photo IPhotoSharingContext.FindPhotoByTitle(string title)
        {
            List<Photo> photos = new List<Photo>();
            photos = Photos.ToList();
            Photo result = photos.Find(x => x.Title == title)
                ; return result;
        }

        Commentaire IPhotoSharingContext.FindCommentById(int ID)
        {
            return Set<Commentaire>().Find(ID);
        }
        T IPhotoSharingContext.Delete<T>(T entity)
        {
            return Set<T>().Remove(entity);
        }
    }
}