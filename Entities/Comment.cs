namespace StackOverflowProject.Entities;

public class Comment : Entity
{
    public int? ParentId { get; set; }
    public Entity Parent { get; set; }
}