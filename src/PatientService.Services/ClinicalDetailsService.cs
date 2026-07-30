using PatientService.InternalModels.DTOs;
using PatientService.Repository;
using PatientService.Utils.Common;

namespace PatientService.Services;

public class ClinicalDetailsService : IClinicalDetailsService
{
    private readonly IPatientRepository _patientRepository;
    private readonly IPatientProfileService _patientProfileService;

    public ClinicalDetailsService(IPatientRepository patientRepository, IPatientProfileService patientProfileService)
    {
        _patientRepository = patientRepository;
        _patientProfileService = patientProfileService;
    }

    public async Task<ApiResponse<PatientWithClinicalDetailsDto>> CreatePatientWithClinicalDetailsAsync(CreatePatientWithClinicalDetailsDto createPatientDto)
    {
        var patientResult = await _patientProfileService.CreatePatientAsync(createPatientDto);
        if (!patientResult.Success || patientResult.Data is null)
        {
            return ApiResponse<PatientWithClinicalDetailsDto>.Fail(patientResult.Message);
        }

        var patientId = patientResult.Data.Id;
        var details = await CreateClinicalDetailsAsync(patientId, createPatientDto);

        return ApiResponse<PatientWithClinicalDetailsDto>.Ok(new PatientWithClinicalDetailsDto
        {
            Patient = patientResult.Data,
            Allergies = details.Allergies,
            Medications = details.Medications,
            MedicalHistory = details.MedicalHistory,
            EmergencyContacts = details.EmergencyContacts,
            Insurance = details.Insurance,
            Vitals = details.Vitals
        }, "Patient created successfully");
    }

    public async Task<ApiResponse<PatientWithClinicalDetailsDto>> UpdatePatientWithClinicalDetailsAsync(int id, UpdatePatientWithClinicalDetailsDto updatePatientDto)
    {
        var patientResult = await _patientProfileService.UpdatePatientAsync(id, updatePatientDto);
        if (!patientResult.Success || patientResult.Data is null)
        {
            return ApiResponse<PatientWithClinicalDetailsDto>.Fail(patientResult.Message);
        }

        await ReplaceClinicalDetailsAsync(id, updatePatientDto);

        return ApiResponse<PatientWithClinicalDetailsDto>.Ok(new PatientWithClinicalDetailsDto
        {
            Patient = patientResult.Data,
            Allergies = updatePatientDto.Allergies,
            Medications = updatePatientDto.Medications,
            MedicalHistory = updatePatientDto.MedicalHistory,
            EmergencyContacts = updatePatientDto.EmergencyContacts,
            Insurance = updatePatientDto.Insurance,
            Vitals = updatePatientDto.Vitals
        }, "Patient updated successfully");
    }

    public async Task<ApiResponse<IEnumerable<AllergyDto>>> GetAllergiesAsync(int patientId)
    {
        var data = await _patientRepository.GetAllergiesAsync(patientId);
        return ApiResponse<IEnumerable<AllergyDto>>.Ok(data.Select(PatientMapper.ToDto).ToList());
    }

    public async Task<ApiResponse<AllergyDto>> GetAllergyAsync(int patientId, int id)
    {
        var data = await _patientRepository.GetAllergyAsync(patientId, id);
        return data is null ? ApiResponse<AllergyDto>.Fail("Allergy not found") : ApiResponse<AllergyDto>.Ok(PatientMapper.ToDto(data));
    }

    public async Task<ApiResponse<AllergyDto>> AddAllergyAsync(int patientId, AllergyDto dto)
    {
        var data = await _patientRepository.AddAllergyAsync(PatientMapper.ToEntity(patientId, dto));
        return ApiResponse<AllergyDto>.Ok(PatientMapper.ToDto(data), "Allergy added");
    }

    public async Task<ApiResponse<AllergyDto>> UpdateAllergyAsync(int patientId, int id, AllergyDto dto)
    {
        var data = await _patientRepository.UpdateAllergyAsync(patientId, id, PatientMapper.ToEntity(patientId, dto));
        return data is null ? ApiResponse<AllergyDto>.Fail("Allergy not found") : ApiResponse<AllergyDto>.Ok(PatientMapper.ToDto(data), "Allergy updated");
    }

    public async Task<ApiResponse<string>> DeleteAllergyAsync(int patientId, int id)
    {
        var deleted = await _patientRepository.DeleteAllergyAsync(patientId, id);
        return deleted ? ApiResponse<string>.Ok("Allergy deleted") : ApiResponse<string>.Fail("Allergy not found");
    }

    public async Task<ApiResponse<IEnumerable<MedicationDto>>> GetMedicationsAsync(int patientId)
    {
        var data = await _patientRepository.GetMedicationsAsync(patientId);
        return ApiResponse<IEnumerable<MedicationDto>>.Ok(data.Select(PatientMapper.ToDto).ToList());
    }

    public async Task<ApiResponse<IEnumerable<MedicationDto>>> GetActiveMedicationsAsync(int patientId)
    {
        var data = await _patientRepository.GetActiveMedicationsAsync(patientId);
        return ApiResponse<IEnumerable<MedicationDto>>.Ok(data.Select(PatientMapper.ToDto).ToList());
    }

    public async Task<ApiResponse<MedicationDto>> GetMedicationAsync(int patientId, int id)
    {
        var data = await _patientRepository.GetMedicationAsync(patientId, id);
        return data is null ? ApiResponse<MedicationDto>.Fail("Medication not found") : ApiResponse<MedicationDto>.Ok(PatientMapper.ToDto(data));
    }

    public async Task<ApiResponse<MedicationDto>> AddMedicationAsync(int patientId, MedicationDto dto)
    {
        var data = await _patientRepository.AddMedicationAsync(PatientMapper.ToEntity(patientId, dto));
        return ApiResponse<MedicationDto>.Ok(PatientMapper.ToDto(data), "Medication added");
    }

    public async Task<ApiResponse<MedicationDto>> UpdateMedicationAsync(int patientId, int id, MedicationDto dto)
    {
        var data = await _patientRepository.UpdateMedicationAsync(patientId, id, PatientMapper.ToEntity(patientId, dto));
        return data is null ? ApiResponse<MedicationDto>.Fail("Medication not found") : ApiResponse<MedicationDto>.Ok(PatientMapper.ToDto(data), "Medication updated");
    }

    public async Task<ApiResponse<MedicationDto>> DiscontinueMedicationAsync(int patientId, int id, DateTime endDate)
    {
        var data = await _patientRepository.DiscontinueMedicationAsync(patientId, id, endDate);
        return data is null ? ApiResponse<MedicationDto>.Fail("Medication not found") : ApiResponse<MedicationDto>.Ok(PatientMapper.ToDto(data), "Medication discontinued");
    }

    public async Task<ApiResponse<string>> DeleteMedicationAsync(int patientId, int id)
    {
        var deleted = await _patientRepository.DeleteMedicationAsync(patientId, id);
        return deleted ? ApiResponse<string>.Ok("Medication deleted") : ApiResponse<string>.Fail("Medication not found");
    }

    public async Task<ApiResponse<IEnumerable<MedicalHistoryDto>>> GetMedicalHistoryAsync(int patientId)
    {
        var data = await _patientRepository.GetMedicalHistoryAsync(patientId);
        return ApiResponse<IEnumerable<MedicalHistoryDto>>.Ok(data.Select(PatientMapper.ToDto).ToList());
    }

    public async Task<ApiResponse<MedicalHistoryDto>> GetMedicalHistoryAsync(int patientId, int id)
    {
        var data = await _patientRepository.GetMedicalHistoryAsync(patientId, id);
        return data is null ? ApiResponse<MedicalHistoryDto>.Fail("Medical history not found") : ApiResponse<MedicalHistoryDto>.Ok(PatientMapper.ToDto(data));
    }

    public async Task<ApiResponse<MedicalHistoryDto>> AddMedicalHistoryAsync(int patientId, MedicalHistoryDto dto)
    {
        var data = await _patientRepository.AddMedicalHistoryAsync(PatientMapper.ToEntity(patientId, dto));
        return ApiResponse<MedicalHistoryDto>.Ok(PatientMapper.ToDto(data), "Medical history added");
    }

    public async Task<ApiResponse<MedicalHistoryDto>> UpdateMedicalHistoryAsync(int patientId, int id, MedicalHistoryDto dto)
    {
        var data = await _patientRepository.UpdateMedicalHistoryAsync(patientId, id, PatientMapper.ToEntity(patientId, dto));
        return data is null ? ApiResponse<MedicalHistoryDto>.Fail("Medical history not found") : ApiResponse<MedicalHistoryDto>.Ok(PatientMapper.ToDto(data), "Medical history updated");
    }

    public async Task<ApiResponse<string>> DeleteMedicalHistoryAsync(int patientId, int id)
    {
        var deleted = await _patientRepository.DeleteMedicalHistoryAsync(patientId, id);
        return deleted ? ApiResponse<string>.Ok("Medical history deleted") : ApiResponse<string>.Fail("Medical history not found");
    }

    public async Task<ApiResponse<IEnumerable<EmergencyContactDto>>> GetEmergencyContactsAsync(int patientId)
    {
        var data = await _patientRepository.GetEmergencyContactsAsync(patientId);
        return ApiResponse<IEnumerable<EmergencyContactDto>>.Ok(data.Select(PatientMapper.ToDto).ToList());
    }

    public async Task<ApiResponse<EmergencyContactDto>> GetEmergencyContactAsync(int patientId, int id)
    {
        var data = await _patientRepository.GetEmergencyContactAsync(patientId, id);
        return data is null ? ApiResponse<EmergencyContactDto>.Fail("Emergency contact not found") : ApiResponse<EmergencyContactDto>.Ok(PatientMapper.ToDto(data));
    }

    public async Task<ApiResponse<EmergencyContactDto>> AddEmergencyContactAsync(int patientId, EmergencyContactDto dto)
    {
        var data = await _patientRepository.AddEmergencyContactAsync(PatientMapper.ToEntity(patientId, dto));
        return ApiResponse<EmergencyContactDto>.Ok(PatientMapper.ToDto(data), "Emergency contact added");
    }

    public async Task<ApiResponse<EmergencyContactDto>> UpdateEmergencyContactAsync(int patientId, int id, EmergencyContactDto dto)
    {
        var data = await _patientRepository.UpdateEmergencyContactAsync(patientId, id, PatientMapper.ToEntity(patientId, dto));
        return data is null ? ApiResponse<EmergencyContactDto>.Fail("Emergency contact not found") : ApiResponse<EmergencyContactDto>.Ok(PatientMapper.ToDto(data), "Emergency contact updated");
    }

    public async Task<ApiResponse<string>> DeleteEmergencyContactAsync(int patientId, int id)
    {
        var deleted = await _patientRepository.DeleteEmergencyContactAsync(patientId, id);
        return deleted ? ApiResponse<string>.Ok("Emergency contact deleted") : ApiResponse<string>.Fail("Emergency contact not found");
    }

    public async Task<ApiResponse<IEnumerable<InsuranceDto>>> GetInsuranceAsync(int patientId)
    {
        var data = await _patientRepository.GetInsuranceAsync(patientId);
        return ApiResponse<IEnumerable<InsuranceDto>>.Ok(data.Select(PatientMapper.ToDto).ToList());
    }

    public async Task<ApiResponse<InsuranceDto>> GetInsuranceAsync(int patientId, int id)
    {
        var data = await _patientRepository.GetInsuranceAsync(patientId, id);
        return data is null ? ApiResponse<InsuranceDto>.Fail("Insurance not found") : ApiResponse<InsuranceDto>.Ok(PatientMapper.ToDto(data));
    }

    public async Task<ApiResponse<InsuranceDto>> AddInsuranceAsync(int patientId, InsuranceDto dto)
    {
        var data = await _patientRepository.AddInsuranceAsync(PatientMapper.ToEntity(patientId, dto));
        return ApiResponse<InsuranceDto>.Ok(PatientMapper.ToDto(data), "Insurance added");
    }

    public async Task<ApiResponse<InsuranceDto>> UpdateInsuranceAsync(int patientId, int id, InsuranceDto dto)
    {
        var data = await _patientRepository.UpdateInsuranceAsync(patientId, id, PatientMapper.ToEntity(patientId, dto));
        return data is null ? ApiResponse<InsuranceDto>.Fail("Insurance not found") : ApiResponse<InsuranceDto>.Ok(PatientMapper.ToDto(data), "Insurance updated");
    }

    public async Task<ApiResponse<string>> DeleteInsuranceAsync(int patientId, int id)
    {
        var deleted = await _patientRepository.DeleteInsuranceAsync(patientId, id);
        return deleted ? ApiResponse<string>.Ok("Insurance deleted") : ApiResponse<string>.Fail("Insurance not found");
    }

    public async Task<ApiResponse<IEnumerable<VitalDto>>> GetVitalsAsync(int patientId)
    {
        var data = await _patientRepository.GetVitalsAsync(patientId);
        return ApiResponse<IEnumerable<VitalDto>>.Ok(data.Select(PatientMapper.ToDto).ToList());
    }

    public async Task<ApiResponse<VitalDto>> GetVitalAsync(int patientId, int id)
    {
        var data = await _patientRepository.GetVitalAsync(patientId, id);
        return data is null ? ApiResponse<VitalDto>.Fail("Vital not found") : ApiResponse<VitalDto>.Ok(PatientMapper.ToDto(data));
    }

    public async Task<ApiResponse<VitalDto>> AddVitalAsync(int patientId, VitalDto dto)
    {
        var data = await _patientRepository.AddVitalAsync(PatientMapper.ToEntity(patientId, dto));
        return ApiResponse<VitalDto>.Ok(PatientMapper.ToDto(data), "Vital added");
    }

    public async Task<ApiResponse<VitalDto>> UpdateVitalAsync(int patientId, int id, VitalDto dto)
    {
        var data = await _patientRepository.UpdateVitalAsync(patientId, id, PatientMapper.ToEntity(patientId, dto));
        return data is null ? ApiResponse<VitalDto>.Fail("Vital not found") : ApiResponse<VitalDto>.Ok(PatientMapper.ToDto(data), "Vital updated");
    }

    public async Task<ApiResponse<string>> DeleteVitalAsync(int patientId, int id)
    {
        var deleted = await _patientRepository.DeleteVitalAsync(patientId, id);
        return deleted ? ApiResponse<string>.Ok("Vital deleted") : ApiResponse<string>.Fail("Vital not found");
    }

    private async Task<ClinicalDetailsBundle> CreateClinicalDetailsAsync(int patientId, CreatePatientWithClinicalDetailsDto createPatientDto)
    {
        var allergies = new List<AllergyDto>();
        foreach (var allergy in createPatientDto.Allergies ?? [])
        {
            var result = await AddAllergyAsync(patientId, allergy);
            if (result.Data is not null)
            {
                allergies.Add(result.Data);
            }
        }

        var medications = new List<MedicationDto>();
        foreach (var medication in createPatientDto.Medications ?? [])
        {
            var result = await AddMedicationAsync(patientId, medication);
            if (result.Data is not null)
            {
                medications.Add(result.Data);
            }
        }

        var medicalHistory = new List<MedicalHistoryDto>();
        foreach (var history in createPatientDto.MedicalHistory ?? [])
        {
            var result = await AddMedicalHistoryAsync(patientId, history);
            if (result.Data is not null)
            {
                medicalHistory.Add(result.Data);
            }
        }

        var emergencyContacts = new List<EmergencyContactDto>();
        foreach (var contact in createPatientDto.EmergencyContacts ?? [])
        {
            var result = await AddEmergencyContactAsync(patientId, contact);
            if (result.Data is not null)
            {
                emergencyContacts.Add(result.Data);
            }
        }

        var insurance = new List<InsuranceDto>();
        foreach (var insuranceItem in createPatientDto.Insurance ?? [])
        {
            var result = await AddInsuranceAsync(patientId, insuranceItem);
            if (result.Data is not null)
            {
                insurance.Add(result.Data);
            }
        }

        var vitals = new List<VitalDto>();
        foreach (var vital in createPatientDto.Vitals ?? [])
        {
            var result = await AddVitalAsync(patientId, vital);
            if (result.Data is not null)
            {
                vitals.Add(result.Data);
            }
        }

        return new ClinicalDetailsBundle(allergies, medications, medicalHistory, emergencyContacts, insurance, vitals);
    }

    private async Task ReplaceClinicalDetailsAsync(int patientId, UpdatePatientWithClinicalDetailsDto updatePatientDto)
    {
        var existingAllergies = await _patientRepository.GetAllergiesAsync(patientId);
        foreach (var allergy in existingAllergies)
        {
            await _patientRepository.DeleteAllergyAsync(patientId, allergy.Id);
        }

        var existingMedications = await _patientRepository.GetMedicationsAsync(patientId);
        foreach (var medication in existingMedications)
        {
            await _patientRepository.DeleteMedicationAsync(patientId, medication.Id);
        }

        var existingMedicalHistory = await _patientRepository.GetMedicalHistoryAsync(patientId);
        foreach (var medicalHistoryItem in existingMedicalHistory)
        {
            await _patientRepository.DeleteMedicalHistoryAsync(patientId, medicalHistoryItem.Id);
        }

        var existingEmergencyContacts = await _patientRepository.GetEmergencyContactsAsync(patientId);
        foreach (var contact in existingEmergencyContacts)
        {
            await _patientRepository.DeleteEmergencyContactAsync(patientId, contact.Id);
        }

        var existingInsurance = await _patientRepository.GetInsuranceAsync(patientId);
        foreach (var insuranceItem in existingInsurance)
        {
            await _patientRepository.DeleteInsuranceAsync(patientId, insuranceItem.Id);
        }

        var existingVitals = await _patientRepository.GetVitalsAsync(patientId);
        foreach (var vital in existingVitals)
        {
            await _patientRepository.DeleteVitalAsync(patientId, vital.Id);
        }

        await CreateClinicalDetailsAsync(patientId, new CreatePatientWithClinicalDetailsDto
        {
            Allergies = updatePatientDto.Allergies.ToList(),
            Medications = updatePatientDto.Medications.ToList(),
            MedicalHistory = updatePatientDto.MedicalHistory.ToList(),
            EmergencyContacts = updatePatientDto.EmergencyContacts.ToList(),
            Insurance = updatePatientDto.Insurance.ToList(),
            Vitals = updatePatientDto.Vitals.ToList()
        });
    }

    private sealed record ClinicalDetailsBundle(
        IReadOnlyCollection<AllergyDto> Allergies,
        IReadOnlyCollection<MedicationDto> Medications,
        IReadOnlyCollection<MedicalHistoryDto> MedicalHistory,
        IReadOnlyCollection<EmergencyContactDto> EmergencyContacts,
        IReadOnlyCollection<InsuranceDto> Insurance,
        IReadOnlyCollection<VitalDto> Vitals);
}
