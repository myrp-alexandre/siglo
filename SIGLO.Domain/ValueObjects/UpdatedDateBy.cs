using Flunt.Notifications;
using SIGLO.Shared.Messages;
using System;

namespace SIGLO.Domain.ValueObjects
{
    public class UpdatedDateBy : Notifiable
    {
        public UpdatedDateBy(long? updatedByUserId)
        {
            AddNotifications(new Flunt.Validations.Contract().Requires()
                .IsFalse(updatedByUserId <= 0, "updatedByUserId", Messages.CODE_REQUIRED));
            if (updatedByUserId != null) {
                UpdatedDate = DateTime.Now;
                UpdatedByUserId = updatedByUserId;
            }
        }

        public virtual DateTime? UpdatedDate { get; protected set; }
        public virtual long? UpdatedByUserId { get; protected set; }
    }
}
