namespace StackOverflowProject.Entities;

public abstract class Entity
{
    public int Id { get; set; }
    public string Body { get; set; }
    public DateTime DateCreated { get; set; }
    public DateTime? Edited { get; set; }
    public int Votes { get; set; }
    public User Author { get; set; } // Referencja do Usera
    public int AuthorId { get; set; } // Klucz obcy do Usera
}