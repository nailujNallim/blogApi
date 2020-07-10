using NHibernate;

namespace zmg.blogEngine.repository
{
    public interface INHibernateHelper
    {
        ISession GetSession();
    }
}
