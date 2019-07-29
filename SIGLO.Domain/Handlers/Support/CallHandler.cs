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
    public class CallHandler : Notifiable,
        ICommandHandler<CreateCallCommand>,
        ICommandHandler<EditCallCommand>,
        ICommandHandler<DeleteCallCommand>
    {
        private readonly ICallRepository _CallRepository;
        public CallHandler(ICallRepository compamyRepository)
        {
            _CallRepository = compamyRepository;
        }
        public async Task<ICommandResult> Handle(CreateCallCommand command)
        {
            NameOrDescription title = new NameOrDescription(command.Title);
            CompanyAndBranchOffice companyAndBranchOffice = new CompanyAndBranchOffice(command.CompanyId, command.BranchOfficeId);
            Call call = new Call(
                command.CallTypeId,
                command.CallGroupId,
                command.CustomerId,
                command.SectorId,
                command.ContractId,
                command.EmployeeId,
                command.Priority,
                title,
                command.Details,
                command.CreatedByUserId,
                companyAndBranchOffice,
                command.MachineNameOrIP);

            AddNotifications(title.Notifications);
            AddNotifications(call.Notifications);

            if (!Valid)
                return new CommandResult(
                    false,
                    Messages.NOTIFICATIONS,
                    Notifications);

            await _CallRepository.Create(call);

            return new CommandResult(
                true,
                Messages.RECORDED_WITH_SUCCESS,
                new CallCommandResult
                {
                    Id = call.Id,
                    CallTypeId = call.CallTypeId,
                    CallGroupId = call.CallGroupId,
                    CustomerId = call.CustomerId,
                    SectorId = call.SectorId,
                    ContractId = call.ContractId,
                    EmployeeId = call.EmployeeId,
                    Priority = call.Priority,
                    Title = call.Title.Name,
                    Details = call.Details,
                    Status = call.Status,
                    CreatedDate = call.Audit.CreatedDateBy.CreatedDate,
                    CreatedByUserId = call.Audit.CreatedDateBy.CreatedByUserId,
                    UpdatedDate = call.Audit.UpdatedDateBy.UpdatedDate,
                    UpdatedByUserId = call.Audit.UpdatedDateBy.UpdatedByUserId,
                    CompanyId = call.Audit.CompanyAndBranchOffice.CompanyId,
                    BranchOfficeId = call.Audit.CompanyAndBranchOffice.BranchOfficeId,
                    MachineNameOrIP = call.Audit.MachineNameOrIP
                });
        }

        public async Task<ICommandResult> Handle(EditCallCommand command)
        {
            Call call = await _CallRepository.GetById(command.Id);
            if (call == null)
                return new CommandResult(false, Messages.Account_NOT_FOUND, null);

            NameOrDescription title = new NameOrDescription(command.Name);
            call.Update(
                command.CallTypeId,
                command.CallGroupId,
                command.SectorId,
                command.ContractId,
                command.CustomerId,
                command.EmployeeId,
                command.Priority,
                title,
                command.Details,
                command.UpdatedByUserId
                );

            AddNotifications(title.Notifications);
            AddNotifications(call.Notifications);

            if (!Valid)
                return new CommandResult(
                    false,
                    Messages.NOTIFICATIONS,
                    Notifications);

            await _CallRepository.Update(call);

            return new CommandResult(
                true,
                Messages.UPDATED_WITH_SUCCESS,
                new CallCommandResult
                {
                    Id = call.Id,
                    CallTypeId = call.CallTypeId,
                    CallGroupId = call.CallGroupId,
                    CustomerId = call.CustomerId,
                    SectorId = call.SectorId,
                    ContractId = call.ContractId,
                    EmployeeId = call.EmployeeId,
                    Priority = call.Priority,
                    Title = call.Title.Name,
                    Details = call.Details,
                    Status = call.Status,
                    CreatedDate = call.Audit.CreatedDateBy.CreatedDate,
                    CreatedByUserId = call.Audit.CreatedDateBy.CreatedByUserId,
                    UpdatedDate = call.Audit.UpdatedDateBy.UpdatedDate,
                    UpdatedByUserId = call.Audit.UpdatedDateBy.UpdatedByUserId,
                    CompanyId = call.Audit.CompanyAndBranchOffice.CompanyId,
                    BranchOfficeId = call.Audit.CompanyAndBranchOffice.BranchOfficeId,
                    MachineNameOrIP = call.Audit.MachineNameOrIP
                });
        }

        public async Task<ICommandResult> Handle(DeleteCallCommand command)
        {
            Call Call = await _CallRepository.GetById(command.Id);
            if (Call == null)
                return new CommandResult(false, Messages.Account_NOT_FOUND, null);

            await _CallRepository.Delete(Call);

            return new CommandResult(
                true,
                Messages.DELETED_WITH_SUCCESS,
                null);
        }
    }
}