using System.ComponentModel.DataAnnotations;

namespace ServerArchitecture.DTO
{
    public class FriendRequestToClientDTO
    {
        public int Id { get; set; }
        public int SenderId { get; set; }
        public int ReceiverId { get; set; }
        public DateTime Timestamp { get; set; }

    }
}



