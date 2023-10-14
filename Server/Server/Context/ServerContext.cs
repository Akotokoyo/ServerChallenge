using Microsoft.EntityFrameworkCore;
using Server.Entities;
using Server.Entities.FriendEntities;
using ServerArchitecture.Entities.UserEntities;

namespace ServerArchitecture.Context
{
    public class ServerContext : DbContext
    {
        public ServerContext(){}

        // Entities        
        public DbSet<User>? Users { get; set; }
        public DbSet<UserItems> UserItems { get; set; }
        public DbSet<Friend> Friends { get; set; }
        public DbSet<FriendRequest> FriendRequests { get; set; }
    }
}
