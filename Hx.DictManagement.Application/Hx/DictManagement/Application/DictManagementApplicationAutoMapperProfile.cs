using AutoMapper;
using Hx.DictManagement.Application.Contracts;
using Hx.DictManagement.Domain;

namespace Hx.DictManagement.Application
{
    public class DictManagementApplicationAutoMapperProfile : Profile
    {
        public DictManagementApplicationAutoMapperProfile()
        {
            CreateMap<DictType, DictTypeDto>();
            CreateMap<DictItem, DictItemDto>()
                .ForMember(dest => dest.Children, opt => opt.Ignore());
            CreateMap<DictTypeGroup, DictTypeGroupDto>();
        }
    }
}
