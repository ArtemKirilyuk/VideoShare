using System;
using System.Collections.Generic;
using System.Linq;
using VideoShare.DAL.Entities;
using VideoShare.DAL.EF;
using System.Data.Entity;
using VideoShare.DAL.Interfaces;
using System.Threading.Tasks;

namespace VideoShare.DAL.Repositories
{
    public class CommentRepository : IRepository<Comment>
    {
        private ApplicationContext db;

        public CommentRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Comment> GetAll()
        {
            return db.Comments;
        }

        public Comment Get(int id)
        {
            return db.Comments.Find(id);
        }

        public async Task<Comment> GetAsync(int id)
        {
            return await db.Comments.FindAsync(id);
        }

        public void Create(Comment comment)
        {
            db.Comments.Add(comment);
        }

        public void Update(Comment comment)
        {
            db.Entry(comment).State = EntityState.Modified;
        }

        public IEnumerable<Comment> Find(Func<Comment, Boolean> predicate)
        {
            return db.Comments.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Comment comment = db.Comments.Find(id);
            if (comment != null)
                db.Comments.Remove(comment);
        }
    }
}