using Microsoft.AspNetCore.Mvc;
using TodoList.API.Controllers.Basics;
using TodoList.API.Services.TaskLists;

namespace TodoList.API.Controllers;

[Route("api/[controller]")]
public class TaskListsController(ITaskListsService service) : HttpController
{
    [HttpGet]
    public async Task<IActionResult> GetMany([FromQuery] Guid onBehalfOf, [FromQuery] int skip = 0, [FromQuery] int limit = 10,
        CancellationToken cancellationToken = default) =>
        AsActionResult(await service.GetManyTaskListsAsync(onBehalfOf, skip, limit, cancellationToken));
}