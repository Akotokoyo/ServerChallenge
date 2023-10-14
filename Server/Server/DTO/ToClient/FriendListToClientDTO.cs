using System.ComponentModel.DataAnnotations;

namespace ServerArchitecture.DTO
{
    public class FriendListToClientDTO
    {
        public int UserId { get; set; }

        public ICollection<int>? FriendList { get; set; }

    }
}



