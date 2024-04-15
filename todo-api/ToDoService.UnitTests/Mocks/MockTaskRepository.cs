using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ToDoService.Application.Contracts.Persistence;
using ToDoService.Domain.Entities;

namespace ToDoService.UnitTests.Mocks;
public static class MockTaskRepository
{
    public static Mock<ITaskRepository> GetTaskRepository()
    {
        var tasks = new List<ToDoTask>
            {
                new ToDoTask
                {
                    Id = 1,
                    TaskName = "Task1Task122434",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                },
                new ToDoTask
                {
                    Id = 2,
                    TaskName = "Task1Task122434",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                },
                new ToDoTask
                {
                    Id = 3,
                    TaskName = "Task1Task122434",
                    StartDate = DateTime.Now,
                    EndDate = DateTime.Now,
                }
            };

        var mockRepo = new Mock<ITaskRepository>();

        _ = mockRepo.Setup(r => r.GetAll()).ReturnsAsync(tasks);

        _ = mockRepo.Setup(r => r.Add(It.IsAny<ToDoTask>())).ReturnsAsync((ToDoTask task) =>
        {
            tasks.Add(task);
            return task;
        });

        return mockRepo;
    }
}
