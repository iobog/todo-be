

using AutoMapper;
using ToDo.Entities.Data;
using ToDo.Entities.DTOs.Tasks;

namespace ToDo.Entities.Profiles;

public class TaskProfile : Profile
{
  public TaskProfile()
  {
    CreateMap<TTask,GetTaskDTO>()
    .ForMember(
      destinationMember => destinationMember.Tags,
      memberOptions => memberOptions.MapFrom(
      sourceMember => sourceMember.TTaskTags.Select(_ => _.Tag)
      )
    );

    CreateMap<CreateUpdateTaskDTO,TTask>();
  }
}