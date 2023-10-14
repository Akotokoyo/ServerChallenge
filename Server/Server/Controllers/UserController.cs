using Microsoft.AspNetCore.Mvc;
using ServerArchitecture.Common;
using ServerArchitecture.Context;
using ServerArchitecture.DTO;
using ServerArchitecture.DTO.FromClient;
using ServerArchitecture.Entities.UserEntities;
using ServerArchitecture.Services;


namespace ServerArchitecture.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly ServerContext _context;
        private readonly UserService _userService;
        private readonly DtoConverter _dtoConverter;

        public UserController(ServerContext context)
        {
            _context = context;
            _userService = new UserService(context);
            _dtoConverter = new DtoConverter();
        }

        [HttpPost(Name = "RegisterUser")]
        public async Task<OkObjectResult> Register([FromBody] CreateUserFromClientDTO userDTO) {
            var user = _userService.CreateAsync(_dtoConverter.ConvertTo<User>(userDTO));
            return Ok(_dtoConverter.ConvertTo<CreateUserToClientDTO>(user));
        }

        [HttpGet(Name = "LoginUser")]
        public async Task<OkObjectResult> Login(int userId) {
            var user = _userService.GetAsync(userId);
            return Ok(_dtoConverter.ConvertTo<CreateUserToClientDTO>(user));
        }
    }
}
