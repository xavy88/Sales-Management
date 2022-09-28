﻿using AutoMapper;
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
        }
    }
}
