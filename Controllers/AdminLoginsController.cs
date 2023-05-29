using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HotelBooking.Database;
using HotelBooking.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace pracapiapp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminLoginsController : ControllerBase
    {
        private readonly HotelResDbContext _context;

        public AdminLoginsController(HotelResDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminLogin>>> GetAdminLogin()
        {
          if (_context.AdminLogin == null)
          {
              return NotFound();
          }
            return await _context.AdminLogin.ToListAsync();
        }

     
        [HttpGet("{id}")]
        public async Task<ActionResult<AdminLogin>> GetAdminLogin(int id)
        {
          if (_context.AdminLogin == null)
          {
              return NotFound();
          }
            var adminLogin = await _context.AdminLogin.FindAsync(id);

            if (adminLogin == null)
            {
                return NotFound();
            }

            return adminLogin;
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdminLogin(int id, AdminLogin adminLogin)
        {
            if (id != adminLogin.AdminId)
            {
                return BadRequest();
            }

            _context.Entry(adminLogin).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminLoginExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        
        [HttpPost]
        public async Task<ActionResult<AdminLogin>> PostAdminLogin(AdminLogin adminLogin)
        {
          if (_context.AdminLogin == null)
          {
              return Problem("Entity set 'HotelResDbContext.AdminLogin'  is null.");
          }
            _context.AdminLogin.Add(adminLogin);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAdminLogin", new { id = adminLogin.AdminId }, adminLogin);
        }

       
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAdminLogin(int id)
        {
            if (_context.AdminLogin == null)
            {
                return NotFound();
            }
            var adminLogin = await _context.AdminLogin.FindAsync(id);
            if (adminLogin == null)
            {
                return NotFound();
            }

            _context.AdminLogin.Remove(adminLogin);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AdminLoginExists(int id)
        {
            return (_context.AdminLogin?.Any(e => e.AdminId == id)).GetValueOrDefault();
        }
    }
}
