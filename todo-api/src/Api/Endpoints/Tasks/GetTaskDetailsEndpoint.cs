using MediatR;
using ToDoService.Application.Features.Requests.Queries;
using IMapper = AutoMapper.IMapper;

namespace ToDoService.Api.Endpoints.Tasks;

/// <summary>
/// Represents the summary for fetching task details.
/// </summary>
public class GetTaskDetailsSummary : Summary<GetTaskDetailsEndpoint>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetTaskDetailsSummary"/> class.
    /// </summary>
    public GetTaskDetailsSummary()
    {
        Summary = "Fetch task details";
        Description = "This endpoint will fetch task details";
        Response(500, "Internal server error.");
    }
}

/// <summary>
/// Represents the endpoint for fetching task details.
/// </summary>
public class GetTaskDetailsEndpoint : BaseEndpoint<GetTaskDetailRequest>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="GetTaskDetailsEndpoint"/> class.
    /// </summary>
    /// <param name="mediator">The mediator.</param>
    /// <param name="mapper">The mapper.</param>
    public GetTaskDetailsEndpoint(ISender mediator, IMapper mapper)
        : base(mediator, mapper) { }

    /// <inheritdoc />
    public override void Configure()
    {
        base.Configure();
        Get("task/details/{Id}");
        AllowAnonymous();
        Description(
            d => d.WithTags("Task")
        );
        Summary(new GetTaskDetailsSummary());
    }

    /// <inheritdoc />
    public override async Task HandleAsync(GetTaskDetailRequest request, CancellationToken ct)
    {
        var result = await Mediator.Send(
            new GetTaskDetailRequest
            {
                Id = request.Id
            },
            ct
        );
        await SendOkAsync(result);
    }
}
