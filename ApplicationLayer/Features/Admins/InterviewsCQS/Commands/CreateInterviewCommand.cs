using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Admins.InterviewsCQS.Commands;
public class CreateInterviewCommand : BaseCreateCommand<InterviewRequest>
{
    public CreateInterviewCommand(InterviewRequest request)
    {
        Request = request;
    }
}
public class CreateInterviewCommandHandler : BaseCreateCommandHandler<CreateInterviewCommand, InterviewRequest>
{
    public CreateInterviewCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork){}
    public override async Task<ResponseWrapper<int>> Handle(CreateInterviewCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var existingResult = await _unitOfWork.ReadRepositoryFor<Interview>()
            .Entities.FirstOrDefaultAsync(x => x.CourseId == command.Request.CourseId && x.ApplicantId == command.Request.ApplicantId);

        if (existingResult != null)
            return new ResponseWrapper<int>().Failed(message: "Applicant has already been interviewed in this program.");

        var model = result.Adapt<Interview>();
        await _unitOfWork.WriteRepositoryFor<Interview>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id, "Interview result has been recorded successfully.");
    }
}
