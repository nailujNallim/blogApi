using FluentNHibernate.Mapping;
using zmg.blogEngine.model.Domain;

namespace zmg.blogEngine.repository.mappings
{
    public class PostMap : ClassMap<Post>
    {
        public PostMap()
        {
            Id(x => x.Id);
            Map(x => x.Title).Not.Nullable();
            Map(x => x.Content).Not.Nullable();
            References(x => x.Author).Nullable();
            References(x => x.ApprovedBy).Nullable();
            Map(x => x.SubmitDate).Not.Nullable();
            Map(x => x.RevisionDate).Nullable();
            Map(x => x.Status).Not.Nullable();
            HasMany(x => x.Comments).KeyColumn("commentId");
            //HasMany(x => x.Roles).Element("EnumValueColumn");

        }
    }
}