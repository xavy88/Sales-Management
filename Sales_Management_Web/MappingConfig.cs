using AutoMapper;
using Sales_Management_Web.Model.DTO;

namespace Sales_Management_Web
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
           CreateMap<DepartmentDTO, DepartmentCreateDTO>().ReverseMap();
            CreateMap<DepartmentDTO, DepartmentUpdateDTO>().ReverseMap();

     
            CreateMap<ServiceDTO, ServiceCreateDTO>().ReverseMap();
            CreateMap<ServiceDTO, ServiceUpdateDTO>().ReverseMap();

        }
    }
}
