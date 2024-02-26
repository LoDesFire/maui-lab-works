using LabsApp.Entities;

namespace LabsApp.Services;

public interface IRateService
{
    Task<IEnumerable<Rate>> GetRates(DateTime? date, IEnumerable<string> currencies);
}