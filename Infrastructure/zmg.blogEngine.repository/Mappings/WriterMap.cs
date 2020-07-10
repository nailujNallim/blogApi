using FluentNHibernate.Mapping;
using zmg.blogEngine.model.Domain;

namespace zmg.blogEngine.repository.mappings
{
    public class WriterMap : SubclassMap<Writer>
    {
        public WriterMap()
        {
        }
    }
}