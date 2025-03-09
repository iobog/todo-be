using System.Runtime.Versioning;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ToDo.Entities.Data;
using ToDo.Entities.DTOs.Tasks;
using Route = Microsoft.AspNetCore.Mvc.RouteAttribute;

namespace ToDo.Api.Tasks;


[Route("api/tasks")]
[ApiController]
public class TaskController:ControllerBase
{
  private readonly AppDbContext _db;
  private readonly IMapper _mapper;

  public TaskController(AppDbContext db, IMapper mapper)
  {
    _db = db;
    _mapper = mapper;
    
  }


  [HttpGet]
  public async Task<ActionResult<IEnumerable<GetTaskDTO>>>GetAllTasks()
  {
    var tasks = await _db.TTasks
      .ProjectTo<GetTaskDTO>(_mapper.ConfigurationProvider)
      .ToListAsync();
    return Ok(tasks);
  }

  [HttpGet("{id}", Name = "GetTaskById")]
  public async Task<ActionResult<GetTaskDTO>>GetTaskById(int id)
  {
    try
    {
      var task = await _db.TTasks
        .Where(_ => _.Id ==id)
        .FirstOrDefaultAsync();
      
      if(task == null)
      {
        return NotFound();
      }
      return Ok(_mapper.Map<GetTaskDTO>(task)); 

    }
    catch(Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
    }
  }

  [HttpPost]
  public async Task<ActionResult<GetTaskDTO>>CreateTask(CreateUpdateTaskDTO createUpdateTaskDto)
  {
    try
    {
      var taskModel = _mapper.Map<TTask>(createUpdateTaskDto);
      await _db.AddAsync(taskModel);
      await _db.SaveChangesAsync();

      var getTaskDTO = _mapper.Map<GetTaskDTO>(taskModel);
      return CreatedAtRoute(nameof(GetTaskById),new{Id = getTaskDTO.Id},getTaskDTO);
    }
    catch(Exception e)
    {
     return StatusCode(StatusCodes.Status500InternalServerError, e.Message); 
    }
  }

  [HttpPut("{id}")]
  public async Task<ActionResult> UpdateTaskAsync(int id, CreateUpdateTaskDTO createUpdateTaskDto)
  {
    try
    {
      var task = await _db.TTasks
        .Where(_ => _.Id == id)
        .FirstOrDefaultAsync();

      if(task == null) 
      {
        return NotFound();
      }

      _mapper.Map(createUpdateTaskDto,task);
      _db.Update(task);
      await _db.SaveChangesAsync();
      return NoContent();
    }
    catch(Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError,e.Message);
    }

  }
  //PATCH api/commands/{id}
  [HttpPatch("{id}")]
  public async Task<ActionResult> PartialTaskUpdateAsync(int id,JsonPatchDocument<CreateUpdateTaskDTO> patchDoc)
  {
    try
    {
      var taskModelFromDb = await _db.TTasks
        .Where(_ => _.Id == id)
        .FirstOrDefaultAsync();

      if(taskModelFromDb == null) 
      {
        return NotFound();
      }

      var taskToPatch = _mapper.Map<CreateUpdateTaskDTO>(taskModelFromDb);
      patchDoc.ApplyTo(taskToPatch,ModelState);

      if(!TryValidateModel(taskToPatch))
      {
        return ValidationProblem(ModelState);
      }

      _mapper.Map(taskToPatch,taskModelFromDb);
      _db.Update(taskModelFromDb);
      await _db.SaveChangesAsync();

      return NoContent();
    }
    catch(Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError,e.Message);
    }
  }


  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteTaskAsync(int id)
  {
    var taskModelFromDb = await _db.TTasks
      .Where(_ => _.Id == id)
      .FirstOrDefaultAsync();

    if(taskModelFromDb == null) 
    {
      return NotFound();
    }
    _db.Remove(taskModelFromDb);
    await _db.SaveChangesAsync();

    return NoContent();

  }

}