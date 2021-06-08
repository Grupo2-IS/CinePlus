using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CinePlus.Entities;

namespace CinePlus.Context.Repositories
{
    public class ArtistRepository : IArtistRepository
    {
        private ConcurrentDictionary<int, Artist> artistCache;
        private CinePlusDb db;

        public ArtistRepository(CinePlusDb db)
        {
            this.db = db;

            if (artistCache == null)
            {
                artistCache = new ConcurrentDictionary<int, Artist>(
                    db.Artists
                    .ToDictionary(t => t.ArtistID)
                );
            }
        }

        public Task<Artist> CreateAsync(Artist film)
        {
            throw new System.NotImplementedException();
        }

        public Task<bool?> DeleteAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<IEnumerable<Artist>> RetrieveAllAsync()
        {
            return Task.Run<IEnumerable<Artist>>(
                () => artistCache.Values
            );
        }

        public Task<Artist> RetrieveAsync(int id)
        {
            throw new System.NotImplementedException();
        }

        public Task<Artist> UpdateAsync(int id, Artist film)
        {
            throw new System.NotImplementedException();
        }
    }
}