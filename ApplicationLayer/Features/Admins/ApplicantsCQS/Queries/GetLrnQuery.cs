using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using AutoMapper;
using Common.Responses;
using Common.Wrapper;
using Domain.Entities;
using Mapster;

namespace ApplicationLayer.Features.Admins.ApplicantsCQS.Queries;
public class GetLrnQuery : BaseGetQuery<LrnResponse>
{
    public GetLrnQuery(int id) : base(id){}
}
public class GetLrnQueryHandler : BaseGetQueryHandler<GetLrnQuery, LrnResponse>
{
    public GetLrnQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper){}
    public override async Task<ResponseWrapper<LrnResponse>> Handle(GetLrnQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<AcademicBackground>().GetAsync(get.Id);
        if (resultInDb == null)
            return new ResponseWrapper<LrnResponse>().Failed(message: "Result does not exists.");

        return new ResponseWrapper<LrnResponse>().Success(data: resultInDb.Adapt<LrnResponse>());
    }
}
