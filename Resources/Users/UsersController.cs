using Microsoft.AspNetCore.Identity;
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

        /// <summary>
        /// Gets all existing users
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("all-user")]
        public IActionResult GetAll()
        {
            return Ok(_userService.GetUsers());
        }

        /// <summary>
        /// Gets specific user by it's id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            return Ok(_userService.GetById(id));
        }

        /// <summary>
        /// Authenticates specific user
        /// </summary>
        /// <param name="credentialsDTO"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("login")]
        public IActionResult GetByCredential([FromBody] CredentialsDTO credentialsDTO)
        {           
            return Ok(_userService.GetByCredentials(credentialsDTO.Credential, credentialsDTO.Password));
        }


        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="newUser"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("new")]
        public IActionResult AddNew([FromBody] User newUser)
        {                      
            return Ok(_userService.CreateUser(newUser));
        }


        /// <summary>
        /// Deletes specific user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id:int}")]
        public IActionResult DeleteById(int id)
        {
            _userService.DeleteUser(id);
            return Ok("User deleted successfully");
        }


        /// <summary>
        /// Changes specific user
        /// </summary>
        /// <param name="changedEntity"></param>
        /// <returns></returns>
        [HttpPatch]
        [Route("change-info")]
        public IActionResult Update([FromBody] User changedEntity)
        {
            _userService.UpdateUser(changedEntity);
            return Ok("User has been updated successfully");
        }

    }
}
