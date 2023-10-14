using System.ComponentModel.DataAnnotations;

namespace ServerArchitecture.Entities.UserEntities
{
    public class UserItems
    {
        public int Id {  get; set; }
        public int ItemId { get; set; }
        public int ItemAmount { get; set; }

        public int UserId { get; set; }
        public User? User { get; set; }
    }
}
