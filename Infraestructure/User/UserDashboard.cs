
namespace DomainModel.User
{
    public class UserDashboard : EntityBase
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string? MobilePhone { get; set; }
        public string? Address { get; set; }
        public string? City { get; set; }
        public string? Country { get; set; }
        public UserStatus? Status { get; set; }
        public int Age { get; set; }

        public override string ToString()
        {
            return $"ID: {this.Id} | Name: {this.Name} | Email: {this.Email}";
        }
    }

    public enum UserStatus
    {
        Unknown = 0,
        Active = 1,
        Inactive = 2,
    }
}
