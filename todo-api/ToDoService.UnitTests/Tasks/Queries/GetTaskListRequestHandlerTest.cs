using AutoMapper;
using Moq;
using Shouldly;
using ToDoService.Application.Contracts.Persistence;
using ToDoService.Application.DTOs;
using ToDoService.Application.Features.Handlers.Queries;
using ToDoService.Application.Features.Requests.Queries;
using ToDoService.Application.Profiles;
using ToDoService.UnitTests.Mocks;
using Xunit;
namespace ToDoService.UnitTests;

public class GetTaskListRequestHandlerTest
{
    private readonly IMapper _mapper;
    private readonly Mock<ITaskRepository> _mockRepo;
    public GetTaskListRequestHandlerTest()
    {
        _mockRepo = MockTaskRepository.GetTaskRepository();

        var mapperConfig = new MapperConfiguration(c =>
        {
            c.AddProfile<MappingProfile>();
        });

        _mapper = mapperConfig.CreateMapper();
    }

    [Fact]
    public async Task GetTaskListTest()
    {
        var handler = new GetTaskListRequestHandler(_mockRepo.Object, _mapper);

        var result = await handler.Handle(new GetTaskListRequest(), CancellationToken.None);

        _ = result.ShouldBeOfType<List<TaskDto>>();

        result.Count.ShouldBe(3);
    }
}
