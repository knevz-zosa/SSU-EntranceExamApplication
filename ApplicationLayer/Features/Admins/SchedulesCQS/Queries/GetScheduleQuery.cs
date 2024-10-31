using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using AutoMapper;
using Common.Responses;
using Common.Wrapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Admins.SchedulesCQS.Queries;

public class GetScheduleQuery : BaseGetQuery<ScheduleResponse>
{
    public GetScheduleQuery(int id) : base(id) { }
}

public class GetScheduleQueryHandler : BaseGetQueryHandler<GetScheduleQuery, ScheduleResponse>
{
    public GetScheduleQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public override async Task<ResponseWrapper<ScheduleResponse>> Handle(GetScheduleQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Schedule>().Entities
            .Include(x => x.Campus)
                .ThenInclude(c => c.Courses)
            .Include(x => x.Applicants)
                .ThenInclude(pi => pi.PersonalInformation)
            .Include(x => x.Applicants)
                .ThenInclude(ab => ab.AcademicBackground)
            .Include(x => x.Applicants)
                .ThenInclude(pgi => pgi.ParentGuardianInformation)
            .Include(x => x.Applicants)
                .ThenInclude(ph => ph.PhysicalHealth)
            .Include(x => x.Applicants)
                .ThenInclude(pf => pf.PersonalityProfile)
            .Include(x => x.Applicants)
                .ThenInclude(ec => ec.EmergencyContact)
            .AsNoTracking()
            .SingleOrDefaultAsync(x => x.Id == get.Id, cancellationToken);

        if (resultInDb == null)
        {
            return new ResponseWrapper<ScheduleResponse>().Failed(message: "Schedule does not exists.");           
        }
        var response = _mapper.Map<ScheduleResponse>(resultInDb);
        return new ResponseWrapper<ScheduleResponse>().Success(response);
    }
}

