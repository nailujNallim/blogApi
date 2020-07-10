using zmg.blogEngine.model.Enumerations;
using System;
using System.Collections.Generic;
using System.Text;

namespace zmg.blogEngine.model.Domain
{
    public class Post : IEntity
    {
        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string Content { get; set; }

        public virtual Writer Author { get; set; }
        public virtual Editor ApprovedBy { get; set; }
        public virtual DateTime SubmitDate { get; set; }
        public virtual DateTime RevisionDate { get; set; }
        public virtual StatusPost Status{ get; set; }
        public virtual ICollection<Comment> Comments{ get; set; }

        public Post()
        {
            Comments = new List<Comment>();
        }
    }
}
