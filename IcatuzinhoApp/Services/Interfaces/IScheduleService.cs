using System;
using System.Threading.Tasks;

namespace IcatuzinhoApp
{
    public interface IScheduleService : IBaseService<Schedule>
    {
        Task GetAllSchedules();
    }
}

