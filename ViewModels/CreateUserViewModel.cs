using almb.Models;
using Flunt.Notifications;
using Flunt.Validations;

namespace almb.ViewModels
{
    public class CreateUserViewModel : Notifiable<Notification>
    {
        public string UserName { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;

        public User MapTo()
        {
            var contract = new Contract<Notification>()
            .Requires()
                 .IsNotEmpty(UserName, "Informe nome do usuário")
                 .IsNotEmpty(Password, "Informe a senha do usuário");

            AddNotifications(contract);

            return new User(Guid.NewGuid(), UserName, Password);
        }
    }
}
