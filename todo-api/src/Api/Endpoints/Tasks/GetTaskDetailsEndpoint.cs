using MediatR;
using ToDoService.Application.Features.Requests.Queries;
using IMapper = AutoMapper.IMapper;

namespace ToDoService.Api.Endpoints.Tasks;

public class GetTaskDetailsSummary : Summary<GetTaskDetailsEndpoint>
{
    public GetTaskDetailsSummary()
    {
        Summary = "Fetch task details";
        Description =
            "This endpoint will fetch task details";
        Response(500, "Internal server error.");
    }
}

public class GetTaskDetailsEndpoint : BaseEndpoint<GetTaskDetailRequest>
{
    public GetTaskDetailsEndpoint(ISender mediator, IMapper mapper)
        : base(mediator, mapper) { }

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
