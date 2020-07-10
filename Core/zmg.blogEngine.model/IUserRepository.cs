using System;
using System.Threading.Tasks;
using zmg.blogEngine.model.Domain;

namespace zmg.blogEngine.model
{
    public interface IUserRepository
    {
        Task<Writer> GetWriterByUsername(string writerUsername);
        Task<Editor> GetEditorByUsername(string editorUsername);
    }
}
