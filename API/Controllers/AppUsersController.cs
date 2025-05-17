using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AppUsersController(DatingContext context) : BaseController
    {

        public async Task<ActionResult<IEnumerable<DatingAppUser>>> GetAppUsers()
        {
            var users = await context.AppUsers.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetAppUser(int id)
        {
            var user = await context.AppUsers.FindAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
    }
}
