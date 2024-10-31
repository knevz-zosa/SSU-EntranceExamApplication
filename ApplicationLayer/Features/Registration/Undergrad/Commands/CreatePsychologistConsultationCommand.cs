using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;
using Mapster;

namespace ApplicationLayer.Features.Registration.Undergrad.Commands;
public class CreatePsychologistConsultationCommand : BaseCreateCommand<PsychologistConsultationRequest>
{
    public CreatePsychologistConsultationCommand(PsychologistConsultationRequest request)
    {
        Request = request;
    }
}
public class CreatePsychologistConsultationCommandHandler : BaseCreateCommandHandler<CreatePsychologistConsultationCommand, PsychologistConsultationRequest>
{
    public CreatePsychologistConsultationCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreatePsychologistConsultationCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var model = result.Adapt<PsychologistConsultation>();
        await _unitOfWork.WriteRepositoryFor<PsychologistConsultation>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}

