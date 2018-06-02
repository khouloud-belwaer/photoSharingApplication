﻿using PhotoSharingApp.Models;
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
    [HandleError(View = "Error")]
    [ValueReporter]
    public class PhotoController : Controller
    {
        private IPhotoSharingContext context;
        public PhotoController()
        {
            context = new PhotoSharingContext();
        }
        public PhotoController(IPhotoSharingContext Context)
        {
            context = Context;
        }

        public ActionResult Index()
        {
            return View("Index");
        }

        [ChildActionOnly]
        public ActionResult _PhotoGallery
   (int number = 0)
        {
            List<Photo> photos;
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
                 Photo photo = context.FindPhotoById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            return View("Display", photo);
        }
        public ActionResult DisplayByTitle(string title)
        {
            Photo photo = context.FindPhotoByTitle(title);
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
            photo.CreatedDate = DateTime.Now;
            if (!ModelState.IsValid)
            {
                return View("Create", photo);
            }
            else
            {
                if (image != null)
                {

                    photo.ImageMimeType = image.ContentType;
                    photo.PhotoFile = new byte[image.ContentLength];
                    image.InputStream.Read(photo.PhotoFile, 0, image.ContentLength);
                }

            }
            context.Add<Photo>(photo);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

            public ActionResult Delete(int id)
        {
            
Photo photo = context.FindPhotoById(id);
            if (photo == null)
            {
                return HttpNotFound();
            }
            else
            {
                context.Delete<Photo>(photo);
                context.SaveChanges();
                return RedirectToAction("Index");
            }
        }



        public FileContentResult GetImage
   (int id)
        {
           
   Photo photo = context.FindPhotoById(id);
            if (photo != null)
            {
                return File(photo.PhotoFile,
                   photo.ImageMimeType);
            }
            else
            {
                return null;
            }
        }
        public ActionResult SlideShow()
        {
            return View("SlideShow", context.Photos.ToList());
        }
    }
     
         
         


    }
