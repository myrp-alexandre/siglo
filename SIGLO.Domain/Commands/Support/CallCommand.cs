using SIGLO.Shared.Commands;
using SIGLO.Shared.Enums;
using System;

namespace SIGLO.Domain.Commands
{
    public class CallCommandQuery
    {
        public long Id { get; set; }
        public long CallTypeId { get; set; }
        public string CallTypeName { get; set; }
        public long CallGroupId { get; set; }
        public string CallGroupName { get; set; }
        public long CustomerId { get; set; }
        public string CallCustomerName { get; set; }
        public long SectorId { get; set; }
        public string CallSectorName { get; set; }
        public long ContractId { get; set; }
        public string CallContractName { get; set; }
        public long EmployeeId { get; set; }
        public string CallEmployeeName { get; set; }
        public EPriority Priority { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int Status { get; set; }
        public long? FinishedBy { get; set; }
        public DateTime? FinishedDate { get; set; }
        public long? CanceledBy { get; set; }
        public DateTime? CanceledDate { get; set; }
        public long LastSequenceItem { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedByUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedByUserId { get; set; }
        public long CompanyId { get; set; }
        public long BranchOfficeId { get; set; }
        public ERecordStatus RecordStatus { get; set; }
        public string MachineNameOrIP { get; set; }
    }

    public class CallCommandResult
    {
        public long Id { get; set; }
        public long CallTypeId { get;  set; }
        public long CallGroupId { get;  set; }
        public long CustomerId { get;  set; }
        public long SectorId { get;  set; }
        public long ContractId { get;  set; }
        public long EmployeeId { get;  set; }
        public EPriority Priority { get;  set; }
        public string Title { get;  set; }
        public string Details { get;  set; }
        public int Status { get;  set; }
        public long? FinishedBy { get;  set; }
        public DateTime? FinishedDate { get;  set; }
        public long? CanceledBy { get;  set; }
        public DateTime? CanceledDate { get;  set; }
        public long LastSequenceItem { get;  set; }
        public DateTime CreatedDate { get;  set; }
        public long CreatedByUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedByUserId { get; set; }
        public long CompanyId { get;  set; }
        public long BranchOfficeId { get; set; }
        public ERecordStatus RecordStatus { get;  set; }
        public string MachineNameOrIP { get;  set; }
    }
    public class CreateCallCommand : ICommand
    {
        public long CallTypeId { get; set; }
        public long CallGroupId { get; set; }
        public long CustomerId { get; set; }
        public long SectorId { get; set; }
        public long ContractId { get; set; }
        public long EmployeeId { get; set; }
        public EPriority Priority { get; set; }
        public string Title { get; set; }
        public string Details { get; set; }
        public int Status { get; set; }
        public long LastSequenceItem { get; set; }
        public ERecordStatus RecordStatus { get; set; }
        public long CreatedByUserId { get; set; }
        public long CompanyId { get; set; }
        public long BranchOfficeId { get; set; }
        public string MachineNameOrIP { get; set; }
    }
    public class EditCallCommand : ICommand
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long UpdatedByUserId { get; set; }
        public long CallTypeId { get; set; }
        public long CallGroupId { get; set; }
        public long CustomerId { get; set; }
        public long SectorId { get; set; }
        public long ContractId { get; set; }
        public long EmployeeId { get; set; }
        public EPriority Priority { get; private set; }
        public string Details { get; private set; }
        public string MachineNameOrIP { get; private set; }
    }
    public class DeleteCallCommand : ICommand
    {
        public long Id { get; set; }
    }
}