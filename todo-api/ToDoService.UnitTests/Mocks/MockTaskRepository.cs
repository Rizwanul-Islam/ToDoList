using AutoMapper;
using Moq;
using ToDoService.Application.Contracts.Persistence;
using ToDoService.Application.DTOs;
using ToDoService.Application.Profiles;
using ToDoService.Domain.Entities;

namespace ToDoService.UnitTests.Mocks;

public class MockTaskRepository
{
    private readonly IMapper _mapper;

    public MockTaskRepository()
    {
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    public Mock<ITaskRepository> GetTaskRepository()
    {
        var tasks = new List<ToDoTask>
        {
            CreateTaskFromDto(new TaskDto
            {
                TaskName = "Task1kdfhkjds",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(7)
            }),
            CreateTaskFromDto(new TaskDto
            {
                TaskName = "Task1sdjfhd",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14)
            }),
            CreateTaskFromDto(new TaskDto
            {
                TaskName = "Task1skdhfk",
                StartDate = DateTime.Now,
                EndDate = DateTime.Now.AddDays(14)
            })
        };

        var mockRepo = new Mock<ITaskRepository>();

        _ = mockRepo.Setup(r => r.GetAll()).ReturnsAsync(tasks);

        _ = mockRepo.Setup(r => r.Add(It.IsAny<ToDoTask>())).ReturnsAsync((ToDoTask task) => { tasks.Add(task); return task; });

        return mockRepo;
    }

    private ToDoTask CreateTaskFromDto(TaskDto taskDto)
    {
        return _mapper.Map<ToDoTask>(taskDto);
    }
}
