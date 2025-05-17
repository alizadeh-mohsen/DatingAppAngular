using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;

namespace API.Controllers
{
    public class AccountController(DatingContext context, ITokenService tokenService) : BaseController
    {

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username))
                return BadRequest("Username is taken");

            using var hmac = new HMACSHA512();

            var user = new DatingAppUser
            {
                UserName = registerDto.Username.ToLower(),
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            context.AppUsers.Add(user);
            await context.SaveChangesAsync();

            var userDto = tokenService.CreateToken(user);

            return Ok(userDto);
        }

        [HttpPost("login")]
        public async Task<ActionResult<DatingAppUser>> Login(LoginDto loginDto)
        {
            var user = await context.AppUsers.FirstOrDefaultAsync(x => x.UserName.ToLower() == loginDto.Username.ToLower());
            if (user == null)
                return Unauthorized("Username not found");

            using var hmac = new HMACSHA512(user.PasswordSalt);
            var computedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));
            if (!computedHash.SequenceEqual(user.PasswordHash))
                return Unauthorized("Invalid password");


            var userDto = tokenService.CreateToken(user);

            return Ok(userDto);
        }

        private async Task<bool> UserExists(string username)
        {
            return await context.AppUsers.AnyAsync(x => x.UserName.ToLower().Equals(username.ToLower()));
        }
    }
}
