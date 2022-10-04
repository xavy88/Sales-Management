using AutoMapper;
using Sales_Management_API.Model;
using Sales_Management_API.Model.DTO;

namespace Sales_Management_API
{
    public class MappingConfig : Profile
    {
        public MappingConfig()
        {
            CreateMap<Department, DepartmentDTO>();
            CreateMap<DepartmentDTO, Department>();
            CreateMap<Department, DepartmentCreateDTO>().ReverseMap();
            CreateMap<Department, DepartmentUpdateDTO>().ReverseMap();

            CreateMap<Service, ServiceDTO>().ReverseMap();
            CreateMap<Service, ServiceCreateDTO>().ReverseMap();
            CreateMap<Service, ServiceUpdateDTO>().ReverseMap();

            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<Employee, EmployeeCreateDTO>().ReverseMap();
            CreateMap<Employee, EmployeeUpdateDTO>().ReverseMap();

            CreateMap<Client, ClientDTO>().ReverseMap();
            CreateMap<Client, ClientCreateDTO>().ReverseMap();
            CreateMap<Client, ClientUpdateDTO>().ReverseMap();
        }
    }
}
