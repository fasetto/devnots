using AutoMapper;
using DevNots.Application.Contracts;
using DevNots.Domain;

namespace DevNots.Application.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<User, UserResponse>();
            CreateMap<RegisterUserRequest, User>(MemberList.Source);
            CreateMap<UpdateUserRequest, User>(MemberList.Source);

            CreateMap<Note, NoteResponse>();
            CreateMap<AddNoteRequest, Note>(MemberList.Source);
            CreateMap<UpdateNoteRequest, Note>(MemberList.Source);

            CreateMap<Tag, TagResponse>();
            CreateMap<CreateTagRequest, Tag>(MemberList.Source);
        }
    }
}
