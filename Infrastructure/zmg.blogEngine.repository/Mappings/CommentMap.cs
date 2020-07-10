using FluentNHibernate.Mapping;
using zmg.blogEngine.model.Domain;

namespace zmg.blogEngine.repository.mappings
{
    public class CommentMap : ClassMap<Comment>
    {
        public CommentMap() 
        {
            Id(x => x.Id);
            Map(x => x.Content).Not.Nullable();
            Map(x => x.PublishDate).Not.Nullable();
            References(x => x.Author).Not.Nullable();
        }
    }
}