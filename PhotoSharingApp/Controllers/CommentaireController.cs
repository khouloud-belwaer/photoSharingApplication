using PhotoSharingApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PhotoSharingApp.Controllers
{
    public class CommentaireController : Controller
    {
        private IPhotoSharingContext context;

      
        public CommentaireController()
        {
            context = new PhotoSharingContext();
        }

        public CommentaireController(IPhotoSharingContext Context)
        {
            context = Context;
        }

        
        public ActionResult Delete(int id = 0)
        {
            Commentaire commentaire = context.FindCommentById(id);
            ViewBag.PhotoID = commentaire.PhotoID;
            if (commentaire == null)
            {
                return HttpNotFound();
            }
            return View(commentaire);
        }

       
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
       {
           Commentaire commentaire = context.FindCommentById(id);
            context.Delete<Commentaire>(commentaire);
            context.SaveChanges();
            return RedirectToAction("Display", "Photo", new { id = commentaire.PhotoID });
        }
        [ChildActionOnly]
        public PartialViewResult _CommentsForPhoto(int PhotoId)
        {
            var commentaire = from c in context.Commentaire
                              where c.PhotoID == PhotoId
                              select c;
            ViewBag.PhotoId = PhotoId;
            return PartialView("_CommentsForPhoto", commentaire.ToList());


        }
        public PartialViewResult _Create(int PhotoId)
        {
            Commentaire newComment = new Commentaire();
            newComment.PhotoID = PhotoId;
            ViewBag.PhotoID = PhotoId;
            return PartialView("_CreateAComment");
        }
        [HttpPost]
        public PartialViewResult _CommentsForPhoto(Commentaire comment, int PhotoId)
        {
            context.Add<Commentaire>(comment);
            context.SaveChanges();
            var comments = from c in context.Commentaire
                           where c.PhotoID == PhotoId
                           select c;
            ViewBag.PhotoId = PhotoId;
            return PartialView("_CommentsForPhoto", comments.ToList());
        }
       
    }
   
} 