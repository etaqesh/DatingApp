using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    public class UsersController : BaseApiController //the first thing we need to do is derive from the Controller Base Class
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        //api/users
        [HttpGet]//Get All Users 
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() // OR public ActionResult<List<AppUser>> GetUsers()
        {
            return await _context.Users.ToListAsync();
            //returns the list of users 
        }

        [Authorize]
        //api/users/3
        [HttpGet("{id}")]//Get Specific User By ID
        public ActionResult<AppUser> GetUser(int id)
        {
            return _context.Users.Find(id);
            //returns the user
        }
    }
}