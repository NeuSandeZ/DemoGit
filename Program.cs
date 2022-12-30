using Microsoft.EntityFrameworkCore;
using StackOverflowProject;
using StackOverflowProject.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<StackOverflowContext>(option => option
    .UseNpgsql(builder.Configuration.GetConnectionString("StackOverflowProjectConnectionString")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetService<StackOverflowContext>();
var pendingMigrations = dbContext.Database.GetPendingMigrations();

if (pendingMigrations.Any()) dbContext.Database.Migrate();

if (!dbContext.Users.Any()) dbContext.Seed();

app.MapPost("createQuestion", (StackOverflowContext db) =>
{
    var question = new Question
    {
        AuthorId = 1,
        Body = "Cos tam cos tam",
        DateCreated = DateTime.UtcNow,
        Title = "Zapytanie",
        Votes = 0
    };

    db.Add(question);
    db.SaveChanges();
});

app.MapPut("tagsToQuestionAttached", (StackOverflowContext db) =>
{
    var quesiton = db.Questions.First(question => question.Id == 62);
    var tags = db.Tags.Take(5);

    quesiton.Tags = tags.ToList();
    db.Questions.Update(quesiton);
    db.SaveChanges();
});

app.MapPost("createAnwserToQuesion", (StackOverflowContext db) =>
{
    var question = dbContext.Questions.First(q => q.Id == 1);
    var anwser = new Answer
    {
        AuthorId = 3,
        Body = "Odpowiedz na pytanie",
        DateCreated = DateTime.UtcNow,
        QuestionId = question.Id,
        Votes = 0
    };
    db.Add(anwser);
    db.SaveChanges();
});

app.MapPost("IncreaseVoteToQuestion", (StackOverflowContext db) =>
{
    var question = dbContext.Questions.First(q => q.AuthorId == 2);
    question.Votes += 1;
    db.Update(question);
    db.SaveChanges();
    return question;
});

app.MapPost("IncreaseVoteToAnswer", (StackOverflowContext db) =>
{
    var anwser = dbContext.Answers.First(q => q.AuthorId == 6);
    anwser.Votes += 1;
    db.Update(anwser);
    db.SaveChanges();
    return anwser;
});

app.MapPut("VoteTrying", (StackOverflowContext db) =>
{
    var vote = dbContext.Votes.FirstOrDefault(vote => vote.Id == 1);

    if (vote != null)
    {
        try
        {
            var hasUserVoted = dbContext.Votes.Any(v => v.UserId == vote.UserId && v.EntityId == vote.Id);
            if (hasUserVoted)
            {
                throw new Exception("You already voted");
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
        }
    }
    else
    {
        var newVote = new Vote
        {
            UserId = 1,
            EntityId = 1,
            ValueOfVote = 1
        };
        db.Add(newVote);
        db.SaveChanges();
    }
});

app.Run();