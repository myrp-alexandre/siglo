using Flunt.Notifications;
using SIGLO.Shared.Messages;

namespace SIGLO.Domain.ValueObjects
{
    public class CompanyAndBranchOffice : Notifiable
    {
        public CompanyAndBranchOffice(long companyId, long branchOfficeId)
        {
            AddNotifications(new Flunt.Validations.Contract().Requires()
                .IsGreaterThan(companyId, 0, "Empresa (companyId)", Messages.CODE_NOT_ZERO)
                .IsGreaterThan(branchOfficeId, 0, "Filial (branchOfficeId)", Messages.CODE_NOT_ZERO));
            CompanyId = companyId;
            BranchOfficeId = branchOfficeId;
        }

        public virtual long CompanyId { get; protected set; }
        public virtual long BranchOfficeId { get; protected set; }
    }
}
