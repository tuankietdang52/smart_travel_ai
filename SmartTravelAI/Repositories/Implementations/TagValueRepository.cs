using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using SmartTravelAI.Data;
using SmartTravelAI.Exceptions;
using SmartTravelAI.Models;

namespace SmartTravelAI.Repositories.Implementations
{
    public class TagValueRepository : ITagValueRepository
    {
        private readonly AppDbContext dbContext;

        public TagValueRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<TagValue> AddAsync(TagValue entity)
        {
            await dbContext.TagValues.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            TagValue tagValue =
                await dbContext.TagValues.FirstOrDefaultAsync(t => t.Id == id)
                ?? throw new ModelNotFoundException($"Tag Value with Id {id} not exist");
            dbContext.TagValues.Remove(tagValue);
        }

        public async Task<IEnumerable<TagValue>> GetAllAsync()
        {
            return await dbContext.TagValues.AsNoTracking().ToListAsync();
        }

        public async Task<TagValue?> GetByIdAsync(long id)
        {
            return await dbContext.TagValues.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<TagValue> UpdateAsync(long id, TagValue entity)
        {
            TagValue tagValue =
                await dbContext.TagValues.FirstOrDefaultAsync(t => t.Id == id)
                ?? throw new ModelNotFoundException($"Tag Value with Id {id} not exist");

            entity.Adapt(tagValue);
            return tagValue;
        }
    }
}
