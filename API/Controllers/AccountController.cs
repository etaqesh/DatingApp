using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Data;
using API.DTOs;
using API.Entities;
using API.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class AccountController : BaseApiController
    {
        private readonly DataContext _context;
        private readonly ITokenService _tokenService;
        public AccountController(DataContext context,ITokenService tokenService)
        {
            _tokenService = tokenService;
            _context = context;
        }

        //register : name of method
        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(RegisterDto registerDto)
        {
            if (await UserExists(registerDto.Username.ToLower()))
                return BadRequest("Username Is Taken");

            using var hmac = new HMACSHA512();

            var user = new AppUser
            {
                UserName = registerDto.Username,
                PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(registerDto.Password)),
                PasswordSalt = hmac.Key
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateService(user)
            };
        }

        [HttpPost("login")]
        public async Task<ActionResult<UserDto>> Login(LoginDto loginDto)
        {
            var user = await _context.Users.SingleOrDefaultAsync(x => x.UserName.ToLower() == loginDto.Username.ToLower());

            if (user == null) return Unauthorized("Invalid Username");

            using var hmac = new HMACSHA512(user.PasswordSalt);//PasswordSalt == Key
            var copmutedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(loginDto.Password));

            for (int i = 0; i < copmutedHash.Length; i++)
            {
                if (copmutedHash[i] != user.PasswordHash[i]) return Unauthorized("Invalid Password");
            }
            
            return new UserDto
            {
                Username = user.UserName,
                Token = _tokenService.CreateService(user)
            };
        }
        private async Task<bool> UserExists(string username)
        {
            return await _context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());
        }
        #region Old
        //         public async Task<ActionResult<AppUser>> Register(string username, string password)
        //         {
        //             using var hmac = new HMACSHA512();
        // 
        //             var user = new AppUser
        //             {
        //                 UserName = username,
        //                 PasswordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password)),
        //                 PasswordSalt = hmac.Key
        //             };
        // 
        //             _context.Users.Add(user);
        //             await _context.SaveChangesAsync();//Here we insert to the DB
        // 
        //             return user;
        //         }
        #endregion

    }
}