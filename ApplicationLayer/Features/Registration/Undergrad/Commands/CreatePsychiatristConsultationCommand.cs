using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;
using Mapster;

namespace ApplicationLayer.Features.Registration.Undergrad.Commands;
public class CreatePsychiatristConsultationCommand : BaseCreateCommand<PsychiatristConsultationRequest>
{
    public CreatePsychiatristConsultationCommand(PsychiatristConsultationRequest request)
    {
        Request = request;
    }
}
public class CreatePsychiatristConsultationCommandHandler : BaseCreateCommandHandler<CreatePsychiatristConsultationCommand, PsychiatristConsultationRequest>
{
    public CreatePsychiatristConsultationCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreatePsychiatristConsultationCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var model = result.Adapt<PsychiatristConsultation>();
        await _unitOfWork.WriteRepositoryFor<PsychiatristConsultation>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}
