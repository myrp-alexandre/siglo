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
    public class ContractHandler : Notifiable,
        ICommandHandler<CreateContractCommand>,
        ICommandHandler<EditContractCommand>,
        ICommandHandler<DeleteContractCommand>
    {
        private readonly IContractRepository _ContractRepository;
        public ContractHandler(IContractRepository compamyRepository)
        {
            _ContractRepository = compamyRepository;
        }
        public async Task<ICommandResult> Handle(CreateContractCommand command)
        {
            NameOrDescription name = new NameOrDescription(command.Name);

            CompanyAndBranchOffice companyAndBranchOffice = new CompanyAndBranchOffice(command.CompanyId, command.BranchOfficeId);
            Contract contract = new Contract(name, command.ValidateDate, command.CreatedByUserId, companyAndBranchOffice, command.MachineNameOrIP);

            AddNotifications(name.Notifications);
            AddNotifications(contract.Notifications);

            if (!Valid)
                return new CommandResult(
                    false,
                    Messages.NOTIFICATIONS,
                    Notifications);

            await _ContractRepository.Create(contract);

            return new CommandResult(
                true,
                Messages.RECORDED_WITH_SUCCESS,
                new ContractCommandResult
                {
                    Id = contract.Id,
                    Name = contract.Name.Name,
                    ValidateDate = contract.ValidateDate,
                    CreatedDate = contract.Audit.CreatedDateBy.CreatedDate,
                    CreatedByUserId = contract.Audit.CreatedDateBy.CreatedByUserId,
                    UpdatedDate = contract.Audit.UpdatedDateBy.UpdatedDate,
                    UpdatedByUserId = contract.Audit.UpdatedDateBy.UpdatedByUserId,
                    CompanyId = contract.Audit.CompanyAndBranchOffice.CompanyId,
                    BranchOfficeId = contract.Audit.CompanyAndBranchOffice.BranchOfficeId
                });
        }

        public async Task<ICommandResult> Handle(EditContractCommand command)
        {
            Contract contract =  await _ContractRepository.GetById(command.Id);
            if (contract == null)
                return new CommandResult(false, Messages.Account_NOT_FOUND, null);

            NameOrDescription name = new NameOrDescription(command.Name);
            contract.Update(name, command.ValidateDate, command.UpdatedByUserId);


            if (!Valid)
                return new CommandResult(
                    false,
                    Messages.NOTIFICATIONS,
                    Notifications);

            await _ContractRepository.Update(contract);

            return new CommandResult(
                true,
                Messages.UPDATED_WITH_SUCCESS,
                new ContractCommandResult
                {
                    Id = contract.Id,
                    Name = contract.Name.Name,
                    CreatedDate = contract.Audit.CreatedDateBy.CreatedDate,
                    CreatedByUserId = contract.Audit.CreatedDateBy.CreatedByUserId,
                    UpdatedDate = contract.Audit.UpdatedDateBy.UpdatedDate,
                    UpdatedByUserId = contract.Audit.UpdatedDateBy.UpdatedByUserId,
                    CompanyId = contract.Audit.CompanyAndBranchOffice.CompanyId,
                    BranchOfficeId = contract.Audit.CompanyAndBranchOffice.BranchOfficeId
                });
        }

        public async Task<ICommandResult> Handle(DeleteContractCommand command)
        {
            Contract contract = await _ContractRepository.GetById(command.Id);
            if (contract == null)
                return new CommandResult(false, Messages.Account_NOT_FOUND, null);

            await _ContractRepository.Delete(contract);

            return new CommandResult(
                true,
                Messages.DELETED_WITH_SUCCESS,
                null);
        }
    }
}