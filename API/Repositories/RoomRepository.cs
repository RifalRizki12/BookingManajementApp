using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class RoomRepository : GeneralRepository<Room>, IRoomRepository
    {
        protected RoomRepository(BookingManagementDbContext context) : base(context)
        {
        }
    }
}
