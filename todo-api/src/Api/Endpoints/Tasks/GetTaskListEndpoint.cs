using MediatR;
using ToDoService.Application.Features.Requests.Queries;
using IMapper = AutoMapper.IMapper;

namespace ToDoService.Api.Endpoints.Tasks;

public class GetTaskListSummary : Summary<GetTaskListEndpoint>
{
    public GetTaskListSummary()
    {
        Summary = "Get list of task for ToDo List";
        Description =
            "This endpoint will fetch list of tasks.";
        Response(500, "Internal server error.");
    }
}

public class GetTaskListEndpoint : BaseEndpoint<GetTaskListRequest>
{
    public GetTaskListEndpoint(ISender mediator, IMapper mapper)
        : base(mediator, mapper) { }

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

    public override async Task HandleAsync(GetTaskListRequest req, CancellationToken ct)
    {
        var result = await Mediator.Send(
            new GetTaskListRequest
            {
            },
            ct
        );
        await SendOkAsync(result);
    }
}
