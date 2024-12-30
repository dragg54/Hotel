using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel_Management_API.DTOs.Requests;
using Hotel_Management_API.Responses;
using Hotel_Management_API.Services;
using Microsoft.AspNetCore.Mvc;

namespace Hotel_Management_API.Controllers
{
    [Route("api/users")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IResponseHandler _responseHandler;
        public UserController(IUserService userService, IResponseHandler responseHandler)
        {
            _userService = userService;
            _responseHandler = responseHandler;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterAsync([FromBody] PostUserRequest request)
        {
            var result = await _userService.ProcessPostUserRequest(request);

            if (!result.IsAuthenticated)
            {
                return _responseHandler.Unauthorized("User not authenticated");
            }
            return _responseHandler.Created(result, "User registration successful");
        }

        
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginUserRequest request)
        {
            var result = await _userService.ProcessLoginRequest(request);

            if (!result.IsAuthenticated)
            {
                return _responseHandler.Unauthorized("User not authenticated");
            }
            return _responseHandler.Success(result, "User logged in successfully");
        }
    }
}
