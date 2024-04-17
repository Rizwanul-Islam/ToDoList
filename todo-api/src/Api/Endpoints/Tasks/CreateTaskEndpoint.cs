using MediatR;
using ToDoService.Application.DTOs;
using ToDoService.Application.Features.Requests.Commands;
using IMapper = AutoMapper.IMapper;

namespace ToDoService.Api.Endpoints.Tasks;

public class CreateTaskSummary : Summary<CreateTaskEndpoint>
{
    public CreateTaskSummary()
    {
        Summary = "Create Task for ToDo List";
        Description =
            "This endpoint will create task for ToDo List.";
        Response(500, "Internal server error.");
    }
}

public class CreateTaskEndpoint : BaseEndpoint<CreateTaskDto>
{
    public CreateTaskEndpoint(ISender mediator, IMapper mapper)
        : base(mediator, mapper) { }

    public override void Configure()
    {
        base.Configure();
        Post("t");
        AllowAnonymous();
        Description(
            d => d.WithTags("Task")
        );
        Summary(new CreateTaskSummary());
    }

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
