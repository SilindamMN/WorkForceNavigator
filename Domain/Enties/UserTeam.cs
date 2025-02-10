namespace Domain.Enties.TimeSheets
{
  using Domain.Account;
  using Domain.Entities;

  public class UserTeam 
  {
    public int Id { get; set; }
    public string UserId { get; set; }
    public ApplicationUser User { get; set; }

    public int TeamId { get; set; }
    public Team Team { get; set; }
  }
}