using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using AutoMapper;
using Common.Responses;
using Common.Wrapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Admins.CoursesCQS.Queries;
public class GetCourseQuery : BaseGetQuery<CourseResponse>
{
    public GetCourseQuery(int id) : base(id) { }
}

public class GetCourseQueryHandler : BaseGetQueryHandler<GetCourseQuery, CourseResponse>
{
    public GetCourseQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper) : base(unitOfWork, mapper) { }

    public override async Task<ResponseWrapper<CourseResponse>> Handle(GetCourseQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Course>().Entities
               .Include(x => x.Campus)
               .Include(x => x.Department)
               .AsNoTracking()
               .SingleOrDefaultAsync(x => x.Id == get.Id, cancellationToken);

        if (resultInDb == null)
            return new ResponseWrapper<CourseResponse>().Failed(message: "Course does not exists.");

        var response = _mapper.Map<CourseResponse>(resultInDb);
        return new ResponseWrapper<CourseResponse>().Success(response);

    }
}
