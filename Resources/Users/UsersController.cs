using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using users_service.Database.Entities;
using users_service.Resources.Users.Classes;
using users_service.Resources.Users.Interfaces;

namespace users_service.Resources.Users
{
    [ApiController]
    [Route("users")]
    [Produces("application/json")]
    public class UsersController : Controller
    {
        private IUserService _userService;
        private IUsersPerformersService _usersPerformersService;
        private IPhotoService _photoService;

        public UsersController(IUserService userService, IUsersPerformersService usersPerformersService, IPhotoService photoService)
        {
            _userService = userService;
            _usersPerformersService = usersPerformersService;
            _photoService = photoService;
        }

        /// <summary>
        /// Gets all existing users
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns all users</response>
        /// <response code="500">If server error occurs</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// <response code="200">Returns specific user by id</response>
        /// <response code="400">If param is absent</response>
        /// <response code="404">If specific user not found</response>
        /// <response code="500">If server error occurs</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{id:int}")]
        public IActionResult GetById(int id)
        {
            User user = _userService.GetById(id);

            if (user.Id != 0)
            {
                return Ok(_userService.GetById(id));
            }
            else return NotFound($"User with id={id} has not found");

        }

        /// <summary>
        /// Authenticates specific user
        /// </summary>
        /// <param name="credentialsDTO"></param>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// {
        ///     "credential": "test-mail@mail.test",
        ///     "password": "secret_word"
        /// }
        /// 
        /// 
        /// </remarks>
        /// <response code="200">The specific user has been authenticated</response>
        /// <response code="400">If entity is null</response>
        /// <response code="404">If specific user not found</response>
        /// <response code="500">If server error occurs</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
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
        /// <remarks>
        /// 
        ///  {
        ///     "login": "fred21",
        ///     "firstname": "Fred",
        ///     "lastname": "Johnson",
        ///     "email": "freddie22@mail.com",
        ///     "sex": "MALE",
        ///     "password": "iamfreddie"
        ///  }
        /// 
        /// </remarks>
        /// <response code="201">Returns newly created user</response>
        /// <response code="400">If entity is null</response>
        /// <response code="500">If server error occurs</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("new")]
        public IActionResult AddNew([FromBody] User newUser)
        {
            if (newUser == null)
            {
                return BadRequest("User's data hasn't provided");
            }
            string result = _userService.CreateUser(newUser);
            if (result != "Пользователь успешно зарегистрирован")
            {
                return Ok(result);
            }
            else
            {
                return Created("", newUser);
            }
        }


        /// <summary>
        /// Deletes specific user
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <response code="200">Deletes specific user by id</response>
        /// <response code="400">If param is absence</response>
        /// <response code="404">If user not found</response>
        /// <response code="500">If server error occurs</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("{id:int}")]
        public IActionResult DeleteById(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok("User deleted successfully");
            }
            catch (Exception ex)
            {
                return BadRequest($"Exception occured: {ex.StackTrace}");
            }

        }


        /// <summary>
        /// Changes specific user
        /// </summary>
        /// <param name="changedEntity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// {
        ///     "id": 10,
        ///     "login": "fred21",
        ///     "firstname": "Fred",
        ///     "lastname": "Johnson",
        ///     "email": "freddie22@mail.com",
        ///     "sex": "MALE",
        ///     "password": "iamfreddie"
        ///  }
        /// 
        /// </remarks>
        /// <response code="200">Returns updated user</response>
        /// <response code="400">If entity is null</response>
        /// <response code="404">If user not found</response>
        /// <response code="500">If server error occurs</response>
        [HttpPatch]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("change-info")]
        public IActionResult Update([FromBody] User changedEntity)
        {
            if (changedEntity == null)
            {
                return BadRequest("User's data hasn't provided");
            }
            try
            {
                _userService.UpdateUser(changedEntity);
                return Ok("User has been updated successfully");
            }
            catch
            {
                return NotFound("User has not found");
            }

        }

        // Routes for performers of every user

        [HttpPost]
        [Route("performer")]
        public IActionResult AddFavoritePerformer([FromBody] UsersPerformers entity)
        {
            string result = _usersPerformersService.CreateNew(entity);

            return Created(result, entity);
        }

        [HttpGet]
        [Route("performers/{userId:int}")]
        public IActionResult GetPerfIndexes(int userId)
        {
            IEnumerable<int> indexes = _usersPerformersService.GetUserPerformersId(userId);

            return Ok(indexes);
        }


        // Routes for user's photos

        [HttpGet]
        [Route("photos")]
        public IActionResult GetPhotos([FromQuery] int userId, [FromQuery] int performerId)
        {                       
            IEnumerable<string> photos = _photoService.GetPerformerPhotos(userId, performerId);

            return Ok(photos);
        }

        [HttpPost]
        [Route("newPhoto")]
        public IActionResult AddPhoto([FromBody] Photo entity)
        {           

            string result = _photoService.CreateNew(entity);

            return Created("", entity);
        }
    }
}
