namespace LearningPlatform.Dtos
{
public class CourseWithLessonsDTO
{
    public int CourseId { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<LessonDTO> Lessons { get; set; } = new List<LessonDTO>();
}
}