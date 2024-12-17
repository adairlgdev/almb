using almb.Services;

namespace almb.Models
{
    public class User(Guid id, string userName, string password)
    {
        public Guid Id { get; set; } = id;
        public string UserName { get; private set; } = userName ?? string.Empty;
        public string Password { get; private set; } = password ?? string.Empty;

        public void EncryptPassword()
        {
            Password = HashService.Encrypt(Password);
        }
    }
}
