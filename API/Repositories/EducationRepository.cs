using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class EducationRepository : GeneralRepository<Education>, IEducationRepository
    {
        protected EducationRepository(BookingManagementDbContext context) : base(context)
        {
        }
    }
}
