using Domain.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace Application.DataContext;

public class ProgramAppDbContext : DbContext
{
    public ProgramAppDbContext(DbContextOptions<ProgramAppDbContext> contextOptions) : base(contextOptions)
    {
    }
    public DbSet<CustomQuestion> CustomQuestions { get; set; }
    public DbSet<Question> Questions { get; set; }
    public DbSet<Answer> Answers { get; set; }
    public DbSet<CandidateApplication> CandidateApplications { get; set; }
    public DbSet<Choice> Choices { get; set; }
    public DbSet<Program> Programs { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<CustomQuestion>()
            .HasKey(cq => cq.Id);
        modelBuilder.Entity<Question>()
            .HasKey(q => q.Id);
        modelBuilder.Entity<Answer>()
            .HasKey(ans => ans.Id);
        modelBuilder.Entity<CandidateApplication>()
            .HasKey(progApp => progApp.Id);
        modelBuilder.Entity<Choice>()
            .HasKey(ch => ch.Id);
        modelBuilder.Entity<Program>()
            .HasKey(prog => prog.Id);


        modelBuilder.Entity<Program>(prog =>
        {
            prog.HasMany(p => p.Questions)
            .WithOne(q => q.Program)
            .HasForeignKey(q => q.ProgramId);
            prog.HasMany(p => p.CustomQuestions)
            .WithOne(cq => cq.Program)
            .HasForeignKey(cq => cq.ProgramId);
            prog.HasMany(p => p.CandidateApplications)
            .WithOne(app => app.Program)
            .HasForeignKey(app => app.ProgramId);
        });
        modelBuilder.Entity<CandidateApplication>(candidateapp =>
        {
            candidateapp.HasMany(candapp => candapp.Answers)
            .WithOne(ans => ans.CandidateApplication)
            .HasForeignKey(ans => ans.CandidateApplicationId);
        });
        modelBuilder.Entity<CustomQuestion>(customQues =>
        {
            customQues.HasMany(cques => cques.Choices)
            .WithOne(choice => choice.Question)
            .HasForeignKey(choice => choice.QuestionId)
            .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<Answer>(answer =>
        {
            answer.HasMany(ans => ans.Choices)
            .WithOne(choice => choice.Answer)
            .HasForeignKey(choice => choice.AnswerId);
        });
    }
}
