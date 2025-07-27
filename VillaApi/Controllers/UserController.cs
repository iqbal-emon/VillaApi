using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using VillaApi.Model.Dtos.LoginDto;
using VillaApi.Repositories.IRepositories;
using VillaApi.Utility;

namespace VillaApi.Controllers
{
    [Route("api/Auth")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpPost("Login")]
        public ActionResult<ApiResponse> Login([FromBody] LoginRequestDto userLogin)
        {
            if (userLogin == null || string.IsNullOrEmpty(userLogin.Name) || string.IsNullOrEmpty(userLogin.Password))
            {
                return BadRequest("Invalid login credentials.");
            }
          var  user= _userRepository.Login(userLogin);
            if(user.Result.Data!=null)
            {
                return Ok(user.Result);
            }
            else
            {
                return user.Result;
            }




        }
    }
}
