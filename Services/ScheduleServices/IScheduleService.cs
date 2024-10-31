using Common.CustomClasses;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;

namespace Services.ScheduleServices;
public interface IScheduleService
{
    Task<ResponseWrapper<int>> Create(ScheduleRequest request);
    Task<ResponseWrapper<int>> Delete(int id);
    Task<ResponseWrapper<ScheduleResponse>> Get(int id);
    Task<ResponseWrapper<PagedList<ScheduleResponse>>> List(DataGridQuery query, string access);
}
