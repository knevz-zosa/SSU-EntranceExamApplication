using Common.CustomClasses;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;

namespace Services.CourseServices;
public interface ICourseService
{
    Task<ResponseWrapper<int>> Create(CourseRequest request);
    Task<ResponseWrapper<int>> Update(CourseUpdate update);
    Task<ResponseWrapper<int>> Delete(int id);
    Task<ResponseWrapper<CourseResponse>> Get(int id);
    Task<ResponseWrapper<PagedList<CourseResponse>>> List(DataGridQuery query, string access);

}

