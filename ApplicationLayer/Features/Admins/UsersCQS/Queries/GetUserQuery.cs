using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using AutoMapper;
using Common.Responses;
using Common.Wrapper;
using Domain.Entities;

namespace ApplicationLayer.Features.Admins.UsersCQS.Queries;

public class GetUserQuery : BaseGetQuery<UserResponse>
{
    public GetUserQuery(int id) : base(id) { }
}

public class GetUserQueryHandler : BaseGetQueryHandler<GetUserQuery, UserResponse>
{
    public GetUserQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

    public override async Task<ResponseWrapper<UserResponse>> Handle(GetUserQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<User>().GetAsync(get.Id);
        if (resultInDb == null)
            return new ResponseWrapper<UserResponse>().Failed(message: "User does not exists.");

        var response = _mapper.Map<UserResponse>(resultInDb);
        return new ResponseWrapper<UserResponse>().Success(response);
    }
}
