﻿using Flunt.Validations;
using SIGLO.Domain.ValueObjects;
using SIGLO.Shared.Entities;
using SIGLO.Shared.Enums;

namespace SIGLO.Domain.Entities
{
    public class PatrimonyColor : Entity, IValidatable
    {
        public PatrimonyColor() { }

        public PatrimonyColor(NameOrDescription name, long createdByUser, CompanyAndBranchOffice companyId, string machineNameOrIP)
        {
            Name = name;

            CreatedDateBy created = new CreatedDateBy(createdByUser);
            UpdatedDateBy updated = new UpdatedDateBy(null);
            CompanyAndBranchOffice companyAndBranchOffice = new CompanyAndBranchOffice(companyId.CompanyId, companyId.BranchOfficeId);
            Audit audit = new Audit(created, updated, companyAndBranchOffice, ERecordStatus.Active, machineNameOrIP);
            Audit = audit;

            Validate();
        }
        public void Update(NameOrDescription name, long? updatedByUser)
        {
            Name = name;

            Audit.Update(updatedByUser);
            
            Validate();
        }
        public void Validate()
        {
            AddNotifications(Name);
        }
        public void Delete()
        {
            Audit.Delete();
        }

        public virtual NameOrDescription Name { get; private set; }
        public virtual Audit Audit { get; private set; }
    }
}
