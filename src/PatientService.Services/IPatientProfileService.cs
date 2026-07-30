using PatientService.InternalModels.DTOs;
using PatientService.Utils.Common;

namespace PatientService.Services;

public interface IPatientProfileService
{
    Task<ApiResponse<PagedResult<PatientDto>>> GetPatientsAsync(SearchQuery searchQuery);
    Task<ApiResponse<PatientDto>> GetPatientByIdAsync(int id);
    Task<ApiResponse<PatientDetailsDto>> GetPatientByPatientIdAsync(string patientId);
    Task<ApiResponse<PagedResult<PatientDto>>> SearchPatientsAsync(string searchTerm, int pageNumber, int pageSize);
    Task<ApiResponse<string>> GeneratePatientIdAsync();
    Task<ApiResponse<PatientDto>> CreatePatientAsync(CreatePatientDto createPatientDto);
    Task<ApiResponse<PatientDto>> UpdatePatientAsync(int id, UpdatePatientDto updatePatientDto);
    Task<ApiResponse<string>> DeletePatientAsync(int id);
}
