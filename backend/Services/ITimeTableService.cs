using Backend.DTOs;
using Backend.Models;

namespace Backend.Services;

public interface ITimeTableService
{
    Task<List<TimeTableSummaryResponse>> GetTimeTablesAsync();
    Task<TimeTable> GetTimeTableByIdAsync(Guid id);
    Task<TimeTable> CreateTimeTableAsync(TimeTable timeTable);
    Task UpdateTimeTableAsync(Guid id, TimeTable timeTable);
    Task DeleteTimeTableAsync(Guid id);
}
