using System;
using System.Collections.Generic;

namespace ToDo.Entities.Data;

public partial class TTask
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public bool? IsCompleted { get; set; }

    public string? Notes { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? DeletedAt { get; set; }

    public virtual ICollection<TTaskTag> TTaskTags { get; set; } = new List<TTaskTag>();
}
