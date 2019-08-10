using AutoMapper;
using MGG.Bookloan.Services.Mapper.Profiles;

namespace MGG.Bookloan.Services.Mapper
{
    public class MapperConfig
    {
        public static MapperConfiguration Register()
        {
            return new MapperConfiguration(config =>
            {
                config.AddProfile<DomainToViewModel>();
                config.AddProfile<ViewModelToDomain>();
            });
        }
    }
}