using Common.CustomClasses;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;

namespace Services.InterviewServices;
public interface IInterviewService
{
    Task<ResponseWrapper<int>> Create(InterviewRequest request);
    Task<ResponseWrapper<int>> UpdateRating(InterviewRatingUpdate update);
    Task<ResponseWrapper<int>> Activate(InterviewActiveUpdate update);
    Task<ResponseWrapper<InterviewResponse>> Get(int id);
    Task<ResponseWrapper<PagedList<InterviewResponse>>> List(DataGridQuery query);

}
