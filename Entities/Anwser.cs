namespace StackOverflowProject.Entities;

public class Answer : Entity
{
    public int QuestionId { get; set; }
    public Question Question { get; set; }
}