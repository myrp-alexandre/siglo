using Flunt.Notifications;
using SIGLO.Domain.Commands;
using SIGLO.Domain.Entities;
using SIGLO.Domain.Repositories;
using SIGLO.Domain.ValueObjects;
using SIGLO.Shared.Commands;
using SIGLO.Shared.Messages;
using System.Threading.Tasks;

namespace SIGLO.Domain.Handlers
{
    public class AccountHandler : Notifiable,
        ICommandHandler<CreateAccountCommand>,
        ICommandHandler<EditAccountCommand>,
        ICommandHandler<DeleteAccountCommand>
    {
        private readonly IAccountRepository _accountRepository;
        public AccountHandler(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        public async Task<ICommandResult> Handle(CreateAccountCommand command)
        {
            NameOrDescription name = new NameOrDescription(command.Name);
            CompanyAndBranchOffice companyAndBranchOffice = new CompanyAndBranchOffice(command.CompanyId, command.BranchOfficeId);
            Account account = new Account(name, command.CreatedByUserId, companyAndBranchOffice, command.MachineNameOrIP);

            AddNotifications(name.Notifications);
            AddNotifications(account.Notifications);

            if (!Valid)
                return new CommandResult(
                    false,
                    Messages.NOTIFICATIONS,
                    Notifications);

             await _accountRepository.Create(account);

            return new CommandResult(
                true,
                Messages.RECORDED_WITH_SUCCESS,
                new AccountCommandResult
                {
                    Id = account.Id,
                    Name = account.Name.Name,
                    CreatedDate = account.Audit.CreatedDateBy.CreatedDate,
                    CreatedByUserId = account.Audit.CreatedDateBy.CreatedByUserId,
                    UpdatedDate = account.Audit.UpdatedDateBy.UpdatedDate,
                    UpdatedByUserId = account.Audit.UpdatedDateBy.UpdatedByUserId,
                    CompanyId = account.Audit.CompanyAndBranchOffice.CompanyId,
                    BranchOfficeId = account.Audit.CompanyAndBranchOffice.BranchOfficeId
                });
        }

        public async Task<ICommandResult> Handle(EditAccountCommand command)
        {
            Account account = await _accountRepository.GetById(command.Id);
            if (account == null)
                return new CommandResult(false, Messages.Account_NOT_FOUND, null);

            NameOrDescription name = new NameOrDescription(command.Name);
            account.Update(name, command.UpdatedByUserId);

            AddNotifications(name.Notifications);
            AddNotifications(account.Notifications);

            if (!Valid)
                return new CommandResult(
                    false,
                    Messages.NOTIFICATIONS,
                    Notifications);

            await _accountRepository.Update(account);

            return new CommandResult(
                true,
                Messages.UPDATED_WITH_SUCCESS,
                new AccountCommandResult
                {
                    Id = account.Id,
                    Name = account.Name.Name,
                    CreatedDate = account.Audit.CreatedDateBy.CreatedDate,
                    CreatedByUserId = account.Audit.CreatedDateBy.CreatedByUserId,
                    UpdatedDate = account.Audit.UpdatedDateBy.UpdatedDate,
                    UpdatedByUserId = account.Audit.UpdatedDateBy.UpdatedByUserId,
                    CompanyId = account.Audit.CompanyAndBranchOffice.CompanyId,
                    BranchOfficeId = account.Audit.CompanyAndBranchOffice.BranchOfficeId
                });
        }

        public async Task<ICommandResult> Handle(DeleteAccountCommand command)
        {
            Account account =  await _accountRepository.GetById(command.Id);
            if (account == null)
                return new CommandResult(false, Messages.Account_NOT_FOUND, null);

            await _accountRepository.Delete(account);

            return new CommandResult(
                true,
                Messages.DELETED_WITH_SUCCESS,
                null);
        }
    }
}
