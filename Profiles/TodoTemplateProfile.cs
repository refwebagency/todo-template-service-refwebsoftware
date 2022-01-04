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
        }
    }
}