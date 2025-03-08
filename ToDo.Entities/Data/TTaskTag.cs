using System;
using System.Collections.Generic;

namespace ToDo.Api.Data;

public partial class TTaskTag
{
    public int Id { get; set; }

    public int TaskId { get; set; }

    public int TagId { get; set; }

    public virtual TTag Tag { get; set; } = null!;

    public virtual TTask Task { get; set; } = null!;
}
