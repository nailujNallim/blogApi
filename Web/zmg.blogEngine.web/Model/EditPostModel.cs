using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zmg.blogEngine.web.Model
{
    public class EditPostModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime SubmitDate { get; set; }   
        public string Username { get; set; }   
    }
}
