using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoSharingApp.Models
{
    public interface IPhotoSharingContext
    {
        IQueryable<Photo> Photos { get; }
        IQueryable<Commentaire> Commentaire { get; }
        int SaveChanges();
        T Add<T>(T entity) where T : class;
        Photo FindPhotoById(int ID);
        Commentaire FindCommentById(int ID);
        T Delete<T>(T entity) where T : class;
    }
}
