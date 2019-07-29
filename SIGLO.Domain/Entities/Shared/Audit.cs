using Flunt.Notifications;
using Flunt.Validations;
using SIGLO.Domain.ValueObjects;
using SIGLO.Shared.Enums;

namespace SIGLO.Domain.Entities
{
    public class Audit : Notifiable, IValidatable
    {
        public Audit() { }

        public Audit(
            CreatedDateBy createdDateBy,
            UpdatedDateBy updatedDateBy,
            CompanyAndBranchOffice companyAndBranchOffice,
            ERecordStatus recordStatus,
            string machineNameOrIP)
        {
            CreatedDateBy = createdDateBy;
            UpdatedDateBy = updatedDateBy;
            CompanyAndBranchOffice = companyAndBranchOffice;
            RecordStatus = recordStatus;
            MachineNameOrIP = machineNameOrIP;
        }

        public void Update(long? updatedByUser)
        {
            UpdatedDateBy updated = new UpdatedDateBy(updatedByUser);
            UpdatedDateBy = updated;

            AddNotifications(UpdatedDateBy);
            Validate();
        }

        public void Delete()
        {
            RecordStatus = ERecordStatus.Deleted;
        }

        public void Validate()
        {
            AddNotifications(CreatedDateBy, CompanyAndBranchOffice);
        }

        public virtual CreatedDateBy CreatedDateBy { get; private set; }
        public virtual UpdatedDateBy UpdatedDateBy { get; private set; }
        public virtual CompanyAndBranchOffice CompanyAndBranchOffice { get; private set; }
        public virtual ERecordStatus RecordStatus { get; private set; }
        public virtual string MachineNameOrIP { get; private set; }
    }
}
