using FluentNHibernate.Mapping;
using zmg.blogEngine.model.Domain;

namespace zmg.blogEngine.repository.mappings
{
    public class UserMap : ClassMap<User>
    {
        public UserMap()
        {
            Id(x => x.Id);
            //HasMany(x => x.Rols).Element("EnumValueColumn");
            Map(x => x.FullName).Not.Nullable();
            Map(x => x.UserName).Not.Nullable();
            Map(x => x.UserType).Not.Nullable();

        }
    }
}