using LearningPlatform.Dtos;
using System.Collections.Generic;
using System.Threading.Tasks;

public interface IEnrollmentService
{
    Task<IEnumerable<EnrollmentDTO>> GetAllEnrollmentsAsync();
    Task<EnrollmentDTO> GetEnrollmentByIdAsync(int enrollmentId);
    Task CreateEnrollmentAsync(EnrollmentDTO enrollmentDto);
    Task UpdateEnrollmentAsync(EnrollmentDTO enrollmentDto);
    Task DeleteEnrollmentAsync(int enrollmentId);
}
