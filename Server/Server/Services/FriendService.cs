using Microsoft.EntityFrameworkCore;
using Server.Configurations;
using Server.Entities;
using Server.Entities.FriendEntities;
using ServerArchitecture.Context;
using ServerArchitecture.DTO;
using ServerArchitecture.Entities.UserEntities;
using ServerArchitecture.Exceptions;
using System.Collections.Generic;
using System.Linq;

namespace ServerArchitecture.Services
{
    public class FriendService
    {
        private readonly ServerContext _context;

        public FriendService(ServerContext context)
        {
            _context = context;
        }

        #region Get Method
        public virtual async Task<FriendListToClientDTO> GetFriendList(int userId)
        {
            ICollection<Friend> friends = await _context.Friends.Where(f => f.User1Id == userId || f.User2Id == userId).ToListAsync();            
            if (friends == null) { throw new EntityNotFoundException(typeof(Friend), userId); }

            ICollection<int>? friendList = null;
            foreach (Friend friendLink in friends)
            {
                if(friendLink.User1Id == userId)
                {
                    friendList.Append(friendLink.User2Id);
                }
                else {
                    friendList.Append(friendLink.User1Id);
                }
            }

            FriendListToClientDTO friendListToClientDTO = new FriendListToClientDTO { UserId = userId, FriendList = friendList };

            return friendListToClientDTO;
        }

        public virtual async Task<ICollection<FriendRequest>> GetFriendRequests(int userId)
        {
            return await _context.FriendRequests.Where(fr => fr.ReceiverId == userId).ToListAsync(); ;
        }

        #endregion

        #region Handle Methods
        public virtual async Task SendFriendRequest(int senderId, int receiverId)
        {
            await _context.FriendRequests.AddAsync(new FriendRequest { ReceiverId = receiverId, SenderId = senderId, Timestamp = DateTime.Now });
        }
        #endregion

        public virtual async Task AcceptFriendRequest(int senderId, int receiverId) {
            await _context.Friends.AddAsync(new Friend{ User1Id = senderId, User2Id = receiverId});
        }

    }
}
