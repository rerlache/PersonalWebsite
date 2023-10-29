using API.Data.General;
using AutoMapper;

namespace API.Helpers
{
    public class Mappers : Profile
    {
        public Mappers()
        {
            UserDTOMapping();
            LoginHistoryDTOMapping();
        }
        void UserDTOMapping()
        {
            CreateMap<User, UserDTO>()
                .ForMember(
                    x => x.Apps,
                    opt =>
                        opt.MapFrom(src => 
                            src.AssignedApps)
                )
                .ForMember(
                    x => x.LoginHistory, 
                    opt => 
                        opt.MapFrom(src => 
                            src.UserLoginHistory)
                );
        }
        void LoginHistoryDTOMapping()
        {
            CreateMap<UserLoginHistory, UserLoginHistoryDTO>();
        }
    }
}
