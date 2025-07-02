using Microsoft.AspNetCore.Mvc;
using users_service.Database.Entities;
using users_service.Resources.Users.Classes;
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
        public IActionResult GetByCredential([FromBody] CredentialsDTO credentialsDTO)
        {           
            return Ok(_userService.GetByCredentials(credentialsDTO.Credential, credentialsDTO.Password));
        }

        [HttpPost]
        [Route("new")]
        public IActionResult AddNew([FromBody] User newUser)
        {                      
            return Ok(_userService.CreateUser(newUser));
        }

        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteById(int id)
        {
            _userService.DeleteUser(id);
            return Ok("User deleted successfully");
        }
    }
}
