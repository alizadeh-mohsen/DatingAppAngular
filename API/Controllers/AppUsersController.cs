using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [Authorize]
    public class AppUsersController(DatingContext context) : BaseController
    {

        [AllowAnonymous]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DatingAppUser>>> GetAppUsers()
        {
            var users = await context.AppUsers.ToListAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DatingAppUser>> GetAppUser(int id)
        {
            var user = await context.AppUsers.FindAsync(id);
            if (user == null)
                return NotFound();
            return Ok(user);
        }
    }
}
