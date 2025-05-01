using Microsoft.AspNetCore.Mvc;
using TodoList.API.Contracts;
using TodoList.API.Controllers.Basics;
using TodoList.API.Managers.TaskLists;
using TodoList.API.Services.TaskLists.Contracts.Requests;

namespace TodoList.API.Controllers;

[Route("api/[controller]")]
public class TaskListsController(ITaskListsManager manager) : HttpController
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateRequest payload,
        CancellationToken cancellationToken = default) =>
        AsActionResult(await manager.CreateTaskListAsync(payload, cancellationToken));

    [HttpPut("{taskListId:guid}")]
    public async Task<IActionResult> Change([FromBody] ChangeTaskListRequest payload,
        [FromRoute] Guid taskListId, [FromQuery] Guid? onBehalfOf,
        CancellationToken cancellationToken = default) =>
        AsActionResult(await manager.ChangeTaskListAsync(new ChangeRequest()
        {
            OnBehalfOf = onBehalfOf,
            TaskListId = taskListId,
            Name = payload?.Name
        }, cancellationToken));
    
    [HttpDelete("{taskListId:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid taskListId, [FromQuery] Guid? onBehalfOf,
        CancellationToken cancellationToken = default) =>
        AsActionResult(await manager.RemoveTaskListAsync(new RemoveRequest()
        {
            OnBehalfOf = onBehalfOf,
            TaskListId = taskListId
        }, cancellationToken));
    
    [HttpGet]
    public async Task<IActionResult> GetMany([FromQuery] Guid? onBehalfOf, [FromQuery] int skip = 0, [FromQuery] int limit = 10,
        CancellationToken cancellationToken = default) =>
        AsActionResult(await manager.GetManyTaskListsAsync(new GetManyRequest()
        {
            OnBehalfOf = onBehalfOf,
            Limit = limit,
            Skip = skip
        }, cancellationToken));
    
    [HttpPatch("{taskListId:guid}/share")]
    public async Task<IActionResult> Share([FromQuery] Guid? shareWithId,
        [FromRoute] Guid taskListId, [FromQuery] Guid? onBehalfOf,
        CancellationToken cancellationToken = default) =>
        AsActionResult(await manager.ShareTaskListAsync(new ChangeTaskListAccessRequestModel()
        {
            Id = taskListId,
            OnBehalfOf = onBehalfOf,
            UserId = shareWithId
        }, cancellationToken));
    
    [HttpPatch("{taskListId:guid}/restrict")]
    public async Task<IActionResult> Restrict([FromQuery] Guid? restrictToId,
        [FromRoute] Guid taskListId, [FromQuery] Guid? onBehalfOf,
        CancellationToken cancellationToken = default) =>
        AsActionResult(await manager.RestrictTaskListAsync(new ChangeTaskListAccessRequestModel()
        {
            Id = taskListId,
            OnBehalfOf = onBehalfOf,
            UserId = restrictToId
        }, cancellationToken));
    
    [HttpGet("{taskListId:guid}/users")]
    public async Task<IActionResult> GetUsers([FromRoute] Guid taskListId, 
        [FromQuery] Guid? onBehalfOf,
        CancellationToken cancellationToken = default) =>
        AsActionResult(await manager.GetTaskListUsersAsync(new GetOneRequest()
        {
            OnBehalfOf = onBehalfOf,
            TaskListId = taskListId
        }, cancellationToken));
}