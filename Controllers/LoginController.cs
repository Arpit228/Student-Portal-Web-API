using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentServer.Models;

namespace StudentServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly ACE42023Context _context;

        public LoginController(ACE42023Context context)
        {
            _context = context;
        }

        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login([FromBody] AdminArpit user)
        {
            var Userid = user.Userid;
            var Password = user.Pwd;
            if (_context.AdminArpits == null)
            {
                return NotFound();
            }
            var empInDB = await _context.AdminArpits.SingleOrDefaultAsync((e) => e.Userid == Userid && e.Pwd == Password);
            if (empInDB == null)
                return BadRequest(new Exception("Invalid Username/Password"));
            return Ok(empInDB);
           
        }

        [HttpPost]
        [Route("Register")]
        public async Task<ActionResult> Register(AdminArpit user)
        {
            if (_context.AdminArpits == null)
            {
                return NotFound();
            }

            try
            {
                _context.Add(user);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (AdminArpitExists(user.Userid))
                {
                    return BadRequest(new Exception($"User ID {user.Userid} is already used. Try a different User ID."));
                }
                else if (AdminArpitExists(user.Username))
                {
                    return BadRequest(new Exception($"Username {user.Username} is already used. Try a different Username."));
                }
                else
                {
                    return BadRequest(e);
                }
            }

            return CreatedAtAction("Login", user);
        }

        // GET: api/Login
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AdminArpit>>> GetAdminArpits()
        {
            return await _context.AdminArpits.ToListAsync();
        }

        // PUT: api/Login/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAdminArpit(int id, AdminArpit adminArpit)
        {
            if (id != adminArpit.Userid)
            {
                return BadRequest();
            }

            _context.Entry(adminArpit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AdminArpitExists(id))
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

        private bool AdminArpitExists(string name)
        {
            return _context.AdminArpits.Any(e => e.Username == name);
        }

        
        private bool AdminArpitExists(int id)
        {
            return _context.AdminArpits.Any(e => e.Userid == id);
        }
    }
}
