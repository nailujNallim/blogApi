using System;

namespace zmg.blogEngine.model.Domain
{
    public class User : IEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string FullName{ get; set; }
        public virtual string UserName { get; set; }

    }
}
