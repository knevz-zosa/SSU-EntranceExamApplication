using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.CustomClasses;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;
using Mapster;

namespace ApplicationLayer.Features.Registration.Undergrad.Commands;
public class CreateApplicantCommand : BaseCreateCommand<ApplicantRequest>
{
    public CreateApplicantCommand(ApplicantRequest request)
    {
        Request = request;
    }
}
public class CreateApplicantCommandHandler : BaseCreateCommandHandler<CreateApplicantCommand, ApplicantRequest>
{
    public CreateApplicantCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreateApplicantCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var model = result.Adapt<Applicant>();
        model.ControlNo = Utility.GenerateControlNumber();
        await _unitOfWork.WriteRepositoryFor<Applicant>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}
