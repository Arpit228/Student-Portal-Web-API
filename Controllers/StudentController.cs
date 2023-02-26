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
    public class StudentController : ControllerBase
    {
        private readonly ACE42023Context _context;

        public StudentController(ACE42023Context context)
        {
            _context = context;
        }

        // GET: api/Student
        [HttpGet]
        public async Task<ActionResult<IEnumerable<StudentArpit>>> GetStudentArpits()
        {
            return await _context.StudentArpits.ToListAsync();
        }

        // GET: api/Student/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StudentArpit>> GetStudentArpit(int id)
        {
            var studentArpit = await _context.StudentArpits.FindAsync(id);

            if (studentArpit == null)
            {
                return NotFound();
            }

            return studentArpit;
        }

        // PUT: api/Student/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStudentArpit(int id, StudentArpit studentArpit)
        {
            if (id != studentArpit.Rollno)
            {
                return BadRequest();
            }

            _context.Entry(studentArpit).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!StudentArpitExists(id))
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

        // POST: api/Student
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StudentArpit>> PostStudentArpit(StudentArpit studentArpit)
        {
            _context.StudentArpits.Add(studentArpit);
            try
            {
                _context.Add(studentArpit);
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                if (StudentArpitExists(studentArpit.Rollno))
                {
                    return BadRequest(new Exception($"Roll No. {studentArpit.Rollno} exists. Try a different Roll No."));
                }
                if(studentArpit.Cgpa < 0 || studentArpit.Cgpa > 10)
                {
                    return BadRequest(new Exception("CGPA can be in range of 0 to 10"));
                }
                else
                {
                    return BadRequest(e);
                }
            }

            return CreatedAtAction("GetStudentArpit", new { id = studentArpit.Rollno }, studentArpit);
        }

        // DELETE: api/Student/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStudentArpit(int id)
        {
            var studentArpit = await _context.StudentArpits.FindAsync(id);
            if (studentArpit == null)
            {
                return NotFound();
            }

            _context.StudentArpits.Remove(studentArpit);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool StudentArpitExists(int id)
        {
            return _context.StudentArpits.Any(e => e.Rollno == id);
        }
    }
}
