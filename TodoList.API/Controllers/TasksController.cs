using Microsoft.AspNetCore.Mvc;
using TodoList.API.Contracts;
using TodoList.API.Controllers.Basics;
using TodoList.API.Managers.Tasks;
using TodoList.API.Services.Tasks.Contracts;

namespace TodoList.API.Controllers;

public class TasksController(ITasksManager manager) : HttpController
{
    [HttpGet("api/taskLists/{taskListId:guid}")]
    public async Task<IActionResult> GetMany([FromRoute] Guid taskListId, [FromQuery] Guid onBehalfOf,
        CancellationToken cancellationToken = default) =>
        AsActionResult(await manager.GetManyAsync(taskListId, onBehalfOf, cancellationToken));
    
    [HttpPatch("api/tasks/{taskId:guid}/complete")]
    public async Task<IActionResult> Complete([FromRoute] Guid taskId, [FromQuery] Guid onBehalfOf,
        CancellationToken cancellationToken = default) =>
        AsActionResult(await manager.CompleteTaskAsync(taskId, onBehalfOf, cancellationToken));
    
    [HttpPatch("api/tasks/{taskId:guid}/incomplete")]
    public async Task<IActionResult> InComplete([FromRoute] Guid taskId, [FromQuery] Guid onBehalfOf,
        CancellationToken cancellationToken = default) =>
        AsActionResult(await manager.InCompleteTaskAsync(taskId, onBehalfOf, cancellationToken));
    
    [HttpPost("api/taskLists/{taskListId:guid}")]
    public async Task<IActionResult> Create([FromRoute] Guid taskListId, [FromQuery] Guid onBehalfOf,
        [FromBody] ChangeTaskListRequest? payload,
        CancellationToken cancellationToken = default) =>
        AsActionResult(await manager.CreateAsync(new CreateTaskRequest()
        {
            TaskListId = taskListId,
            Name = payload?.Name,
            OnBehalfOf = onBehalfOf
        }, cancellationToken));
}