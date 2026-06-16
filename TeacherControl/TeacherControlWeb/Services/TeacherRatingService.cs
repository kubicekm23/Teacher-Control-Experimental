using TeacherControlWeb.Entities;

namespace TeacherControlWeb.Services;

public interface ITeacherRatingService
{
    double CalculateExperimentalScore(TeacherEntity teacher);
}

public class TeacherRatingService : ITeacherRatingService
{
    public double CalculateExperimentalScore(TeacherEntity teacher)
    {
        if (teacher == null) return 0;

        double avgRating = teacher.Reviews.Any() ? teacher.Reviews.Average(r => r.Rating) : 3.0;
        int totalLateness = teacher.Latenesses.Sum(l => l.Minutes);
        int totalVotes = teacher.Votes.Count;

        // Formula: Score = AvgRating - (TotalLateness / 60 * 0.2) + (TotalVotes / 10 * 0.1)
        double score = avgRating;
        
        // Penalty: -0.2 for every 60 minutes of lateness
        score -= (totalLateness / 60.0) * 0.2;
        
        // Bonus: +0.1 for every 10 votes
        score += (totalVotes / 10.0) * 0.1;

        // Clamp between 1.0 and 5.0
        return Math.Clamp(score, 1.0, 5.0);
    }
}
