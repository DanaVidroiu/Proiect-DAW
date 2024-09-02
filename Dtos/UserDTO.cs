namespace LearningPlatform.Dtos
{
    public class UserDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public bool IsProfessor { get; set; }
        public string UserName { get; set; } = string.Empty;
    }
}