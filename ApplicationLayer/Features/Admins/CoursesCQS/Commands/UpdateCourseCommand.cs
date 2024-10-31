using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Admins.CoursesCQS.Commands;
public class UpdateCourseCommand : BaseUpdateCommand<CourseUpdate>
{
    public UpdateCourseCommand(CourseUpdate update)
    {
        Update = update;
    }
}

public class UpdateCourseCommandHandler : BaseUpdateCommandHandler<UpdateCourseCommand, CourseUpdate>
{
    public UpdateCourseCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork)
    {
    }

    public override async Task<ResponseWrapper<int>> Handle(UpdateCourseCommand command, CancellationToken cancellationToken)
    {
        var trimmedDepartmentName = command.Update.Name.Trim().ToLower();

        var resultExist = await _unitOfWork.ReadRepositoryFor<Course>()
            .Entities.FirstOrDefaultAsync(x => x.Id != command.Update.Id && x.Name.Trim().ToLower() == trimmedDepartmentName
            && x.CampusId == command.Update.CampusId);

        if (resultExist != null)
        {
            return new ResponseWrapper<int>().Failed("Course name already exists.");
        }

        var resultInDb = await _unitOfWork.ReadRepositoryFor<Course>().GetAsync(command.Update.Id);

        if (resultInDb == null)
        {
            return new ResponseWrapper<int>().Failed("Course does not exists.");
        }
        resultInDb.Update(command.Update.CampusId, command.Update.DepartmentId, command.Update.Name.Trim(),
            command.Update.UpdatedBy, command.Update.IsOpen);

        await _unitOfWork.WriteRepositoryFor<Course>().UpdateAsync(resultInDb);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(resultInDb.Id, "Course updated successfuly.");
    }
}
