using System;
using System.Threading.Tasks;

namespace IcatuzinhoApp
{
    public interface ITravelService : IBaseService<Travel>
    {
        Task GetTravelByScheduleId(int scheduleId);
        Task<int?> GetSeatsAvailableByTravel(int scheduleId);
        Task<int> GetAvailableSeats(int travelId);
        Task<bool> DoCheckIn(int scheduleId, int userId);
        Task<bool> DoCheckOut(int scheduleId, int userId);
    }
}

