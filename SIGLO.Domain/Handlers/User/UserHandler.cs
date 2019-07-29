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
    public class UserHandler : Notifiable,
        ICommandHandler<CreateUserCommand>,
        ICommandHandler<EditUserCommand>,
        ICommandHandler<DeleteUserCommand>
    {
        private readonly IUserRepository _UserRepository;
        public UserHandler(IUserRepository compamyRepository)
        {
            _UserRepository = compamyRepository;
        }
        public async Task<ICommandResult> Handle(CreateUserCommand command)
        {
            NameOrDescription name = new NameOrDescription(command.Name);
            CompanyAndBranchOffice companyAndBranchOffice = new CompanyAndBranchOffice(command.CompanyId, command.BranchOfficeId);
            User user = new User(name,
                                 command.Login,
                                 command.Password,
                                 command.ConfirmPassword,
                                 command.Nivel,
                                 command.PercentM,
                                 command.PercentB,
                                 command.CreatedByUserId,
                                 companyAndBranchOffice,
                                 command.MachineNameOrIP);

            AddNotifications(user.Notifications);

            if (!Valid)
                return new CommandResult(
                    false,
                    Messages.NOTIFICATIONS,
                    Notifications);

            await _UserRepository.Create(user);

            return new CommandResult(
                true,
                Messages.RECORDED_WITH_SUCCESS,
                new UserCommandResult
                {
                    Id = user.Id,
                    Name = user.Name.Name,
                    Login = user.Login,
                    Password = user.Password,
                    Active = (int) user.Active,
                    Nivel = user.Nivel,
                    PercentM = user.PercentM,
                    PercentB = user.PercentB,
                    CreatedDate = user.Audit.CreatedDateBy.CreatedDate,
                    CreatedByUserId = user.Audit.CreatedDateBy.CreatedByUserId,
                    CompanyId = user.Audit.CompanyAndBranchOffice.CompanyId,
                    BranchOfficeId = user.Audit.CompanyAndBranchOffice.BranchOfficeId
                });
        }

        public async Task<ICommandResult> Handle(EditUserCommand command)
        {
            User user = await _UserRepository.GetById(command.Id);
            if (user == null)
                return new CommandResult(false, Messages.Account_NOT_FOUND, null);

            NameOrDescription name = new NameOrDescription(command.Name);
            user.Update(name,
                command.Login,
                command.Password,
                command.ConfirmPassword,
                command.Nivel,
                command.Active,
                command.PercentM,
                command.PercentB,
                command.UpdatedByUserId);

            AddNotifications(user.Notifications);

            if (!Valid)
                return new CommandResult(
                    false,
                    Messages.NOTIFICATIONS,
                    Notifications);

            await _UserRepository.Update(user);

            return new CommandResult(
                true,
                Messages.UPDATED_WITH_SUCCESS,
                new UserCommandResult
                {
                    Id = user.Id,
                    Name = user.Name.Name,
                    Login = user.Login,
                    Password = user.Password,
                    Active = (int) user.Active,
                    Nivel = user.Nivel,
                    PercentM = user.PercentM,
                    PercentB = user.PercentB,
                    CreatedDate = user.Audit.CreatedDateBy.CreatedDate,
                    CreatedByUserId = user.Audit.CreatedDateBy.CreatedByUserId,
                    UpdatedDate = user.Audit.UpdatedDateBy.UpdatedDate,
                    UpdatedByUserId = user.Audit.UpdatedDateBy.UpdatedByUserId,
                    CompanyId = user.Audit.CompanyAndBranchOffice.CompanyId,
                    BranchOfficeId = user.Audit.CompanyAndBranchOffice.BranchOfficeId
                });
        }

        public async Task<ICommandResult> Handle(DeleteUserCommand command)
        {
            User user = await _UserRepository.GetById(command.Id);
            if (user == null)
                return new CommandResult(false, Messages.Account_NOT_FOUND, null);

            await _UserRepository.Delete(user);

            return new CommandResult(
                true,
                Messages.DELETED_WITH_SUCCESS,
                null);
        }
    }
}