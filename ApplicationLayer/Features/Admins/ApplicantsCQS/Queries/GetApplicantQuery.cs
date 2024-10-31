using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using AutoMapper;
using Common.Responses;
using Common.Wrapper;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Admins.ApplicantsCQS.Queries;
public class GetApplicantQuery : BaseGetQuery<ApplicantResponse>
{
    public GetApplicantQuery(int id) : base(id) { }
}
public class GetApplicantQueryHandler : BaseGetQueryHandler<GetApplicantQuery, ApplicantResponse>
{
    public GetApplicantQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }
    public override async Task<ResponseWrapper<ApplicantResponse>> Handle(GetApplicantQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Applicant>().Entities
            .Include(x => x.Schedule)
                .ThenInclude(s => s.Campus)
                    .ThenInclude(c => c.Courses)
           .Include(x => x.PersonalInformation)
           .Include(x => x.Spouse)
           .Include(x => x.SoloParent)
           .Include(x => x.AcademicBackground)
           .Include(x => x.ParentGuardianInformation)
           .Include(x => x.FamilyRelations)
           .Include(x => x.PersonalityProfile)
           .Include(x => x.PhysicalHealth)
           .Include(x => x.PsychiatristConsultation)
           .Include(x => x.PsychologistConsultation)
           .Include(x => x.CouncelorConsultation)
           .Include(x => x.EmergencyContact)
           .Include(x => x.Transfers)
           .Include(x => x.Interviews)
           .Include(x => x.Examination)
           .Include(x => x.FirstApplicationInfo)
           .AsNoTracking()
           .SingleOrDefaultAsync(x => x.Id == get.Id, cancellationToken);

        if (resultInDb == null)
            return new ResponseWrapper<ApplicantResponse>().Failed(message: "Applicant does not exists.");

        var response = _mapper.Map<ApplicantResponse>(resultInDb);
        return new ResponseWrapper<ApplicantResponse>().Success(response);
    }
}
