using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PhotoSharingApp.Models;
using System.Collections.ObjectModel;

namespace PhotoSharingTests.Doubles
{
    class FakePhotoSharingContext : IPhotoSharingContext
    {
        //This object is a keyed collection we use to mock an 
        //entity framework context in memory
        SetMap _map = new SetMap();
        public IQueryable<Photo> Photos
        {
            get { return _map.Get<Photo>().AsQueryable(); }
            set { _map.Use<Photo>(value); }
        }

        public IQueryable<Commentaire> Comments
        {
            get { return _map.Get<Commentaire>().AsQueryable(); }
            set { _map.Use<Commentaire>(value); }
        }

        public bool ChangesSaved { get; set; }

        public IQueryable<Commentaire> Commentaire
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public int SaveChanges()
        {
            ChangesSaved = true;
            return 0;
        }

        public T Add<T>(T entity) where T : class
        {
    _map.Get<T>().Add(entity);
                return entity;
            }

        public Photo FindPhotoById(int ID)
        {
            Photo item = (from p in this.Photos
                          where p.PhotoID == ID
                          select p).First();

            return item;
        }

        public Commentaire FindCommentById(int ID)
        {
            Commentaire item = (from c in this.Comments
                            where c.CommentaireID == ID
                            select c).First();
            return item;
        }

        public T Delete<T>(T entity) where T : class
        {
    _map.Get<T>().Remove(entity);
                return entity;
            }

        public Photo FindPhotoByTitle(string Title)
        {
            throw new NotImplementedException();
        }

        class SetMap : KeyedCollection<Type, object>
        {

            public HashSet<T> Use<T>(IEnumerable<T> sourceData)
            {
                var set = new HashSet<T>(sourceData);
                if (Contains(typeof(T)))
                {
                    Remove(typeof(T));
                }
                Add(set);
                return set;
            }

            public HashSet<T> Get<T>()
            {
                return (HashSet<T>)this[typeof(T)];
            }

            protected override Type GetKeyForItem(object item)
            {
                return item.GetType().GetGenericArguments().Single();
            }
        }    }
}