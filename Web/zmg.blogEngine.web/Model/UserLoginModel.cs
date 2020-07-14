using System.ComponentModel.DataAnnotations;

namespace zmg.blogEngine.web.Model
{
    public class UserLoginModel
    {
        [Required]
        [StringLength(8, ErrorMessage = "Name length can't be more than 8.")]
        public string Username { get; set; }

        //public int UserType { get; set; }

    }
}
