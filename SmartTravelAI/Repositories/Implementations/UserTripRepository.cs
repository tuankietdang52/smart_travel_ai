using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using SmartTravelAI.Data;
using SmartTravelAI.Exceptions;
using SmartTravelAI.Models;

namespace SmartTravelAI.Repositories.Implementations
{
    public class UserTripRepository : IUserTripRepository
    {
        private readonly AppDbContext dbContext;

        public UserTripRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserTrip> AddAsync(UserTrip entity)
        {
            await dbContext.UserTrips.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            UserTrip trip =
                await dbContext.UserTrips.FirstOrDefaultAsync(t => t.Id == id)
                ?? throw new ModelNotFoundException($"User Trip with Id {id} not exist");
            dbContext.UserTrips.Remove(trip);
        }

        public async Task<IEnumerable<UserTrip>> GetAllAsync()
        {
            return await dbContext.UserTrips.AsNoTracking().ToListAsync();
        }

        public async Task<UserTrip?> GetByIdAsync(long id)
        {
            return await dbContext.UserTrips.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id);
        }

        public async Task<UserTrip> UpdateAsync(long id, UserTrip entity)
        {
            UserTrip trip =
                await dbContext.UserTrips.FirstOrDefaultAsync(t => t.Id == id)
                ?? throw new ModelNotFoundException($"User Trip with Id {id} not exist");

            entity.Adapt(trip);
            return trip;
        }
    }
}
