using SIGLO.Shared.Commands;
using System;

namespace SIGLO.Domain.Commands
{
    public class AccountCommandResult
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedByUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedByUserId { get; set; }
        public long? CompanyId { get; set; }
        public long? BranchOfficeId { get; set; }
    }
    public class CreateAccountCommand : ICommand
    {
        public string Name { get; set; }
        public long CreatedByUserId { get; set; }
        public long CompanyId { get; set; }
        public long BranchOfficeId { get; set; }
        public string MachineNameOrIP { get; set; }
    }
    public class DeleteAccountCommand : ICommand
    {
        public long Id { get; set; }
    }
    public class EditAccountCommand : ICommand
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public long UpdatedByUserId { get; set; }
    }
}
