using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Entities;

namespace API.Interfaces
{
    public interface ITokenService
    {
        //what are we going to return from this : string
        string CreateService(AppUser user);
    }
}