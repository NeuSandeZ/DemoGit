using Bogus;
using StackOverflowProject.Entities;

namespace StackOverflowProject;

public static class DataGenerator
{
    public static void Seed(this StackOverflowContext context)
    {
        var random = new Random();

        var tags = new Faker<Tag>()
            .RuleFor(t => t.Value, f => f.Lorem.Word())
            .Generate(10);

        context.AddRange(tags);
        context.SaveChanges();


        var users = new Faker<User>()
            .RuleFor(u => u.FullName, f => f.Person.FullName)
            .RuleFor(u => u.Email, f => f.Person.Email)
            .Generate(10);

        context.AddRange(users);
        context.SaveChanges();


        var questions = new Faker<Question>()
            .RuleFor(q => q.Title, f => f.Lorem.Sentence())
            .RuleFor(q => q.Body, f => f.Lorem.Paragraph())
            .RuleFor(q => q.DateCreated, f => DateTime.UtcNow)
            .RuleFor(q => q.AuthorId, f => f.Random.Number(1, context.Users.Count()))
            .RuleFor(q => q.Tags, f => f.PickRandom<List<Tag>>(context.Tags.Take(random.Next(1, 10)).ToList()))
            .Generate(10);

        context.AddRange(questions);
        context.SaveChanges();
        
        
        var anwsers = new Faker<Answer>()
            .RuleFor(a => a.Body, f => f.Lorem.Paragraph())
            .RuleFor(a => a.DateCreated, f => DateTime.UtcNow)
            .RuleFor(a => a.QuestionId, f => f.Random.Number(1, context.Questions.Count()))
            .RuleFor(a => a.AuthorId, f => f.Random.Number(1, context.Users.Count()))
            .Generate(20);

        context.AddRange(anwsers);
        context.SaveChanges();


        var comments = new Faker<Comment>()
            .RuleFor(c => c.Body, f => f.Lorem.Sentence())
            .RuleFor(c => c.DateCreated, f => DateTime.UtcNow)
            .RuleFor(c => c.ParentId, f => f.Random.Number(1, context.Questions.Count() + context.Answers.Count()))
            .RuleFor(c => c.AuthorId, f => f.Random.Number(1, context.Users.Count()))
            .Generate(30);

        context.AddRange(comments);
        context.SaveChanges();

        // Generuje powielone wpisy do bazy (jeden uzytkownik oddal glos dwa razy na to samo pytanie, mozna zmienic)
        var votesOfQuestions = new Faker<Vote>()
            .RuleFor(vote => vote.ValueOfVote, faker => faker.PickRandom(-1,0,1))
            .RuleFor(vote => vote.UserId, faker => faker.Random.Number(1, context.Users.Count()))
            .RuleFor(vote => vote.EntityId, faker => faker.PickRandom(1, context.Questions.Count()))
            .Generate(10);
        
        context.AddRange(votesOfQuestions);
        context.SaveChanges();
        
        // Generuje powielone wpisy do bazy (jeden uzytkownik oddal glos dwa razy na to samo pytanie)
        var votesOfAnswers = new Faker<Vote>()
            .RuleFor(vote => vote.ValueOfVote, faker => faker.PickRandom(-1,0,1))
            .RuleFor(vote => vote.UserId, faker => faker.Random.Number(1, context.Users.Count()))
            .RuleFor(vote => vote.EntityId, faker => faker.PickRandom(1, context.Answers.Count()))
            .Generate(10);
        
        context.AddRange(votesOfAnswers);
        context.SaveChanges();
    }
}