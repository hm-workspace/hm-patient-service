using Dapper;
using PatientService.Data;
using PatientService.InternalModels.Entities;
using PatientService.Utils.Common;

namespace PatientService.Repository;

public class PatientRepository : IPatientRepository
{
    private readonly IDbConnectionFactory _connectionFactory;

    public PatientRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public Task<PagedResult<PatientEntity>> GetPatientsAsync(SearchQuery searchQuery)
    {
        var query = PatientInMemoryStore.Patients.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(searchQuery.SearchTerm))
        {
            query = query.Where(x =>
                x.PatientId.Contains(searchQuery.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                x.FirstName.Contains(searchQuery.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                x.LastName.Contains(searchQuery.SearchTerm, StringComparison.OrdinalIgnoreCase) ||
                x.Email.Contains(searchQuery.SearchTerm, StringComparison.OrdinalIgnoreCase));
        }

        var total = query.Count();
        var items = query.OrderBy(x => x.Id)
            .Skip((searchQuery.PageNumber - 1) * searchQuery.PageSize)
            .Take(searchQuery.PageSize)
            .ToList();
        return Task.FromResult(new PagedResult<PatientEntity>(items, total, searchQuery.PageNumber, searchQuery.PageSize));
    }

    public Task<PatientEntity?> GetPatientByIdAsync(int id) =>
        Task.FromResult(PatientInMemoryStore.Patients.FirstOrDefault(x => x.Id == id));

    public Task<PatientEntity?> GetPatientByPatientIdAsync(string patientId) =>
        Task.FromResult(PatientInMemoryStore.Patients.FirstOrDefault(x => x.PatientId.Equals(patientId, StringComparison.OrdinalIgnoreCase)));

    public Task<PatientEntity> CreatePatientAsync(PatientEntity patient)
    {
        patient.Id = Interlocked.Increment(ref PatientInMemoryStore.PatientSeed);
        PatientInMemoryStore.Patients.Add(patient);
        return Task.FromResult(patient);
    }

    public Task<PatientEntity?> UpdatePatientAsync(int id, PatientEntity patient)
    {
        var existing = PatientInMemoryStore.Patients.FirstOrDefault(x => x.Id == id);
        if (existing is null)
        {
            return Task.FromResult<PatientEntity?>(null);
        }

        existing.FirstName = patient.FirstName;
        existing.LastName = patient.LastName;
        existing.DateOfBirth = patient.DateOfBirth;
        existing.Gender = patient.Gender;
        existing.Email = patient.Email;
        existing.Phone = patient.Phone;
        existing.Address = patient.Address;
        existing.UpdatedAt = DateTime.UtcNow;
        return Task.FromResult<PatientEntity?>(existing);
    }

    public Task<bool> DeletePatientAsync(int id)
    {
        var existing = PatientInMemoryStore.Patients.FirstOrDefault(x => x.Id == id);
        if (existing is null)
        {
            return Task.FromResult(false);
        }

        PatientInMemoryStore.Patients.Remove(existing);
        PatientInMemoryStore.Allergies.RemoveAll(x => x.PatientId == id);
        PatientInMemoryStore.Medications.RemoveAll(x => x.PatientId == id);
        return Task.FromResult(true);
    }

    public Task<string> GeneratePatientIdAsync()
    {
        var next = PatientInMemoryStore.Patients.Count + 1;
        return Task.FromResult($"PAT{next:000}");
    }

    public Task<IReadOnlyCollection<AllergyEntity>> GetAllergiesAsync(int patientId) =>
        Task.FromResult<IReadOnlyCollection<AllergyEntity>>(PatientInMemoryStore.Allergies.Where(x => x.PatientId == patientId).ToList());

    public Task<AllergyEntity?> GetAllergyAsync(int patientId, int id) =>
        Task.FromResult(PatientInMemoryStore.Allergies.FirstOrDefault(x => x.PatientId == patientId && x.Id == id));

    public Task<AllergyEntity> AddAllergyAsync(AllergyEntity allergy)
    {
        allergy.Id = Interlocked.Increment(ref PatientInMemoryStore.AllergySeed);
        PatientInMemoryStore.Allergies.Add(allergy);
        return Task.FromResult(allergy);
    }

    public Task<AllergyEntity?> UpdateAllergyAsync(int patientId, int id, AllergyEntity allergy)
    {
        var existing = PatientInMemoryStore.Allergies.FirstOrDefault(x => x.PatientId == patientId && x.Id == id);
        if (existing is null)
        {
            return Task.FromResult<AllergyEntity?>(null);
        }

        existing.Allergy = allergy.Allergy;
        existing.Severity = allergy.Severity;
        existing.Notes = allergy.Notes;
        return Task.FromResult<AllergyEntity?>(existing);
    }

    public Task<bool> DeleteAllergyAsync(int patientId, int id)
    {
        var existing = PatientInMemoryStore.Allergies.FirstOrDefault(x => x.PatientId == patientId && x.Id == id);
        if (existing is null)
        {
            return Task.FromResult(false);
        }

        PatientInMemoryStore.Allergies.Remove(existing);
        return Task.FromResult(true);
    }

    public Task<IReadOnlyCollection<MedicationEntity>> GetMedicationsAsync(int patientId) =>
        Task.FromResult<IReadOnlyCollection<MedicationEntity>>(PatientInMemoryStore.Medications.Where(x => x.PatientId == patientId).ToList());

    public Task<IReadOnlyCollection<MedicationEntity>> GetActiveMedicationsAsync(int patientId) =>
        Task.FromResult<IReadOnlyCollection<MedicationEntity>>(PatientInMemoryStore.Medications
            .Where(x => x.PatientId == patientId && (!x.EndDate.HasValue || x.EndDate > DateTime.UtcNow))
            .ToList());

    public Task<MedicationEntity?> GetMedicationAsync(int patientId, int id) =>
        Task.FromResult(PatientInMemoryStore.Medications.FirstOrDefault(x => x.PatientId == patientId && x.Id == id));

    public Task<MedicationEntity> AddMedicationAsync(MedicationEntity medication)
    {
        medication.Id = Interlocked.Increment(ref PatientInMemoryStore.MedicationSeed);
        PatientInMemoryStore.Medications.Add(medication);
        return Task.FromResult(medication);
    }

    public Task<MedicationEntity?> UpdateMedicationAsync(int patientId, int id, MedicationEntity medication)
    {
        var existing = PatientInMemoryStore.Medications.FirstOrDefault(x => x.PatientId == patientId && x.Id == id);
        if (existing is null)
        {
            return Task.FromResult<MedicationEntity?>(null);
        }

        existing.MedicationName = medication.MedicationName;
        existing.Dosage = medication.Dosage;
        existing.Frequency = medication.Frequency;
        existing.StartDate = medication.StartDate;
        existing.EndDate = medication.EndDate;
        existing.Notes = medication.Notes;
        return Task.FromResult<MedicationEntity?>(existing);
    }

    public Task<MedicationEntity?> DiscontinueMedicationAsync(int patientId, int id, DateTime endDate)
    {
        var existing = PatientInMemoryStore.Medications.FirstOrDefault(x => x.PatientId == patientId && x.Id == id);
        if (existing is null)
        {
            return Task.FromResult<MedicationEntity?>(null);
        }

        existing.EndDate = endDate;
        return Task.FromResult<MedicationEntity?>(existing);
    }

    public Task<bool> DeleteMedicationAsync(int patientId, int id)
    {
        var existing = PatientInMemoryStore.Medications.FirstOrDefault(x => x.PatientId == patientId && x.Id == id);
        if (existing is null)
        {
            return Task.FromResult(false);
        }

        PatientInMemoryStore.Medications.Remove(existing);
        return Task.FromResult(true);
    }
}

internal static class PatientInMemoryStore
{
    public static int PatientSeed = 1;
    public static int AllergySeed = 0;
    public static int MedicationSeed = 0;

    public static readonly List<PatientEntity> Patients =
    [
        new PatientEntity
        {
            Id = 1,
            PatientId = "PAT001",
            FirstName = "John",
            LastName = "Doe",
            DateOfBirth = new DateTime(1990, 1, 1),
            Gender = "Male",
            Email = "john.doe@hm.local",
            Phone = "9000000001",
            Address = "Hyderabad",
            IsActive = true,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        }
    ];

    public static readonly List<AllergyEntity> Allergies = [];
    public static readonly List<MedicationEntity> Medications = [];
}
