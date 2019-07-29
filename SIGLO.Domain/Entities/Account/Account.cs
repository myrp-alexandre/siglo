using Flunt.Validations;
using SIGLO.Domain.ValueObjects;
using SIGLO.Shared.Entities;
using SIGLO.Shared.Enums;
using System.Collections.Generic;

namespace SIGLO.Domain.Entities
{
    public class Account : Entity, IValidatable
    {
        public Account() { }

        public Account(NameOrDescription name, long createdByUser, CompanyAndBranchOffice companyId, string machineNameOrIP)
        {
            Name = name;

            CreatedDateBy created = new CreatedDateBy(createdByUser);
            UpdatedDateBy updated = new UpdatedDateBy(null);
            CompanyAndBranchOffice companyAndBranchOffice = new CompanyAndBranchOffice(companyId.CompanyId, companyId.BranchOfficeId);
            Audit audit = new Audit(created, updated, companyAndBranchOffice, ERecordStatus.Active, machineNameOrIP);
            Audit = audit;

            Validate();
        }
        public virtual void Update(NameOrDescription name, long? updatedByUser)
        {
            Name = name;

            Audit.Update(updatedByUser);

            Validate();
        }
        public virtual void Validate()
        {
            AddNotifications(Name);
        }
        public virtual void Delete()
        {
            Audit.Delete();
        }

        public virtual NameOrDescription Name { get; protected set; }
        public virtual Audit Audit { get; protected set; }
        public virtual IList<Call> CallsCustomers { get; protected set; }
        public virtual IList<Call> CallsEmployees { get; protected set; }
    }
}
