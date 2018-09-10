using System;
using System.Collections.Generic;
using System.Linq;
using VideoShare.DAL.Entities;
using VideoShare.DAL.EF;
using VideoShare.DAL.Interfaces;
using System.Threading.Tasks;

using System.Data.Entity;

namespace VideoShare.DAL.Repositories
{
    public class PlaylistRepository : IRepository<Playlist>
    {
        private ApplicationContext db;

        public PlaylistRepository(ApplicationContext context)
        {
            this.db = context;
        }

        public IEnumerable<Playlist> GetAll()
        {
            return db.Playlists;
        }

        public Playlist Get(int id)
        {
            return db.Playlists.Find(id);
        }

        public async Task<Playlist> GetAsync(int id)
        {
            return await db.Playlists.FindAsync(id);
        }

        public void Create(Playlist playlist)
        {
            db.Playlists.Add(playlist);
        }

        public void Update(Playlist playlist)
        {
            db.Entry(playlist).State = EntityState.Modified;
        }

        public IEnumerable<Playlist> Find(Func<Playlist, Boolean> predicate)
        {
            return db.Playlists.Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            Playlist playlist = db.Playlists.Find(id);
            if (playlist != null)
                db.Playlists.Remove(playlist);
        }
    }
}