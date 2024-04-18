using MediatR;
using ToDoService.Application.DTOs;
using ToDoService.Application.Features.Requests.Commands;
using IMapper = AutoMapper.IMapper;

namespace ToDoService.Api.Endpoints.Tasks;

public class UpdateTaskSummary : Summary<UpdateTaskEndpoint>
{
    public UpdateTaskSummary()
    {
        Summary = "Update task details";
        Description =
            "This endpoint will update task details.";
        Response(500, "Internal server error.");
    }
}

public class UpdateTaskEndpoint : BaseEndpoint<UpdateTaskDto>
{
    public UpdateTaskEndpoint(ISender mediator, IMapper mapper)
        : base(mediator, mapper) { }

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
