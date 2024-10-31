using Common.CustomClasses;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;

namespace Services.CampusesServices;
public interface ICampusService
{
    Task<ResponseWrapper<int>> Create(CampusRequest request);
    Task<ResponseWrapper<int>> Update(CampusUpdate update);
    Task<ResponseWrapper<int>> Delete(int id);
    Task<ResponseWrapper<CampusResponse>> Get(int id);
    Task<ResponseWrapper<PagedList<CampusResponse>>> List(DataGridQuery query);
}
