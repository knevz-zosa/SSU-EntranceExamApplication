using ApplicationLayer.Features.Admins.CampusesCQS.Commands;
using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Admins.DepartmentsCQS.Commands;

public class CreateDepartmentCommand : BaseCreateCommand<DepartmentRequest>
{
    public CreateDepartmentCommand(DepartmentRequest request)
    {
        Request = request;
    }
}
public class CreateDepartmentCommandHandeler : BaseCreateCommandHandler<CreateDepartmentCommand, DepartmentRequest>
{
    public CreateDepartmentCommandHandeler(IUnitOfWork<int> unitOfWork): base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreateDepartmentCommand command, CancellationToken cancellationToken)
    {
        var trimmedName = command.Request.Name.Trim().ToLower();

        var existingResult = await _unitOfWork.ReadRepositoryFor<Department>()
            .Entities.FirstOrDefaultAsync(x => x.Name.Trim().ToLower() == trimmedName
            && x.CampusId == command.Request.CampusId);

        if (existingResult != null)
            return new ResponseWrapper<int>().Failed(message: "Department with this name already exists.");

        var model = command.Request.Adapt<Department>();

        model.Name = model.Name.Trim();

        model.DateCreated = DateTime.Now;
        await _unitOfWork.WriteRepositoryFor<Department>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id, "Department created successfully.");
    }
}
