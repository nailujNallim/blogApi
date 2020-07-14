using System;
using System.Collections.Generic;

namespace zmg.blogEngine.api.Models
{
    public class UpdatePostDto
    {
        public bool Approved { get; set; }
        public string Username { get; set; }
    }
}

