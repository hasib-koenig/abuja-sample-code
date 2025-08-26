using Server.Models;

namespace Server.DTOs.User
{
    public class UserRequestDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string UserEmail { get; set; } = string.Empty;

        public string UserPassword { get; set; } = string.Empty;

        public string RoleName { get; set; } = string.Empty;
    }
}
