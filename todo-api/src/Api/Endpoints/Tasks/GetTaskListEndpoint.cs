using MediatR;
using ToDoService.Application.Features.Requests.Queries;
using IMapper = AutoMapper.IMapper;

namespace ToDoService.Api.Endpoints.Tasks;

/// <summary>
/// Represents the summary for getting a list of tasks.
/// </summary>
public class GetTaskListSummary : Summary<GetTaskListEndpoint>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetTaskListSummary"/> class.
    /// </summary>
    public GetTaskListSummary()
    {
        Summary = "Get list of tasks for ToDo List";
        Description = "This endpoint will fetch a list of tasks.";
        Response(500, "Internal server error.");
    }
}

/// <summary>
/// Represents the endpoint for getting a list of tasks.
/// </summary>
public class GetTaskListEndpoint : BaseEndpoint<GetTaskListRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetTaskListEndpoint"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="mapper">The mapper.</param>
    public GetTaskListEndpoint(ISender mediator, IMapper mapper)
        : base(mediator, mapper) { }

    /// <inheritdoc />
    public override void Configure()
    {
        base.Configure();
        Get("task/list");
        AllowAnonymous();
        Description(
            d => d.WithTags("Task")
        );
        Summary(new GetTaskListSummary());
    }

    /// <inheritdoc />
    public override async Task HandleAsync(GetTaskListRequest req, CancellationToken ct)
    {
        var result = await Mediator.Send(
            new GetTaskListRequest(),
            ct
        );
        await SendOkAsync(result);
    }
}
