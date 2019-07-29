using SIGLO.Shared.Commands;
using System;

namespace SIGLO.Domain.Commands
{
    public class PatrimonyFunctionCommand
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedByUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedByUserId { get; set; }
        public long? CompanyId { get; set; }
        public long? BranchOfficeId { get; set; }
        public string MachineNameOrIP { get; set; }
    }
    public class CreatePatrimonyFunctionCommand : ICommand
    {
        public string Name { get; set; }
        public long CreatedByUserId { get; set; }
        public long CompanyId { get; set; }
        public long BranchOfficeId { get; set; }
        public string MachineNameOrIP { get; set; }
    }
    public class EditPatrimonyFunctionCommand : ICommand
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long UpdatedByUserId { get; set; }
    }
    public class DeletePatrimonyFunctionCommand : ICommand
    {
        public long Id { get; set; }
    }
}
