using System.ComponentModel.DataAnnotations;

namespace ServerArchitecture.DTO.FromClient
{
    public class CreateUserFromClientDTO
    {
        [Required]
        public string Username { get; set; }

        public string? Email { get; set; }

        [Required]
        public string LangId { get; set; }

    }
}



