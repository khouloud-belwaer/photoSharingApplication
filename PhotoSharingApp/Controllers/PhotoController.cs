using PhotoSharingApp.Model;
using System.Collections.Generic;
using System.Globalization;
using System.ComponentModel;
using System;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;


namespace PhotoSharingApp.Controllers
{
    [ValueReporter]
    public class PhotoController : Controller
    {
        PhotoSharingContext context = new PhotoSharingContext();
       
        public ActionResult Index()
        {
            return View("Index");
        }

        [ChildActionOnly]
        public ActionResult _PhotoGallery
  (int number = 0)
        {
            List<Photo> photos = new List<Photo>();
            if (number == 0)
            {
                photos = context.Photos.ToList();
            }
            else
            {
                photos = (
                      from p in context.Photos
                      orderby p.CreatedDate descending
                      select p).Take(number).ToList();
            }
            return PartialView("_PhotoGallery",
   photos);
        }
        public ActionResult Display(int id)
        {
            Photo photo =
    context.Photos.Find(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View("Display", photo);
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
