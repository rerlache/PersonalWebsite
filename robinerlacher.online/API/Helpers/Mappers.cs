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
            CreateMap<User, UserDTO>();
        }
        void LoginHistoryDTOMapping()
        {
            CreateMap<UserLoginHistory, UserLoginHistoryDTO>();
        }
    }
}
