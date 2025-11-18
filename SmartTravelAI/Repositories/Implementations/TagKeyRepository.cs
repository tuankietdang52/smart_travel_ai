using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using SmartTravelAI.Data;
using SmartTravelAI.Exceptions;
using SmartTravelAI.Models;

namespace SmartTravelAI.Repositories.Implementations
{
    public class TagKeyRepository : ITagKeyRepository
    {
        private readonly AppDbContext dbContext;

        public TagKeyRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TagKey> AddAsync(TagKey entity)
        {
            await dbContext.TagKeys.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            TagKey tagKey =
                await dbContext.TagKeys.FirstOrDefaultAsync(t => t.Id == id)
                ?? throw new ModelNotFoundException($"Tag Key with Id {id} not exist");
            dbContext.TagKeys.Remove(tagKey);
        }

        public async Task<IEnumerable<TagKey>> GetAllAsync()
        {
            return await dbContext.TagKeys.AsNoTracking().ToListAsync();
        }

        public async Task<TagKey?> GetByIdAsync(long id)
        {
            return await dbContext.TagKeys.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TagKey> UpdateAsync(long id, TagKey entity)
        {
            TagKey tagKey =
                await dbContext.TagKeys.FirstOrDefaultAsync(t => t.Id == id)
                ?? throw new ModelNotFoundException($"Tag Key with Id {id} not exist");

            entity.Adapt(tagKey);
            return tagKey;
        }
    }
}
