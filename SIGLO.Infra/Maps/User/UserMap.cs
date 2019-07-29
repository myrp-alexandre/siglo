using FluentNHibernate.Mapping;
using SIGLO.Domain.Entities;
using SIGLO.Domain.ValueObjects;

namespace SIGLO.Infra.Maps
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Table("[User]");
            Id(x => x.Id, "[Id]").GeneratedBy.Identity();

            Component<NameOrDescription>(x => x.Name, m =>
            {
                m.Map(x => x.Name, "Name");
            });
        }
    }
}
