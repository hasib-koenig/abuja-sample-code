namespace Server.Models
{
    public class UserRole
    {
        public int UserId {  get; set; }
        public User User { get; set; } = new User();
        public int RoleId { get; set; }

        public Role Role { get; set; } = new Role();
    }
}
