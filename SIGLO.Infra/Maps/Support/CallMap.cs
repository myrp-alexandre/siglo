using FluentNHibernate.Mapping;
using SIGLO.Domain.Entities;
using SIGLO.Domain.ValueObjects;

namespace SIGLO.Infra.Maps
{
    public class CallMap : ClassMap<Call>
    {
        public CallMap()
        {
            Table("[Call]");
            Id(x => x.Id, "[Id]").GeneratedBy.Identity();

            Component<NameOrDescription>(x => x.Title, m =>
            {
                m.Map(x => x.Name, "Name");
            });
        }
    }
}
