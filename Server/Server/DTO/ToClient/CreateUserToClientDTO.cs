using System.ComponentModel.DataAnnotations;

namespace ServerArchitecture.DTO
{
    public class CreateUserToClientDTO
    {
        public string Username { get; set; }

        public string? Email { get; set; }

        public string LangId { get; set; }

    }
}



