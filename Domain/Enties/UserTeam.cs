namespace Domain.Enties.TimeSheets
{
  using Domain.Account;
  using Domain.Entities;

  public class UserTeam : BaseEntity<int>
    {
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int TeamId { get; set; }
    public Team Team { get; set; }
  }
}