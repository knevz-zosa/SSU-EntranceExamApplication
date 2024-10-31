using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using AutoMapper;
using Common.Responses;
using Common.Wrapper;
using Domain.Entities;
using Mapster;
using MediatR;

namespace ApplicationLayer.Features.Admins.ExaminationsCQS.Queries;
public class GetExaminationQuery : BaseGetQuery<ExaminationResponse>
{
    public GetExaminationQuery(int id) : base(id){}
}
public class GetExaminationQueryHandler : BaseGetQueryHandler<GetExaminationQuery, ExaminationResponse>
{
    public GetExaminationQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper){}
    public override async Task<ResponseWrapper<ExaminationResponse>> Handle(GetExaminationQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Examination>().GetAsync(get.Id);
        if (resultInDb == null)
            return new ResponseWrapper<ExaminationResponse>().Failed(message: "Result does not exists.");

        return new ResponseWrapper<ExaminationResponse>().Success(data: resultInDb.Adapt<ExaminationResponse>());
    }
}
