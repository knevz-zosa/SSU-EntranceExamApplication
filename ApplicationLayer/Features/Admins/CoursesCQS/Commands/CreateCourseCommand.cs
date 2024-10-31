using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Admins.CoursesCQS.Commands;
public class CreateCourseCommand : BaseCreateCommand<CourseRequest>
{
	public CreateCourseCommand(CourseRequest request)
	{
		Request = request;
	}
}

public class CreateCourseCommandHandler : BaseCreateCommandHandler<CreateCourseCommand, CourseRequest>
{
    public CreateCourseCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork){}
    public override async Task<ResponseWrapper<int>> Handle(CreateCourseCommand command, CancellationToken cancellationToken)
    {
        var trimmedName = command.Request.Name.Trim().ToLower();

        var existingResult = await _unitOfWork.ReadRepositoryFor<Course>()
            .Entities.FirstOrDefaultAsync(x => x.Name.Trim().ToLower() == trimmedName
            && x.CampusId == command.Request.CampusId);

        if (existingResult != null)
            return new ResponseWrapper<int>().Failed(message: "Course with this name already exists.");

        var model = command.Request.Adapt<Course>();        
        model.Name = model.Name.Trim();

        model.DateCreated = DateTime.Now;
        await _unitOfWork.WriteRepositoryFor<Course>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id, "Course created successfully.");
    }
}
