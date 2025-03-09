using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Entities.Data;

namespace ToDo.Api.Tasks;


[ApiController]
[Route("api")]
public class TaskTagController : ControllerBase
{
  private readonly AppDbContext _db;

  public TaskTagController(AppDbContext db)
  {
    _db = db;
  }

  [HttpPost]
  [Route("tasks/{taskId}/tags/{tagId}")]
  public async Task<IActionResult> AddTaskTagAsync(int taskId, int tagId)
  {
    try
    {
      var taskTag = await _db.TTaskTags
        .Where(_ => _.TaskId == taskId && _.TagId == tagId)
        .FirstOrDefaultAsync();

      if (taskTag != null)
      {
        return Conflict("Exista deja");
      }

      taskTag = new TTaskTag()
      {
        TaskId = taskId,
        TagId = tagId
      };

      await _db.TTaskTags.AddAsync(taskTag);
      await _db.SaveChangesAsync();

      return NoContent();
    }
    catch (Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
    }
  }

  [HttpDelete]
  [Route("tasks/{taskId}/tags/{tagId}")]
  public async Task<IActionResult> RemoveTaskTagAsync(int taskId, int tagId)
  {
    try
    {
      var taskTag = await _db.TTaskTags
        .Where(_ => _.TaskId == taskId && _.TagId == tagId)
        .FirstOrDefaultAsync();

      if (taskTag == null)
      {
        return NotFound();
      }

      _db.TTaskTags.Remove(taskTag);
      await _db.SaveChangesAsync();

      return NoContent();
    }
    catch (Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
    }
  }
}