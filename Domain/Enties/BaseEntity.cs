namespace Domain.Entities
{
  using System;
  using System.Collections.Generic;
  using System.ComponentModel.DataAnnotations.Schema;
  using System.Linq;
  using System.Text;
  using System.Threading.Tasks;

  public class BaseEntity<TID>
  {
    public TID Id { get; set; }

    [Column(TypeName = "Date")]
    public DateTime CreatedAt  { get; set; }

    [Column(TypeName = "Date")]
    public DateTime UpdatedAt { get; set; }

    public bool IsActive { get; set; } = true;

    public bool IsDeleted { get; set; } = false;
  }
}