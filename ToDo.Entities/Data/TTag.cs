using System;
using System.Collections.Generic;

namespace ToDo.Entities.Data;

public partial class TTag
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<TTaskTag> TTaskTags { get; set; } = new List<TTaskTag>();
}
