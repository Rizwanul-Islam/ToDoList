using MediatR;
using ToDoService.Application.DTOs;
using ToDoService.Application.Features.Requests.Commands;
using IMapper = AutoMapper.IMapper;

namespace ToDoService.Api.Endpoints.Tasks;

/// <summary>
/// Represents the summary for creating a task.
/// </summary>
public class CreateTaskSummary : Summary<CreateTaskEndpoint>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTaskSummary"/> class.
    /// </summary>
    public CreateTaskSummary()
    {
        Summary = "Create Task for ToDo List";
        Description = "This endpoint will create a task for the ToDo List.";
        Response(500, "Internal server error.");
    }
}

/// <summary>
/// Represents the endpoint for creating a task.
/// </summary>
public class CreateTaskEndpoint : BaseEndpoint<CreateTaskDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="CreateTaskEndpoint"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="mapper">The mapper.</param>
    public CreateTaskEndpoint(ISender mediator, IMapper mapper)
        : base(mediator, mapper) { }

    /// <inheritdoc />
    public override void Configure()
    {
        base.Configure();
        Post("task/create");
        AllowAnonymous();
        Description(
            d => d.WithTags("Task")
        );
        Summary(new CreateTaskSummary());
    }

    /// <inheritdoc />
    public override async Task HandleAsync(CreateTaskDto req, CancellationToken ct)
    {
        var result = await Mediator.Send(
            new CreateTaskCommand
            {
                CreateTaskDto = req
            },
            ct
        );
        await SendOkAsync(result);
    }
}
