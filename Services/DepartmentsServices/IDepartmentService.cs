using Common.CustomClasses;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;

namespace Services.DepartmentsServices;
public interface IDepartmentService
{
    Task<ResponseWrapper<int>> Create(DepartmentRequest request);
    Task<ResponseWrapper<int>> Update(DepartmentUpdate update);
    Task<ResponseWrapper<int>> Delete(int id);
    Task<ResponseWrapper<DepartmentResponse>> Get(int id);
    Task<ResponseWrapper<PagedList<DepartmentResponse>>> List(DataGridQuery query);

}
