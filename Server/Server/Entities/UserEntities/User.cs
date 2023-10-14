using System.ComponentModel.DataAnnotations.Schema;

namespace ServerArchitecture.Entities.UserEntities
{
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int Level { get; set; }

        public string? Email { get; set; }

        public string Language { get; set; }

        public ICollection<UserItems> UserItems { get; set; }
    }
}
