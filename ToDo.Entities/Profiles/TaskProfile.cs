

using AutoMapper;
using ToDo.Entities.Data;
using ToDo.Entities.DTOs.Tasks;

namespace ToDo.Entities.Profiles;

public class TaskProfile : Profile
{
  public TaskProfile()
  {
    CreateMap<TTask,GetTaskDTO>();
    CreateMap<TTask,CreateUpdateTaskDTO>();
    CreateMap<CreateUpdateTaskDTO,TTask>();
  }
}