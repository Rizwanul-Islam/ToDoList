using MediatR;
using ToDoService.Application.Features.Requests.Commands;
using ToDoService.Application.Features.Requests.Queries;
using IMapper = AutoMapper.IMapper;

namespace ToDoService.Api.Endpoints.Tasks;

public class DeleteTaskSummary : Summary<DeleteTaskEndpoint>
{
    public DeleteTaskSummary()
    {
        Summary = "Delete Task frm ToDo List";
        Description =
            "This endpoint will delete task from ToDo List.";
        Response(500, "Internal server error.");
    }
}

public class DeleteTaskEndpoint : BaseEndpoint<GetTaskDetailRequest>
{
    public DeleteTaskEndpoint(ISender mediator, IMapper mapper)
        : base(mediator, mapper) { }

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
