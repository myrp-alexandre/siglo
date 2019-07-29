using Flunt.Notifications;

namespace SIGLO.Shared.Entities
{
    public abstract class Entity : Notifiable
    {
        protected Entity() { }

        public virtual long Id { get; private set; }
    }
}
