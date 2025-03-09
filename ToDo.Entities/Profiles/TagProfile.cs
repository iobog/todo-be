using AutoMapper;
using ToDo.Entities.Data;
using ToDo.Entities.DTOs.Tags;
using ToDo.Entities.DTOs.Tasks;

namespace ToDo.Entities.Profiles;


public class TagProfile : Profile
{
  public TagProfile()
  {
    CreateMap<TTag,GetTagDTO>();
    CreateMap<CreateUpdateTagDTO,TTag>();
    CreateMap<TTag,CreateUpdateTagDTO>();
  }

}