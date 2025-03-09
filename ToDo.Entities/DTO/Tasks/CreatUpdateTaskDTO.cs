namespace ToDo.Entities.DTOs.Tasks;

public class CreateUpdateTaskDTO
{
  public string Title { get; set; } = null!;

  public string? Description { get; set; }

  public bool? IsCompleted { get; set; }

  public string? Notes { get; set; }

  public DateTime? DeletedAt { get; set; }

}