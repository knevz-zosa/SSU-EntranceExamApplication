using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Wrapper;
using Domain.Entities;
using MediatR;

namespace ApplicationLayer.Features.Admins.CoursesCQS.Commands;

public class DeleteCourseCommand : BaseDeleteCommand { }
public class DeleteCourseCommandHandler : BaseDeleteCommandHandler<DeleteCourseCommand>
{
    public DeleteCourseCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }

    public override async Task<ResponseWrapper<int>> Handle(DeleteCourseCommand command, CancellationToken cancellationToken)
    {
        var model = await _unitOfWork.ReadRepositoryFor<Course>().GetAsync(command.Id);

        if (model == null)
        {
            return new ResponseWrapper<int>().Failed("Course does not exist.");
        }

        await _unitOfWork.WriteRepositoryFor<Course>().DeleteAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id, "Course deleted successfully.");
    }
}
