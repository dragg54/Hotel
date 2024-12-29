using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel_Management_API.Services
{
    public interface ITokenService
    {
       string GenerateToken(string userId); 
    }
}