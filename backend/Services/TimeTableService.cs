using Backend.Data;
using Backend.DTOs;
using Backend.Exceptions;
using Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Backend.Services;

public class TimeTableService(AppDbContext context) : ITimeTableService
{
    private readonly AppDbContext _context = context;

    public async Task<TimeTable> CreateTimeTableAsync(TimeTable timeTable)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteTimeTableAsync(Guid id)
    {
        throw new NotImplementedException();
    }

    public async Task<TimeTable> GetTimeTableByIdAsync(Guid id)
    {
        return await _context.TimeTables.AsNoTracking().FirstOrDefaultAsync(t => t.Id == id)
            ?? throw new NotFoundException($"TimeTable with ID {id} not found.");
    }

    public async Task<List<TimeTableSummaryResponse>> GetTimeTablesAsync()
    {
        return await _context.TimeTables.AsNoTracking();
    }

    public async Task UpdateTimeTableAsync(Guid id, TimeTable timeTable)
    {
        throw new NotImplementedException();
    }
}
