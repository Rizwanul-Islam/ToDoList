using MediatR;
using ToDoService.Application.DTOs;
using ToDoService.Application.Features.Requests.Commands;
using IMapper = AutoMapper.IMapper;

namespace ToDoService.Api.Endpoints.Tasks;

/// <summary>
/// Represents the summary for updating task details.
/// </summary>
public class UpdateTaskSummary : Summary<UpdateTaskEndpoint>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTaskSummary"/> class.
    /// </summary>
    public UpdateTaskSummary()
    {
        Summary = "Update task details";
        Description = "This endpoint will update task details.";
        Response(500, "Internal server error.");
    }
}

/// <summary>
/// Represents the endpoint for updating task details.
/// </summary>
public class UpdateTaskEndpoint : BaseEndpoint<UpdateTaskDto>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateTaskEndpoint"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="mapper">The mapper.</param>
    public UpdateTaskEndpoint(ISender mediator, IMapper mapper)
        : base(mediator, mapper) { }

    /// <inheritdoc />
    public override void Configure()
    {
        base.Configure();
        Put("task/update/{Id}");
        AllowAnonymous();
        Description(
            d => d.WithTags("Task")
        );
        Summary(new UpdateTaskSummary());
    }

    /// <inheritdoc />
    public override async Task HandleAsync(UpdateTaskDto req, CancellationToken ct)
    {
        var result = await Mediator.Send(
            new UpdateTaskCommand
            {
                updateTaskDto = req
            },
            ct
        );
        await SendNoContentAsync();
    }
}
