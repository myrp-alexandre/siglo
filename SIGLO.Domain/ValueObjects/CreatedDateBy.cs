using Flunt.Notifications;
using SIGLO.Shared.Messages;
using System;

namespace SIGLO.Domain.ValueObjects
{
    public class CreatedDateBy : Notifiable
    {
        public CreatedDateBy(long createdByUserId)
        {
            CreatedDate = DateTime.Now;
            AddNotifications(new Flunt.Validations.Contract().Requires()
                .IsFalse(createdByUserId <= 0, "creadtedByUserId", Messages.CODE_REQUIRED));
            CreatedByUserId = createdByUserId;
        }
        public virtual DateTime CreatedDate { get; protected set; }
        public virtual long CreatedByUserId { get; protected set; }
    }
}
