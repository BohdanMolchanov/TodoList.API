using Microsoft.AspNetCore.Mvc;
using TodoList.API.Controllers.Basics;
using TodoList.API.Services.Tasks;

namespace TodoList.API.Controllers;

public class TasksController(ITasksService service) : HttpController
{
    [HttpGet("taskLists/{taskListId:guid}")]
    public async Task<IActionResult> GetMany([FromRoute] Guid taskListId, [FromQuery] Guid onBehalfOf,
        CancellationToken cancellationToken = default) =>
        AsActionResult(await service.GetManyAsync(taskListId, onBehalfOf, cancellationToken));
    
    [HttpPatch("tasks/{taskId:guid}/complete")]
    public async Task<IActionResult> Complete([FromRoute] Guid taskId, [FromQuery] Guid onBehalfOf,
        CancellationToken cancellationToken = default) =>
        AsActionResult(await service.CompleteTaskAsync(taskId, onBehalfOf, cancellationToken));
    
    [HttpPatch("tasks/{taskId:guid}/incomplete")]
    public async Task<IActionResult> InComplete([FromRoute] Guid taskId, [FromQuery] Guid onBehalfOf,
        CancellationToken cancellationToken = default) =>
        AsActionResult(await service.InCompleteTaskAsync(taskId, onBehalfOf, cancellationToken));
}