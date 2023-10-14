using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Configurations;
using ServerArchitecture.Common;
using ServerArchitecture.Context;
using ServerArchitecture.DTO;
using ServerArchitecture.Entities.UserEntities;
using ServerArchitecture.Services;
using System.Numerics;

namespace ServerArchitecture.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FriendListController : Controller
    {
        private readonly ServerContext _context;
        private readonly DtoConverter _dtoConverter;
        private readonly FriendService _friendService;

        public FriendListController(ServerContext context)
        {
            _context = context;
            _dtoConverter = new DtoConverter();
            _friendService = new FriendService(_context);
        }

        [HttpGet (Name = "GetFriendList")]
        public async Task<FriendListToClientDTO> GetFriendList(int userId) {
            return await _friendService.GetFriendList(userId);
        }

        [HttpGet]
        [Route("GetFriendRequests/{userId}")]
        public async Task<OkObjectResult> GetFriendRequests(int userId) {
            var requests = await _friendService.GetFriendRequests(userId);
            return Ok(_dtoConverter.ConvertTo<FriendRequestToClientDTO>(requests));
        }
        
        [HttpPost]
        [Route("SendFriendRequest/{senderId}/{receiverId}")]
        public async void SendFriendRequest(int senderId, int receiverId) {
            await _friendService.SendFriendRequest(senderId, receiverId);
        }

        [HttpPost]
        [Route("AcceptFriendRequest/{senderId}/{receiverId}")]
        public async void AcceptFriendRequest(int senderId, int receiverId) {
            await _friendService.AcceptFriendRequest(senderId, receiverId);
        }
    }
}
