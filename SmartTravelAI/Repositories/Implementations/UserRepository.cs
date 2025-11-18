using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using SmartTravelAI.Data;
using SmartTravelAI.Exceptions;
using SmartTravelAI.Models;

namespace SmartTravelAI.Repositories.Implementations
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext dbContext;

        public UserRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<User> AddAsync(User entity)
        {
            await dbContext.Users.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            User user =
                await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new ModelNotFoundException($"User with Id {id} not exist");
            dbContext.Users.Remove(user);
        }

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await dbContext.Users.AsNoTracking().ToListAsync();
        }

        public async Task<User?> GetByIdAsync(long id)
        {
            return await dbContext.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> UpdateAsync(long id, User entity)
        {
            User user =
                await dbContext.Users.FirstOrDefaultAsync(u => u.Id == id)
                ?? throw new ModelNotFoundException($"User with Id {id} not exist");

            entity.Adapt(user);
            return user;
        }
    }
}