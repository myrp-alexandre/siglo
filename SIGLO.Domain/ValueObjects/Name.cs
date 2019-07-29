using Flunt.Notifications;
using SIGLO.Shared;
using SIGLO.Shared.Messages;

namespace SIGLO.Domain.ValueObjects
{
    public class NameOrDescription : Notifiable
    {
        public NameOrDescription(string name)
        {
            Name = name;

            AddNotifications(new Flunt.Validations.Contract().Requires()
                .HasMaxLen(name, Constantes.LENGTH_MAX, "Name", Messages.LENGTH_60_MAX)
                .HasMinLen(name, Constantes.LENGTH_MIN, "Name", Messages.LENGTH_05_MIN));
        }

        public virtual string Name { get; protected set; }
        public override string ToString()
        {
            return Name;
        }
    }
}
