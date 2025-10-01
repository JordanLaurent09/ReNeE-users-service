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
        private IUsersAlbumsService _usersAlbumsService;
        private IUsersSongsService _usersSongsService;
        private IPhotoService _photoService;

        public UsersController(
            IUserService userService, 
            IUsersPerformersService usersPerformersService, 
            IUsersAlbumsService usersAlbumsService, 
            IUsersSongsService usersSongsService,
            IPhotoService photoService)
        {
            _userService = userService;
            _usersPerformersService = usersPerformersService;
            _usersAlbumsService = usersAlbumsService;
            _usersSongsService = usersSongsService;
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

        ///<summary>
        /// Add performer to user's favorites
        ///</summary>
        ///<param name="entity"></param>
        ///<returns></returns>
        ///<remarks>
        /// 
        /// {
        ///     "userId": 1,
        ///     "performerId": 1
        /// }
        /// 
        ///</remarks>
        ///<response code="200">Returns success message</response>
        ///<response code="400">If uncorrect request occurs</response>      
        ///<response code="500">If server error occurs</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("performer")]
        public IActionResult AddFavoritePerformer([FromBody] UsersPerformers entity)
        {
            try
            {
                string result = _usersPerformersService.CreateNew(entity);
                if (result == "OK")
                {
                    return Created(result, StatusCodes.Status201Created);
                }
                else 
                {
                    return BadRequest("Error");
                }
            }
            catch (Exception ex)
            {
                return BadRequest(StatusCodes.Status500InternalServerError);
            }
        }


        /// <summary>
        /// Get indexes of user's favorite performers
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <response code="200">Returns indexes array</response>
        /// <response code="400">If bad response occurs</response>
        /// <response code="404">If values didn't find</response>
        /// <response code="500">If server error occurs</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("performers/{userId:int}")]
        public IActionResult GetPerfIndexes(int userId)
        {
            IEnumerable<int> indexes = _usersPerformersService.GetUserPerformersId(userId);

            return Ok(indexes);
        }

        /// <summary>
        /// Removes favorite user's performer
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="performerId"></param>
        /// <returns></returns>
        /// <response code="200">Returns success message</response>
        /// <response code="400">If bad response occurs</response>
        /// <response code="404">If favorite performer didn't find</response>
        /// <response code="500">If server error occurs</response>
        /// 
        [HttpDelete] 
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("deletePerformer")]
        public IActionResult DeleteFavoritePerformer([FromQuery] int userId, [FromQuery] int performerId)
        {
            _usersPerformersService.DeleteByIds(userId, performerId);

            _photoService.DeleteByIds(userId, performerId);

            _usersAlbumsService.DeleteByIds(userId, performerId);

            return Ok("Performer and all info successfully removed");
        }



        /// <summary>
        /// Return's array with user's photos
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="performerId"></param>
        /// <returns></returns>
        /// <response code="200">Returns photo array</response>
        /// <response code="400">If bad response occurs</response>
        /// <response code="404">If photos didn't find</response>
        /// <response code="500">If server error occurs</response>
        [HttpGet]       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("photos")]
        public IActionResult GetPhotos([FromQuery] int userId, [FromQuery] int performerId)
        {                       
            IEnumerable<Photo> photos = _photoService.GetPerformerPhotos(userId, performerId);

            return Ok(photos);
        }


        /// <summary>
        /// Get ALL photos in table (only admin use)
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Returns photo array or empty array</response>
        /// <response code="400">If bad response occurs</response>
        /// <response code="500">If server error occurs</response>
        [HttpGet]       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("allPhotos")]
        public IActionResult GetAllPhotos()
        {
            IEnumerable<Photo> photos = _photoService.GetAll();

            return Ok(photos);
        }


        /// <summary>
        /// Add new user's photo of favorite performer
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        ///<remarks>
        ///
        /// {
        ///     performerId: 1,
        ///     userId: 1,
        ///     image: "text view of picture file"
        /// }
        /// 
        ///</remarks> 
        ///
        /// <response code="200"> Returns success message</response>
        /// <response code="400">If bad response occurs</response>
        /// <response code="500">If server error occurs</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("newPhoto")]
        public IActionResult AddPhoto([FromBody] Photo entity)
        {           

            string result = _photoService.CreateNew(entity);

            return Created(result, entity);
        }


        /// <summary>
        /// Removes chosen photo
        /// </summary>
        /// <param name="photoId"></param>
        /// <returns></returns>
        /// <response code="200"> Returns success message</response>
        /// <response code="400">If bad response occurs</response>
        /// <response code="404">If chosen photo didn't find</response>
        /// <response code="500">If server error occurs</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("deletePhoto/{photoId:int}")]
        public IActionResult DeletePhoto(int photoId)
        {
            _photoService.Delete(photoId);

            return Ok("Photo has been deleted");
        }



        /// <summary>
        /// Get user's favorite albums indexes
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="performerId"></param>
        /// <returns></returns>
        /// <response code="200"> Returns array of albums indexes</response>
        /// <response code="400">If bad response occurs</response>
        /// <response code="404">If chosen photo didn't find</response>
        /// <response code="500">If server error occurs</response>
        [HttpGet]       
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("albums")]
        public IActionResult GetAlbums([FromQuery] int userId, [FromQuery] int performerId)
        {
            IEnumerable<int> albums = _usersAlbumsService.GetAlbumsIds(userId, performerId);

            return Ok(albums);
        }


        /// <summary>
        /// Add album to user's favorites
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        /// <remarks>
        /// 
        /// {
        ///     albumId: 1,
        ///     userId: 1,
        ///     performerId: 1
        ///     
        /// }
        ///
        /// </remarks>
        /// <response code="200">Returns success message</response>
        /// <response code="400">If uncorrect request occurs</response>      
        /// <response code="500">If server error occurs</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("newAlbum")]
        public IActionResult AddAlbum([FromBody] UsersAlbums entity)
        {
            string result = _usersAlbumsService.CreateNew(entity);

            return Created(result, entity);
        }


        /// <summary>
        /// Removes user's favorite album
        /// </summary>
        /// <param name="albumId"></param>
        /// <param name="userId"></param>
        /// <returns></returns>
        /// <response code="200"> Returns success message</response>
        /// <response code="400">If bad response occurs</response>
        /// <response code="404">If chosen album didn't find</response>
        /// <response code="500">If server error occurs</response>
        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [Route("deleteFavoriteAlbum")]
        public IActionResult DeleteFavoriteAlbum([FromQuery] int albumId, [FromQuery] int userId)
        {
            _usersAlbumsService.DeleteByAlbumId(albumId, userId);

            return Ok("Album successfully deleted");
        }

        /// <summary>
        /// DEPRECATED METHOD
        /// </summary>
        /// <param name="albumId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("deleteAlbum/{albumId:int}")]
        public IActionResult DeleteAlbum(int albumId)
        {
            _usersAlbumsService.Delete(albumId);

            return Ok("Album successfully deleted");
        }


        // DANGER ZONE
        
        [HttpGet]
        [Route("songs")]
        public IActionResult GetSongs([FromQuery] int userId, [FromQuery] int performerId)
        {
            IEnumerable<int> songs = _usersSongsService.GetSongsIds(userId, performerId);

            return Ok(songs);
        }

        [HttpPost]
        [Route("newSong")]
        public IActionResult AddSong([FromBody] UsersSongs entity)
        {
            string result = _usersSongsService.CreateNew(entity);

            return Created(result, entity); 
        }
    }
}
