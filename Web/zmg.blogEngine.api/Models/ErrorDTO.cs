namespace zmg.blogEngine.api.Models
{
    public class ErrorDTO
    {
        public ErrorDTO()
        {

        }

        public ErrorDTO(string code, string message, string description)
        {
            Code = code;
            Message = message;
            Description = description;
        }

        public string Code { get; set; }

        public string Message { get; set; }

        public string Description { get; set; }

        public string StackTrace { get; set; }
    }
}
