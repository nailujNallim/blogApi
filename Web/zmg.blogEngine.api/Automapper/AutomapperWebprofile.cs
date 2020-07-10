using AutoMapper;
using zmg.blogEngine.api.Models;
using zmg.blogEngine.model.Domain;

namespace zmg.blogEngine.api.Automapper
{
    public class AutomapperWebprofile : Profile
    {
        public AutomapperWebprofile()
        {
            AutoMapperPost();
        }

        private void AutoMapperPost()
        {
            CreateMap<Post, NewPostDto>()
                .ForMember(x => x.Title, y => y.MapFrom(z => z.Title))
                .ForMember(x => x.Content, y => y.MapFrom(z => z.Content))
                .ForMember(x => x.Username, y => y.MapFrom(z => z.Author.UserName))
                .ReverseMap();
        }
    }
}