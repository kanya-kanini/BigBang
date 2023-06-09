﻿using HotelBooking.Database;
using HotelBooking.Models;
using Microsoft.EntityFrameworkCore;

namespace pracapiapp.Repositories
{
    public class HotelRepository : IHotelRepository
    {
        private readonly HotelResDbContext projectcontext;

        public HotelRepository(HotelResDbContext context)
        {
            this.projectcontext = context;
        }

        public async Task<Hotel> DelHotelsAsync(int id)
        {
            try
            {
                Hotel del = await projectcontext.Hotels.FirstOrDefaultAsync(x => x.HotelId == id);
                projectcontext.Hotels.Remove(del);
                await projectcontext.SaveChangesAsync();
                return del;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Hotel>> GetAllHotelsAsync()
        {
            try
            {
                return await projectcontext.Hotels.Include(x => x.Rooms).
                    Include(x => x.Staffs)
                     .Include(x => x.Customers).ToListAsync();
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<Hotel> GetHotelByIdAsync(int id)
        {
            try
            {
                return await projectcontext.Hotels.Include(x => x.Rooms)
                                                  .Include(x => x.Staffs)
                                                  .Include(x => x.Customers)
                                                  .FirstOrDefaultAsync(x => x.HotelId == id);
            }
            catch (Exception) 
            { throw; }
        }

        public async Task<Hotel> PostHotelsAsync(Hotel hotel)
        {
            try
            {
                projectcontext.Hotels.Add(hotel);
                await projectcontext.SaveChangesAsync();
                return hotel;
            }
            catch (Exception) { throw; }
        }

        public async Task<Hotel> PutHotelAsync(int id, Hotel hotel)
        {
            try
            {
                projectcontext.Entry(hotel).State = EntityState.Modified;
                await projectcontext.SaveChangesAsync();
                return hotel;
            }
            catch (Exception) { throw; }
        }
    }
}

