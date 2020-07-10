using System;
using System.Collections.Generic;

namespace zmg.blogEngine.api.Models
{
    public class NewPostDto
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Username { get; set; }
    }
}

