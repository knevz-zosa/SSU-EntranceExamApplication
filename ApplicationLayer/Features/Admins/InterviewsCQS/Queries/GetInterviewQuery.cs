using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using AutoMapper;
using Common.Responses;
using Common.Wrapper;
using Domain.Entities;
using Mapster;
using MediatR;

namespace ApplicationLayer.Features.Admins.InterviewsCQS.Queries;
public class GetInterviewQuery : BaseGetQuery<InterviewResponse>
{
    public GetInterviewQuery(int id) : base(id){}
}
public class GetInterviewQueryHandler : BaseGetQueryHandler<GetInterviewQuery, InterviewResponse>
{
    public GetInterviewQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper){}
    public override async Task<ResponseWrapper<InterviewResponse>> Handle(GetInterviewQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Interview>().GetAsync(get.Id);
        if (resultInDb == null)
            return new ResponseWrapper<InterviewResponse>().Failed(message: "Result does not exists.");

        return new ResponseWrapper<InterviewResponse>().Success(data: resultInDb.Adapt<InterviewResponse>());
    }
}
