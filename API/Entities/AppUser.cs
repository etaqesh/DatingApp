using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Entities
{
    public class AppUser
    {
        // private int _Id; => property full example
        // public int Id
        // {
        //     get { return _Id; }
        //     set { _Id = value; }
        // }
        public int Id { get; set; }

        public string UserName { get; set; }

        public byte[] PasswordHash { get; set; } //Get returned when we calculate the hash

        public byte[] PasswordSalt { get; set; }
    }
}