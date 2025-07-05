using MyRestApi.Models;

namespace MyRestApi.Repositories;

public interface IScheduleRepository
{
    Task<Schedule?> GetScheduleById(int id);
    Task AddSchedule(Schedule schedule);
    Task ModifySchedule(Schedule schedule);
    Task DeleteSchedule(Schedule schedule);
}