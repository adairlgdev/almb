using almb.Models;
using Flunt.Notifications;
using Flunt.Validations;

namespace almb.ViewModels
{
    public class CreateImageViewModel : Notifiable<Notification>
    {
        public string Caption { get; set; }
        public string Url { get; set; }
        public int Sequence { get; private set; }

        public void SetSequence(int sequence)
        {
            Sequence = sequence;
        }

        public Image MapTo()
        {
            var contract = new Contract<Notification>()
                    .Requires()
                    .IsNotNull(Caption, "Informe a legenda da imagem")
                    .IsNotNull(Url, "Informe a url da imagem")
                    .IsGreaterThan(Sequence, 0, "Sequencia", "A sequencia deve ser maior que 0");

            AddNotifications(contract);

            return new Image(Guid.NewGuid(), Caption, Url, Sequence);
        }
    }
}
