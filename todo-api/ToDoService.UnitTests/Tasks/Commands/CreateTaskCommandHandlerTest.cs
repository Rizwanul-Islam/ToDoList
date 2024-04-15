using AutoMapper;
using Moq;
using Shouldly;
using ToDoService.Application.Contracts.Persistence;
using ToDoService.Application.DTOs;
using ToDoService.Application.Features.Handlers.Commands;
using ToDoService.Application.Features.Requests.Commands;
using ToDoService.Application.Profiles;
using ToDoService.Application.Responses;
using ToDoService.Domain.Entities;
using ToDoService.UnitTests.Mocks;
using Xunit;
namespace ToDoService.UnitTests;

public class CreateTaskCommandHandlerTest
{
    private readonly IMapper _mapper;
    private readonly TaskDto _taskDto;
    private readonly Mock<ITaskRepository> _mockUow;
    private readonly CreateTaskCommandHandler _handler;

    public CreateTaskCommandHandlerTest()
    {
        _mockUow = MockTaskRepository.GetTaskRepository();
        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
        _handler = new CreateTaskCommandHandler(_mockUow.Object, _mapper);

        _taskDto = new TaskDto
        {
            TaskName = "Test DTO Task",
            StartDate = DateTime.Now,
            EndDate = DateTime.Now
        };
    }

    [Fact]
    public async Task Valid_Task_Added()
    {
        var result = await _handler.Handle(new CreateTaskCommand() { CreateTaskDto = _taskDto }, CancellationToken.None);

        var tasks = await _mockUow.Object.GetAll();

        _ = result.ShouldBeOfType<BaseCommandResponse>();

        tasks.Count.ShouldBe(4);
    }

    [Fact]
    public async Task InValid_Task_Added()
    {
        var result = await _handler.Handle(new CreateTaskCommand() { CreateTaskDto = _taskDto }, CancellationToken.None);

        var tasks = await _mockUow.Object.GetAll();

        _ = result.ShouldBeOfType<BaseCommandResponse>();

        tasks.Count.ShouldBe(4);
    }

}
