using AutoMapper;
using ToDoService.Application.DTOs;
using ToDoService.Domain.Entities;

namespace ToDoService.Application.Profiles;
public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UpdateTaskDto, ToDoTask>()
        .ForAllMembers(opts => opts.Condition((src, dest, srcMember) => srcMember != null));

        _ = CreateMap<ToDoTask, TaskDto>().ReverseMap();
    }
}
