using ToDo.Entities.DTOs.Tags;

namespace ToDo.Entities.DTOs.Tasks;

public class GetTaskDTO
{
  public GetTaskDTO()
  {
    Tags = new List<GetTagDTO>();
  }

  public int Id { get; set; }

  public string Title { get; set; } = string.Empty;

  public string? Description { get; set; }

  public string? Notes { get; set; }

  public DateTime? CreatedAt { get; set; }
    
  public List<GetTagDTO> Tags { get; set; }


}