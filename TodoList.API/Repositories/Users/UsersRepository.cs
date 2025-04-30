using Microsoft.EntityFrameworkCore;
using TodoList.API.Data;
using TodoList.API.Extensions;
using TodoList.API.Models;

namespace TodoList.API.Repositories.Users;

public class UsersRepository(DbContextOptions<TodoListContext> contextOptions) : IUsersRepository
{
    public async Task CreateUserAsync(UserModel model, CancellationToken cancellationToken)
    {
        await using var context = new TodoListContext(contextOptions);
        context.Users.Add(model.ToEntity());
        await context.SaveChangesAsync(cancellationToken);
    }

    public async Task<List<UserModel>> GetUsersAsync(CancellationToken cancellationToken)
    {
        await using var context = new TodoListContext(contextOptions);
        var result = await context.Users.ToListAsync(cancellationToken);
        return result.Select(u => u.ToModel()).ToList();
    }
}