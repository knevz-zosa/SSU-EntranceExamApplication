using ApplicationLayer.Features.Admins.CampusesCQS.Queries;
using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using AutoMapper;
using Common.Responses;
using Common.Wrapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Admins.DepartmentsCQS.Queries;

public class GetDepartmentQuery : BaseGetQuery<DepartmentResponse>
{
    public GetDepartmentQuery(int id) : base(id) { }
}

public class GetDepartmentQueryHandler : BaseGetQueryHandler<GetDepartmentQuery, DepartmentResponse>
{
    public GetDepartmentQueryHandler(IUnitOfWork<int> unitOfWork, IMapper mapper)
        : base(unitOfWork, mapper) { }
   
    public override async Task<ResponseWrapper<DepartmentResponse>> Handle(GetDepartmentQuery get, CancellationToken cancellationToken)
    {
        var resultInDb = await _unitOfWork.ReadRepositoryFor<Department>().Entities
               .Include(x => x.Campus)
               .Include(x => x.Courses)
               .AsNoTracking()
               .SingleOrDefaultAsync(x => x.Id == get.Id, cancellationToken);

        if (resultInDb == null)
            return new ResponseWrapper<DepartmentResponse>().Failed(message: "Department does not exists.");

        var response = _mapper.Map<DepartmentResponse>(resultInDb);
        return new ResponseWrapper<DepartmentResponse>().Success(response);

    }
}