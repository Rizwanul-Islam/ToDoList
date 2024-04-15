using AutoMapper;
using ToDoService.Application.DTOs;
using ToDoService.Domain.Entities;

namespace ToDoService.Application.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        _ = CreateMap<ToDoTask, TaskDto>().ReverseMap();
    }
}
