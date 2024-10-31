using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;
using Mapster;
using MediatR;

namespace ApplicationLayer.Features.Registration.Undergrad.Commands;
public class CreateCouncelorConsultationCommand : BaseCreateCommand<CouncelorConsultationRequest>
{
    public CreateCouncelorConsultationCommand(CouncelorConsultationRequest request)
    {
        Request = request;
    }
}
public class CreateCouncelorConsultationCommandHandler : BaseCreateCommandHandler<CreateCouncelorConsultationCommand, CouncelorConsultationRequest>
{
    public CreateCouncelorConsultationCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreateCouncelorConsultationCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var model = result.Adapt<CouncelorConsultation>();
        await _unitOfWork.WriteRepositoryFor<CouncelorConsultation>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}
