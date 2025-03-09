namespace ToDo.Entities.DTOs.TaskTags;




public class GetTaskTagsDTO
{
  public int Id { get; set; } 
  public string Title { get; set; } = string.Empty; 
  public string? Description { get; set; } 
  public DateTime? CreatedAt { get; set; } 
  public string TagName { get; set; } = string.Empty;
}


