using SIGLO.Domain.ValueObjects;
using SIGLO.Shared.Entities;
using SIGLO.Shared.Enums;
using System;
using System.Collections.Generic;

namespace SIGLO.Domain.Entities
{
    public class Contract : Entity
    {
        public Contract() { }

        public Contract(NameOrDescription name, DateTime validateDate, long createdByUser, CompanyAndBranchOffice companyId, string machineNameOrIP)
        {
            Name = name;
            ValidateDate = validateDate;

            CreatedDateBy created = new CreatedDateBy(createdByUser);
            UpdatedDateBy updated = new UpdatedDateBy(null);
            CompanyAndBranchOffice companyAndBranchOffice = new CompanyAndBranchOffice(companyId.CompanyId, companyId.BranchOfficeId);
            Audit audit = new Audit(created, updated, companyAndBranchOffice, ERecordStatus.Active, machineNameOrIP);
            Audit = audit;

            AddNotifications(new Flunt.Validations.Contract().Requires()
                .IsNotNull(validateDate, "validateDate", "Data requerida")
                .IsLowerOrEqualsThan(validateDate, DateTime.Now, "ValidateDate", "Data da validade precisa ser maior que a data atua"),
                name);
        }
        public void Update(NameOrDescription name, DateTime validateDate, long? updatedByUser)
        {
            Name = name;
            ValidateDate = validateDate;

            Audit.Update(updatedByUser);

            AddNotifications(new Flunt.Validations.Contract().Requires()
                .IsNotNull(validateDate, "validateDate", "Data requerida")
                .IsLowerOrEqualsThan(validateDate, DateTime.Now, "ValidateDate", "Data da validade precisa ser maior que a data atua"),
                name);
        }

        public void Delete()
        {
            Audit.Delete();
        }

        public virtual NameOrDescription Name { get; private set; }
        public virtual DateTime ValidateDate { get; private set; }
        public virtual Audit Audit { get; private set; }
        public virtual IList<Call> Calls { get; private set; }
    }
}
