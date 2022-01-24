using AutoMapper;
using TodoTemplateService.Models;
using TodoTemplateService.Dtos;

namespace TodoTemplateService.Profiles
{
    public class TodoTemplateProfile : Profile
    {
        public TodoTemplateProfile()
        {
            CreateMap<TodoTemplate, TodoTemplateReadDto>();
            CreateMap<TodoTemplateCreateDto, TodoTemplate>();
            CreateMap<TodoTemplateUpdateDto, TodoTemplate>();

            CreateMap<Specialization, ReadSpecializationDTO>();
            CreateMap<CreateSpecializationDTO,Specialization>();
            CreateMap<UpdateSpecializationDTO, Specialization>();

            CreateMap<ProjectType, ProjectTypeReadDto>();
            CreateMap<ProjectTypeCreateDto, ProjectType>();

            CreateMap<ProjectType, ProjectTypeUpdateDto>();
            CreateMap<ProjectTypeUpdateDto, ProjectType>();

            CreateMap<Specialization, UpdateSpecializationAsyncDTO>();
            CreateMap<UpdateSpecializationAsyncDTO, Specialization>();
        }
    }
}