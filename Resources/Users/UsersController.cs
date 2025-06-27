using Microsoft.AspNetCore.Mvc;
using users_service.Database.Entities;
using users_service.Resources.Users.Interfaces;

namespace users_service.Resources.Users
{
    [Route("users")]
    public class UsersController : Controller
    {
        private IUserService _userService;

        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpGet]
        [Route("all-user")]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetUsers());
        }

        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_userService.GetById(id));
        }

        [HttpGet]
        [Route("login")]
        public IActionResult GetByCredential(string credential, string password)
        {
            return Ok(_userService.GetByCredentials(credential, password));
        }

        [HttpPost]
        [Route("new")]
        public IActionResult AddNew([FromBody] User newUser)
        {
            return Ok(_userService.CreateUser(newUser));
        }
    }
}
