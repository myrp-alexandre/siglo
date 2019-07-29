using SIGLO.Shared.Commands;
using System;

namespace SIGLO.Domain.Commands
{
    public class UserCommandResult
    {
        public long Id { get; set; }
        public string Name { get;  set; }
        public string Login { get;  set; }
        public string Password { get;  set; }
        public long Active { get; set; }
        public long Nivel { get;  set; }
        public decimal PercentM { get;  set; }
        public decimal PercentB { get;  set; }
        public DateTime? CreatedDate { get; set; }
        public long? CreatedByUserId { get; set; }
        public DateTime? UpdatedDate { get; set; }
        public long? UpdatedByUserId { get; set; }
        public long? CompanyId { get; set; }
        public long? BranchOfficeId { get; set; }
        public string MachineNameOrIP { get; set; }
    }
    public class CreateUserCommand : ICommand
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public long Active { get; set; }
        public long Nivel { get; set; }
        public decimal PercentM { get; set; }
        public decimal PercentB { get; set; }
        public long CreatedByUserId { get; set; }
        public long CompanyId { get; set; }
        public long BranchOfficeId { get; set; }
        public string MachineNameOrIP { get; set; }

    }
    public class EditUserCommand : ICommand
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }
        public long Active { get; set; }
        public long Nivel { get; set; }
        public decimal PercentM { get; set; }
        public decimal PercentB { get; set; }
        public long? UpdatedByUserId { get; set; }
    }
    public class DeleteUserCommand : ICommand
    {
        public long Id { get; set; }
    }
    public class AuthenticateCommandResult
    {
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
