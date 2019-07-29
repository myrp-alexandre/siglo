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
    public class CallTypeHandler : Notifiable,
        ICommandHandler<CreateCallTypeCommand>,
        ICommandHandler<EditCallTypeCommand>,
        ICommandHandler<DeleteCallTypeCommand>
    {
        private readonly ICallTypeRepository _CallTypeRepository;
        public CallTypeHandler(ICallTypeRepository compamyRepository)
        {
            _CallTypeRepository = compamyRepository;
        }
        public async Task<ICommandResult> Handle(CreateCallTypeCommand command)
        {
            NameOrDescription name = new NameOrDescription(command.Name);
            CompanyAndBranchOffice companyAndBranchOffice = new CompanyAndBranchOffice(command.CompanyId, command.BranchOfficeId);
            CallType callType = new CallType(name, command.CreatedByUserId, companyAndBranchOffice, command.MachineNameOrIP);

            AddNotifications(name.Notifications);
            AddNotifications(callType.Notifications);

            if (!Valid)
                return new CommandResult(
                    false,
                    Messages.NOTIFICATIONS,
                    Notifications);

            await _CallTypeRepository.Create(callType);

            return new CommandResult(
                true,
                Messages.RECORDED_WITH_SUCCESS,
                new CallTypeCommandResult
                {
                    Id = callType.Id,
                    Name = callType.Name.Name,
                    CreatedDate = callType.Audit.CreatedDateBy.CreatedDate,
                    CreatedByUserId = callType.Audit.CreatedDateBy.CreatedByUserId,
                    UpdatedDate = callType.Audit.UpdatedDateBy.UpdatedDate,
                    UpdatedByUserId = callType.Audit.UpdatedDateBy.UpdatedByUserId,
                    CompanyId = callType.Audit.CompanyAndBranchOffice.CompanyId,
                    BranchOfficeId = callType.Audit.CompanyAndBranchOffice.BranchOfficeId

                });
        }

        public async Task<ICommandResult> Handle(EditCallTypeCommand command)
        {
            CallType callType = await _CallTypeRepository.GetById(command.Id);
            if (callType == null)
                return new CommandResult(false, Messages.Account_NOT_FOUND, null);

            NameOrDescription name = new NameOrDescription(command.Name);
            callType.Update(name, command.UpdatedByUserId);

            AddNotifications(name.Notifications);
            AddNotifications(callType.Notifications);

            if (!Valid)
                return new CommandResult(
                    false,
                    Messages.NOTIFICATIONS,
                    Notifications);

            await _CallTypeRepository.Update(callType);

            return new CommandResult(
                true,
                Messages.UPDATED_WITH_SUCCESS,
                new CallTypeCommandResult
                {
                    Id = callType.Id,
                    Name = callType.Name.Name,
                    CreatedDate = callType.Audit.CreatedDateBy.CreatedDate,
                    CreatedByUserId = callType.Audit.CreatedDateBy.CreatedByUserId,
                    UpdatedDate = callType.Audit.UpdatedDateBy.UpdatedDate,
                    UpdatedByUserId = callType.Audit.UpdatedDateBy.UpdatedByUserId,
                    CompanyId = callType.Audit.CompanyAndBranchOffice.CompanyId,
                    BranchOfficeId = callType.Audit.CompanyAndBranchOffice.BranchOfficeId
                });
        }

        public async Task<ICommandResult> Handle(DeleteCallTypeCommand command)
        {
            CallType callType = await _CallTypeRepository.GetById(command.Id);
            if (callType == null)
                return new CommandResult(false, Messages.Account_NOT_FOUND, null);

            await _CallTypeRepository.Delete(callType);

            return new CommandResult(
                true,
                Messages.DELETED_WITH_SUCCESS,
                null);
        }
    }
}