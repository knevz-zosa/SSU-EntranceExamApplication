using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using AutoMapper;
using Common.Responses;
using Common.Wrapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Admins.CampusesCQS.Queries;
public class GetCampusQuery : BaseGetQuery<CampusResponse>
{
    public GetCampusQuery(int id) : base(id) { }
}

public class GetCampusQueryHandler : BaseGetQueryHandler<GetCampusQuery, CampusResponse>
{
    public GetCampusQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper) { }

    public override async Task<ResponseWrapper<CampusResponse>> Handle(GetCampusQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Campus>().Entities
            .Include(x => x.Departments)
            .Include(x => x.Courses)
            .Include(x => x.Schedules)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == get.Id, cancellationToken);

        if (resultInDb == null) 
            return new ResponseWrapper<CampusResponse>().Failed(message: "Campus does not exists.");   

        var response = _mapper.Map<CampusResponse>(resultInDb);
        return new ResponseWrapper<CampusResponse>().Success(response);

    }
}
