using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using SmartTravelAI.Data;
using SmartTravelAI.Exceptions;
using SmartTravelAI.Models;

namespace SmartTravelAI.Repositories.Implementations
{
    public class UserTripRouteRepository : IUserTripRouteRepository
    {
        private readonly AppDbContext dbContext;

        public UserTripRouteRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserTripRoute> AddAsync(UserTripRoute entity)
        {
            await dbContext.UserTripRoutes.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            UserTripRoute route =
                await dbContext.UserTripRoutes.FirstOrDefaultAsync(r => r.Id == id)
                ?? throw new ModelNotFoundException($"User Trip Route with Id {id} not exist");
            dbContext.UserTripRoutes.Remove(route);
        }

        public async Task<IEnumerable<UserTripRoute>> GetAllAsync()
        {
            return await dbContext.UserTripRoutes.AsNoTracking().ToListAsync();
        }

        public async Task<UserTripRoute?> GetByIdAsync(long id)
        {
            return await dbContext.UserTripRoutes.AsNoTracking().FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task<UserTripRoute> UpdateAsync(long id, UserTripRoute entity)
        {
            UserTripRoute route =
                await dbContext.UserTripRoutes.FirstOrDefaultAsync(r => r.Id == id)
                ?? throw new ModelNotFoundException($"User Trip Route with Id {id} not exist");

            entity.Adapt(route);
            return route;
        }
    }
}
