using MediatR;
using ToDoService.Application.Features.Requests.Commands;
using ToDoService.Application.Features.Requests.Queries;
using IMapper = AutoMapper.IMapper;

namespace ToDoService.Api.Endpoints.Tasks;

/// <summary>
/// Represents the summary for deleting a task.
/// </summary>
public class DeleteTaskSummary : Summary<DeleteTaskEndpoint>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteTaskSummary"/> class.
    /// </summary>
    public DeleteTaskSummary()
    {
        Summary = "Delete Task from ToDo List";
        Description = "This endpoint will delete a task from the ToDo List.";
        Response(500, "Internal server error.");
    }
}

/// <summary>
/// Represents the endpoint for deleting a task.
/// </summary>
public class DeleteTaskEndpoint : BaseEndpoint<GetTaskDetailRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DeleteTaskEndpoint"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="mapper">The mapper.</param>
    public DeleteTaskEndpoint(ISender mediator, IMapper mapper)
        : base(mediator, mapper) { }

    /// <inheritdoc />
    public override void Configure()
    {
        base.Configure();
        Delete("task/delete/{Id}");
        AllowAnonymous();
        Description(
            d => d.WithTags("Task")
        );
        Summary(new DeleteTaskSummary());
    }

    /// <inheritdoc />
    public override async Task HandleAsync(GetTaskDetailRequest request, CancellationToken ct)
    {
        var result = await Mediator.Send(
            new DeleteTaskCommand
            {
                Id = request.Id
            },
            ct
        );
        await SendNoContentAsync();
    }
}
