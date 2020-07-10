using System;

namespace zmg.blogEngine.model.Domain
{
    public class Comment : IEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string Content { get; set; }
        public virtual DateTime PublishDate { get; set; }
        public virtual User Author { get; set; }

    }
}