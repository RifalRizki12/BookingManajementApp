﻿using API.Contracts;
using API.Data;
using API.Models;

namespace API.Repositories
{
    public class BookingRepository : GeneralRepository<Booking>, IBookingRepository
    {
        protected BookingRepository(BookingManagementDbContext context) : base(context)
        {
        }
    }
}
