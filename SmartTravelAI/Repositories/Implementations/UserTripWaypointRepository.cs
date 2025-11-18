using System.Threading.Tasks;
using Mapster;
using Microsoft.EntityFrameworkCore;
using SmartTravelAI.Data;
using SmartTravelAI.Exceptions;
using SmartTravelAI.Models;

namespace SmartTravelAI.Repositories.Implementations
{
    public class UserTripWaypointRepository : IUserTripWaypointRepository
    {
        private readonly AppDbContext dbContext;

        public UserTripWaypointRepository(AppDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<UserTripWaypoint> AddAsync(UserTripWaypoint entity)
        {
            await dbContext.UserTripWaypoints.AddAsync(entity);
            return entity;
        }

        public async Task DeleteAsync(long id)
        {
            UserTripWaypoint waypoint =
                await dbContext.UserTripWaypoints.FirstOrDefaultAsync(w => w.Id == id)
                ?? throw new ModelNotFoundException($"User Trip Waypoint with Id {id} not exist");
            dbContext.UserTripWaypoints.Remove(waypoint);
        }

        public async Task<IEnumerable<UserTripWaypoint>> GetAllAsync()
        {
            return await dbContext.UserTripWaypoints.AsNoTracking().ToListAsync();
        }

        public async Task<UserTripWaypoint?> GetByIdAsync(long id)
        {
            return await dbContext.UserTripWaypoints.AsNoTracking().FirstOrDefaultAsync(w => w.Id == id);
        }

        public async Task<UserTripWaypoint> UpdateAsync(long id, UserTripWaypoint entity)
        {
            UserTripWaypoint waypoint =
                await dbContext.UserTripWaypoints.FirstOrDefaultAsync(w => w.Id == id)
                ?? throw new ModelNotFoundException($"User Trip Waypoint with Id {id} not exist");

            entity.Adapt(waypoint);
            return waypoint;
        }
    }
}
