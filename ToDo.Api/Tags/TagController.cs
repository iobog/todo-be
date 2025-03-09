


using AutoMapper;
using Azure;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using ToDo.Entities.Data;
using ToDo.Entities.DTOs.Tags;

[Route("api/tags")]
[ApiController]
public class TagController:ControllerBase
{
  private readonly AppDbContext _db;
  private readonly IMapper _mapper;

  public TagController(AppDbContext dbContext, IMapper mapper)
  {
    _db = dbContext;
    _mapper = mapper;
  }

  [HttpGet]
  public async Task<ActionResult<IEnumerable<GetTagDTO>>> GetAllTasks()
  {
    return Ok(_mapper.Map<IEnumerable<GetTagDTO>>(await _db.TTags.ToListAsync()));
  }

  [HttpGet("{id}",Name = "GetTagById")]
  public async Task<ActionResult<GetTagDTO>> GetTagById(int id)
  {
    try
    {
      var tag = await _db.TTags
        .Where(_ => _.Id == id)
        .FirstOrDefaultAsync();

      if (tag == null)
        return NotFound();

      return Ok(_mapper.Map<GetTagDTO>(tag));
    }
    catch (Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
    }
  }
  

  [HttpPost]
  public async Task<ActionResult<GetTagDTO>> CreateTag(CreateUpdateTagDTO createUpdateTagDTO)
  {
    try
    {
      var tagModel = _mapper.Map<TTag>(createUpdateTagDTO);
      await _db.AddAsync(tagModel);
      await _db.SaveChangesAsync();

      var tagRead = _mapper.Map<GetTagDTO>(tagModel);

      return CreatedAtRoute(nameof(GetTagById),new {Id = tagRead.Id},tagRead);

    }
    catch(Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError,e.Message);
    }
  }

  [HttpPut("{id}")]
  public async Task<ActionResult>UpdateTag(int id, CreateUpdateTagDTO createUpdateTagDTO)
  {
    try
    {
      var tag = await _db.TTags
        .Where(_ => _.Id == id)
        .FirstOrDefaultAsync();

      if (tag == null)
        return NotFound();

      _mapper.Map(createUpdateTagDTO,tag);
      _db.Update(tag);
      await _db.SaveChangesAsync();

      return NoContent();

    }
    catch (Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
    }
  }

  [HttpPatch("{id}")]
  public async Task<ActionResult> PartialTagUpdate(int id, JsonPatchDocument<CreateUpdateTagDTO> jsonPatchDocument)
  {
    try
    {
      var tag = await _db.TTags
        .Where(_ => _.Id == id)
        .FirstOrDefaultAsync();
      
      if(tag == null)
      {
        return NotFound();
      }

      var tagToPatch = _mapper.Map<CreateUpdateTagDTO>(tag);
      jsonPatchDocument.ApplyTo(tagToPatch,ModelState);

      if(!TryValidateModel(tagToPatch))
      {
        return ValidationProblem(ModelState);
      }

      _mapper.Map(tagToPatch,tag);
      _db.Update(tag);
      await _db.SaveChangesAsync();

      return NoContent();


    }
    catch (Exception e)
    {
      return StatusCode(StatusCodes.Status500InternalServerError, e.Message);
    }
  }

  [HttpDelete("{id}")]
  public async Task<ActionResult> DeleteTagAsync(int id)
  {
    var tag = await _db.TTags
      .Where(_ => _.Id == id)
      .FirstOrDefaultAsync();

    if(tag == null) 
    {
      return NotFound();
    }
    _db.Remove(tag);
    await _db.SaveChangesAsync();

    return NoContent();
  }

}