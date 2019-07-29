using FluentNHibernate.Mapping;
using SIGLO.Domain.Entities;
using SIGLO.Domain.ValueObjects;

namespace SIGLO.Infra.Maps
{
    public class AccountMap : ClassMap<Account>
    {
        public AccountMap()
        {
            Table("[Account]");
            Id(x => x.Id, "[Id]").GeneratedBy.Identity();

            Component<NameOrDescription>(x => x.Name, m =>
            {
                m.Map(x => x.Name, "Name")
                .Length(200)
                .Nullable()
                .Default(null);
            });
            Component<Audit>(x => x.Audit, m =>
            {
                m.Map(x => x.CreatedDateBy.CreatedByUserId, "CreatedByUserId");
                m.Map(x => x.CreatedDateBy.CreatedDate, "CreatedDate");
                m.Map(x => x.UpdatedDateBy.UpdatedByUserId, "UpdatedByUserId");
                m.Map(x => x.UpdatedDateBy.UpdatedDate, "UpdatedDate");
                m.Map(x => x.RecordStatus, "RecordStatus");
                m.Map(x => x.MachineNameOrIP, "MachineNameOrIP");
            });
            
        }
    }
}
