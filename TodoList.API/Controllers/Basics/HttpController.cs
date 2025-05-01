using Microsoft.AspNetCore.Mvc;
using TodoList.API.Models.ResponseData;
using TodoList.API.Models.ServiceResponse;

namespace TodoList.API.Controllers.Basics;

public class HttpController : Controller
{
    protected IActionResult AsActionResult<T>(ServiceResponse<(T data, bool hasNext)> response)
    {
        if (!response.IsSuccess)
            return BadRequest(new ResponseData()
            {
                Errors = response.Errors
            });
        
        return Ok(new ResponseData<T>()
        {
            Data = response.Result.data,
            Meta = new Meta()
            {
                HasNext = response.Result.hasNext
            }
        });
    }
    
    protected IActionResult AsActionResult<T>(ServiceResponse<T> response)
    {
        if (!response.IsSuccess)
            return BadRequest(new ResponseData()
            {
                Errors = response.Errors
            });
        
        return Ok(new ResponseData<T>()
        {
            Data = response.Result
        });
    }
    
    protected IActionResult AsActionResult(ServiceResponse response)
    {
        if (!response.IsSuccess)
            return BadRequest(new ResponseData()
            {
                Errors = response.Errors
            });
        
        return Ok();
    }
}