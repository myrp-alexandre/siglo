using Flunt.Validations;
using SIGLO.Domain.ValueObjects;
using SIGLO.Shared.Entities;
using SIGLO.Shared.Enums;
using System;

namespace SIGLO.Domain.Entities
{
    public class Call : Entity, IValidatable

    {
        public Call() { }

        public Call(long callTypeId,
                    long callGroupId,
                    long sectorId,
                    long agreementId,
                    long customerId,
                    long employeeId,
                    EPriority priority,
                    NameOrDescription title,
                    string details,
                    long createdByUser,
                    CompanyAndBranchOffice companyId,
                    string machineNameOrIP)
        {
            CallTypeId = callTypeId;
            CallGroupId = callGroupId;
            SectorId = sectorId;
            ContractId = agreementId;
            CustomerId = customerId;
            EmployeeId = employeeId;
            Priority = priority;
            Title = title;
            Details = details;
            Status = 0;

            CreatedDateBy created = new CreatedDateBy(createdByUser);
            UpdatedDateBy updated = new UpdatedDateBy(null);
            CompanyAndBranchOffice companyAndBranchOffice = new CompanyAndBranchOffice(companyId.CompanyId, companyId.BranchOfficeId);
            Audit audit = new Audit(created, updated, companyAndBranchOffice, ERecordStatus.Active, machineNameOrIP);
            Audit = audit;

            Validate();
        }
        public void Canceling(int canceledBy)
        {
            CanceledByUserId = canceledBy;
            CanceledDate = DateTime.Now;
        }

        public void Finishing(int finishedBy)
        {
            FinishedByUserId = finishedBy;
            FinishedDate = DateTime.Now;
        }

        public void Update(long callTypeId,
                    long callGroupId,
                    long sectorId,
                    long agreementId,
                    long customerId,
                    long employeeId,
                    EPriority priority,
                    NameOrDescription title,
                    string details,
                    long updatedByUserId)
        {
            CallTypeId = callTypeId;
            CallGroupId = callGroupId;
            SectorId = sectorId;
            ContractId = agreementId;
            CustomerId = customerId;
            EmployeeId = employeeId;
            Priority = priority;
            Details = details;

            Title = title;
            Audit.Update(updatedByUserId);
            Validate();
        }

        public void Validate()
        {
            AddNotifications(Title);
        }
        public void Delete()
        {
            Audit.Delete();
        }

        public virtual long ContractId { get; private set; }
        public virtual Contract Contract { get; private set; }
        public virtual long CallTypeId { get; private set; }
        public virtual CallType CallType { get; private set; }
        public virtual long CallGroupId { get; private set; }
        public virtual CallGroup CallGroup { get; private set; }
        public virtual long CustomerId { get; private set; }
        public virtual Account Customer { get; private set; }
        public virtual long EmployeeId { get; private set; }
        public virtual Account Employee { get; private set; }
        public virtual long SectorId { get; private set; }
        public virtual Sector Sector { get; private set; }
        public virtual EPriority Priority { get; private set; }
        public virtual NameOrDescription Title { get; private set; }
        public virtual string Details { get; private set; }
        public virtual int Status { get; private set; }
        public virtual long? FinishedByUserId { get; private set; }
        public virtual DateTime? FinishedDate { get; private set; }
        public virtual long? CanceledByUserId { get; private set; }
        public virtual DateTime? CanceledDate { get; private set; }
        public virtual long LastSequenceItem { get; private set; }
        public virtual Audit Audit { get; protected set; }
    }
}