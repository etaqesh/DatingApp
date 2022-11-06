using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    //the first thing we need to do is derive from the Controller Base Class
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        private readonly DataContext _context;
        public UsersController(DataContext context)
        {
            _context = context;
        }

        //api/users
        [HttpGet]//Get All Users 
        public async Task<ActionResult<IEnumerable<AppUser>>> GetUsers() // OR public ActionResult<List<AppUser>> GetUsers()
        {
            return await _context.Users.ToListAsync();
            //returns the list of users 
        }

        //api/users/3
        [HttpGet("{id}")]//Get Specific User By ID
        public ActionResult<AppUser> GetUser(int id)
        {
            return _context.Users.Find(id);
            //returns the user
        }
    }
}