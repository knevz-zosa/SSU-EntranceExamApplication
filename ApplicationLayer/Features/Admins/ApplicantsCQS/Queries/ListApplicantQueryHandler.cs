using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using AutoMapper;
using Common.CustomClasses;
using Common.Responses;
using Common.Wrapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Admins.ApplicantsCQS.Queries;
public class ListApplicantQuery : BaseListQuery<ApplicantResponse>
{
    public int? ScheduleId { get; set; }
    public string Access { get; set; }
}
public class ListApplicantQueryHandler : BaseListQueryHandler<ListApplicantQuery, ApplicantResponse>
{
    public ListApplicantQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper){}
    public override async Task<ResponseWrapper<PagedList<ApplicantResponse>>> Handle(ListApplicantQuery list, CancellationToken cancellationToken)
    {
        var repository = _unitOfWork.ReadRepositoryFor<Applicant>().Entities;
        var query = repository;

        if (list.Access != "All")
        {
            if (list.ScheduleId == null || list.ScheduleId == 0)
            {
                query = query
               .Where(x => x.Schedule.Campus.Name == list.Access)
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
              .Include(x => x.Registered);
            }
            else
            {
                query = query
                .Where(x => x.ScheduleId == list.ScheduleId && x.Schedule.Campus.Name == list.Access)
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
             .Include(x => x.Registered);
            }
        }
        else
        {
            if (list.ScheduleId == null || list.ScheduleId == 0)
            {
                query = query
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
              .Include(x => x.Registered);
            }
            else
            {
                query = query
             .Where(x => x.ScheduleId == list.ScheduleId)
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
             .Include(x => x.Registered);
            }
        }

        query = query.Where(x => x.Registered != null);

        if (!string.IsNullOrEmpty(list.GridQuery.Search))
        {
            query = query.Where(c =>
                 c.PersonalInformation.LastName.Contains(list.GridQuery.Search) ||
                 c.PersonalInformation.FirstName.Contains(list.GridQuery.Search) ||
                 c.Schedule.Campus.Name.Contains(list.GridQuery.Search) ||
                 c.Schedule.Campus.Courses
                    .Any(course => course.Id == c.CourseId && course.Name.Contains(list.GridQuery.Search)));
        }

        var totalCount = await query.CountAsync();

        var sortField = list.GridQuery.SortField ?? nameof(Applicant.PersonalInformation.LastName);
        query = QuerySort(query, sortField, list.GridQuery.SortDir);

        var page = list.GridQuery.Page ?? 0;
        var size = list.GridQuery.PageSize ?? 20;
        var models = await query.Skip(page * size).Take(size).ToListAsync();

        var response = _mapper.Map<List<ApplicantResponse>>(models);

        var pagedList = new PagedList<ApplicantResponse>(totalCount, response);

        return new ResponseWrapper<PagedList<ApplicantResponse>>().Success(pagedList);
    }

    IQueryable<Applicant> QuerySort(IQueryable<Applicant> query, string sortField, DataGridQuerySortDirection sortDirection)
    {
        switch (sortField)
        {
            case nameof(Applicant.PersonalInformation):
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.PersonalInformation.LastName)
                    : query.OrderByDescending(c => c.PersonalInformation.LastName);
            case nameof(Applicant.Schedule):
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.Schedule.Campus.Name)
                    : query.OrderByDescending(c => c.Schedule.Campus.Name);
            default:
                return sortDirection == DataGridQuerySortDirection.Ascending
                    ? query.OrderBy(c => c.Id)
                    : query.OrderByDescending(c => c.Id);
        }
    }
}
