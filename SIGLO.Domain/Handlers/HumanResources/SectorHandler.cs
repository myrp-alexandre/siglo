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
    public class SectorHandler : Notifiable,
        ICommandHandler<CreateSectorCommand>,
        ICommandHandler<EditSectorCommand>,
        ICommandHandler<DeleteSectorCommand>
    {
        private readonly ISectorRepository _SectorRepository;
        public SectorHandler(ISectorRepository compamyRepository)
        {
            _SectorRepository = compamyRepository;
        }
        public async Task<ICommandResult> Handle(CreateSectorCommand command)
        {
            NameOrDescription name = new NameOrDescription(command.Name);
            CompanyAndBranchOffice companyAndBranchOffice = new CompanyAndBranchOffice(command.CompanyId, command.BranchOfficeId);
            Sector sector = new Sector(name, command.CreatedByUserId, companyAndBranchOffice, command.MachineNameOrIP);

            AddNotifications(name.Notifications);
            AddNotifications(sector.Notifications);

            if (!Valid)
                return new CommandResult(
                    false,
                    Messages.NOTIFICATIONS,
                    Notifications);

             await _SectorRepository.Create(sector);

            return new CommandResult(
                true,
                Messages.RECORDED_WITH_SUCCESS,
                new SectorCommandResult
                {
                    Id = sector.Id,
                    Name = sector.Name.Name,
                    CreatedDate = sector.Audit.CreatedDateBy.CreatedDate,
                    CreatedByUserId = sector.Audit.CreatedDateBy.CreatedByUserId,
                    UpdatedDate = sector.Audit.UpdatedDateBy.UpdatedDate,
                    UpdatedByUserId = sector.Audit.UpdatedDateBy.UpdatedByUserId,
                    CompanyId = sector.Audit.CompanyAndBranchOffice.CompanyId,
                    BranchOfficeId = sector.Audit.CompanyAndBranchOffice.BranchOfficeId
                });
        }

        public async Task<ICommandResult> Handle(EditSectorCommand command)
        {
            Sector Sector = await _SectorRepository.GetById(command.Id);
            if (Sector == null)
                return new CommandResult(false, Messages.Account_NOT_FOUND, null);

            NameOrDescription name = new NameOrDescription(command.Name);
            Sector.Update(name, command.UpdatedByUserId);

            AddNotifications(name.Notifications);
            AddNotifications(Sector.Notifications);

            if (!Valid)
                return new CommandResult(
                    false,
                    Messages.NOTIFICATIONS,
                    Notifications);

            await _SectorRepository.Update(Sector);

            return new CommandResult(
                true,
                Messages.UPDATED_WITH_SUCCESS,
                new SectorCommandResult
                {
                    Id = Sector.Id,
                    Name = Sector.Name.Name,
                    CreatedDate = Sector.Audit.CreatedDateBy.CreatedDate,
                    CreatedByUserId = Sector.Audit.CreatedDateBy.CreatedByUserId,
                    UpdatedDate = Sector.Audit.UpdatedDateBy.UpdatedDate,
                    UpdatedByUserId = Sector.Audit.UpdatedDateBy.UpdatedByUserId,
                    CompanyId = Sector.Audit.CompanyAndBranchOffice.CompanyId,
                    BranchOfficeId = Sector.Audit.CompanyAndBranchOffice.BranchOfficeId
                });
        }

        public async Task<ICommandResult> Handle(DeleteSectorCommand command)
        {
            Sector Sector = await _SectorRepository.GetById(command.Id);
            if (Sector == null)
                return new CommandResult(false, Messages.Account_NOT_FOUND, null);

            await _SectorRepository.Delete(Sector);

            return new CommandResult(
                true,
                Messages.DELETED_WITH_SUCCESS,
                null);
        }
    }
}
