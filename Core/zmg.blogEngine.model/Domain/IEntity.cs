using System;
using System.Collections.Generic;
using System.Text;

namespace zmg.blogEngine.model.Domain
{
    public interface IEntity
    {
        Guid Id { get; set; }
    }
}
