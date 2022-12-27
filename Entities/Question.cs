namespace StackOverflowProject.Entities;


public class Question : Entity
{
    public string Title { get; set; }
    public List<Tag> Tags { get; set; }
}