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
    public class CallGroupHandler : Notifiable,
        ICommandHandler<CreateCallGroupCommand>,
        ICommandHandler<EditCallGroupCommand>,
        ICommandHandler<DeleteCallGroupCommand>
    {
        private readonly ICallGroupRepository _CallGroupRepository;
        public CallGroupHandler(ICallGroupRepository compamyRepository)
        {
            _CallGroupRepository = compamyRepository;
        }
        public async Task<ICommandResult> Handle(CreateCallGroupCommand command)
        {
            NameOrDescription name = new NameOrDescription(command.Name);
            CompanyAndBranchOffice companyAndBranchOffice = new CompanyAndBranchOffice(command.CompanyId, command.BranchOfficeId);
            CallGroup callGroup = new CallGroup(name, command.CreatedByUserId, companyAndBranchOffice, command.MachineNameOrIP);

            AddNotifications(name.Notifications);
            AddNotifications(callGroup.Notifications);

            if (!Valid)
                return new CommandResult(
                    false,
                    Messages.NOTIFICATIONS,
                    Notifications);

            await _CallGroupRepository.Create(callGroup);

            return new CommandResult(
                true,
                Messages.RECORDED_WITH_SUCCESS,
                new CallGroupCommandResult
                {
                    Id = callGroup.Id,
                    Name = callGroup.Name.Name,
                    CreatedDate = callGroup.Audit.CreatedDateBy.CreatedDate,
                    CreatedByUserId = callGroup.Audit.CreatedDateBy.CreatedByUserId,
                    UpdatedDate = callGroup.Audit.UpdatedDateBy.UpdatedDate,
                    UpdatedByUserId = callGroup.Audit.UpdatedDateBy.UpdatedByUserId,
                    CompanyId = callGroup.Audit.CompanyAndBranchOffice.CompanyId,
                    BranchOfficeId = callGroup.Audit.CompanyAndBranchOffice.BranchOfficeId,
                    MachineNameOrIP = callGroup.Audit.MachineNameOrIP,
                });
        }

        public async Task<ICommandResult> Handle(EditCallGroupCommand command)
        {
            CallGroup CallGroup = await _CallGroupRepository.GetById(command.Id);
            if (CallGroup == null)
                return new CommandResult(false, Messages.Account_NOT_FOUND, null);

            NameOrDescription name = new NameOrDescription(command.Name);
            CallGroup.Update(name, command.UpdatedByUserId);

            AddNotifications(name.Notifications);
            AddNotifications(CallGroup.Notifications);

            if (!Valid)
                return new CommandResult(
                    false,
                    Messages.NOTIFICATIONS,
                    Notifications);

            await _CallGroupRepository.Update(CallGroup);

            return new CommandResult(
                true,
                Messages.UPDATED_WITH_SUCCESS,
                new CallGroupCommandResult
                {
                    Id = CallGroup.Id,
                    Name = CallGroup.Name.Name,
                    CreatedDate = CallGroup.Audit.CreatedDateBy.CreatedDate,
                    CreatedByUserId = CallGroup.Audit.CreatedDateBy.CreatedByUserId,
                    UpdatedDate = CallGroup.Audit.UpdatedDateBy.UpdatedDate,
                    UpdatedByUserId = CallGroup.Audit.UpdatedDateBy.UpdatedByUserId,
                    CompanyId = CallGroup.Audit.CompanyAndBranchOffice.CompanyId,
                    BranchOfficeId = CallGroup.Audit.CompanyAndBranchOffice.BranchOfficeId,
                    MachineNameOrIP = CallGroup.Audit.MachineNameOrIP
                });
        }

        public async Task<ICommandResult> Handle(DeleteCallGroupCommand command)
        {
            CallGroup CallGroup = await _CallGroupRepository.GetById(command.Id);
            if (CallGroup == null)
                return new CommandResult(false, Messages.Account_NOT_FOUND, null);

            await _CallGroupRepository.Delete(CallGroup);

            return new CommandResult(
                true,
                Messages.DELETED_WITH_SUCCESS,
                null);
        }
    }
}