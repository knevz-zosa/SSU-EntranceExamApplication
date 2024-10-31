using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using AutoMapper;
using Common.Responses;
using Common.Wrapper;
using Domain.Entities;
using Mapster;
using MediatR;

namespace ApplicationLayer.Features.Admins.ApplicantsCQS.Queries;
public class GetPersonalInformationQuery : BaseGetQuery<PersonalInformationResponse>
{
    public GetPersonalInformationQuery(int id) : base(id){}
}
public class GetPersonalInformationQueryHandler : BaseGetQueryHandler<GetPersonalInformationQuery, PersonalInformationResponse>
{
    public GetPersonalInformationQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper){}
    public override async Task<ResponseWrapper<PersonalInformationResponse>> Handle(GetPersonalInformationQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<PersonalInformation>().GetAsync(get.Id);
        if (resultInDb == null)
            return new ResponseWrapper<PersonalInformationResponse>().Failed(message: "Result does not exists.");

        return new ResponseWrapper<PersonalInformationResponse>().Success(data: resultInDb.Adapt<PersonalInformationResponse>());
    }
}
