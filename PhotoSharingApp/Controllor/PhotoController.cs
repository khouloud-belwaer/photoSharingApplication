using PhotoSharingApp.Model;
using System.Collections.Generic;
using System.Globalization;
using PhotoSharingApp.Models;
using System;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using PhotoSharingApp.Controllers;

namespace PhotoSharingApp.Controllor
{
    [ValueReporter]
    public class PhotoController : Controller
    {
        PhotoSharingContext context = new PhotoSharingContext();
       
        public ActionResult Index()
        {
            return View(context.Photos.ToList());
        }


        public ActionResult Display(int id)
        { 
           List<Photo> photos = context.Photos.ToList();
           var verif = photos.Find(photo => photo.PhotoID == id);
            if (verif != null)
                return View("Display", verif);
            else
                return HttpNotFound();
        }
        public ActionResult Create()
        {
            Photo photo = new Photo();
            photo.CreatedDate= DateTime.Now;
            return View("Create", photo);
       }

        [HttpPost]
        public ActionResult Create(Photo photo, HttpPostedFileBase image)
        {
                photo.CreatedDate= DateTime.Now;
            if (ModelState.IsValid)
           {
               if (image!=null)                {
                   photo.ImageMimeType = image.ContentType;
                   photo.PhotoFile = new byte[image.ContentLength];
                    image.InputStream.Read(photo.PhotoFile,0,image.ContentLength);
                    context.Photos.Add(photo);
                    context.SaveChanges();

            return RedirectToAction("Index");                }
                return RedirectToAction("Index");
           }            else
            {
                return View("Create", photo);
            }
         }

        public ActionResult Delete(int id)
        {
            List<Photo> photos = context.Photos.ToList();
            var verif = photos.Find(photo => photo.PhotoID == id);
            if (verif==null)
            {
                return HttpNotFound();
          }
            else
            {
                return View("Delete", verif);
            }
        }
       [HttpPost]
        [ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            List<Photo> photos = context.Photos.ToList();
           var verif = photos.Find(photo => photo.PhotoID == id);
           context.Entry(verif).State = EntityState.Deleted;
           context.SaveChanges();
           return RedirectToAction("Index");
        }
        public FileContentResult GetImage(int id)
        {
            List<Photo> photos = context.Photos.ToList();
            var verif = photos.Find(photo => photo.PhotoID == id);
            if(verif!=null)
            {
               
               return (new FileContentResult(verif.PhotoFile, verif.ImageMimeType));
            }
            else
            {
                return null;
            }
        }
     }
     
         
         


    }
