using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TodoList.API.Controllers.Basics;
using TodoList.API.Services.Requests;
using TodoList.API.Services.Users;

namespace TodoList.API.Controllers;

[Route("api/[controller]")]
public class UsersController(IUsersService service) : HttpController
{
    [HttpPost]
    public async Task<IActionResult> CreateUser([FromBody] CreateUserRequestModel request,
        CancellationToken cancellationToken = default) =>
        AsActionResult(await service.CreateUserAsync(request, cancellationToken));

    [HttpGet]
    public async Task<IActionResult> GetUsers(CancellationToken cancellationToken = default) => AsActionResult(await service.GetUsersAsync(cancellationToken));
}