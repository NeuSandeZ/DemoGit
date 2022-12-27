namespace StackOverflowProject.Entities;

public class Tag
{
    public int Id { get; set; }
    public string Value { get; set; }

    public List<Question> Questions { get; set; }
}