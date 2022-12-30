namespace StackOverflowProject.Entities;

public class Vote
{
    public int Id { get; set; }
    public int ValueOfVote { get; set; }
    public User User { get; set; }
    public int UserId { get; set; }
    public Entity Entity { get; set; }
    public int EntityId { get; set; } 
}