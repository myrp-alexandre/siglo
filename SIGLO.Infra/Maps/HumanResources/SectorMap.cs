using FluentNHibernate.Mapping;
using SIGLO.Domain.Entities;
using SIGLO.Domain.ValueObjects;

namespace SIGLO.Infra.Maps
{
    public class SectorMap : ClassMap<Sector>
    {
        public SectorMap()
        {
            Table("[Sector]");
            Id(x => x.Id, "[Id]").GeneratedBy.Identity();

            Component<NameOrDescription>(x => x.Name, m =>
            {
                m.Map(x => x.Name, "Name");
            });
        }
    }
}
