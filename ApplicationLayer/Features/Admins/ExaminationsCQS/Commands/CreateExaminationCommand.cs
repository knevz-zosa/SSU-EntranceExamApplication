using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Wrapper;
using Domain.Entities;
using Mapster;

namespace ApplicationLayer.Features.Admins.ExaminationsCQS.Commands;
public class CreateExaminationCommand : BaseCreateCommand<ExaminationRequest>
{
    public CreateExaminationCommand(ExaminationRequest request)
    {
        Request = request;
    }
}
public class CreateExaminationCommandHandler : BaseCreateCommandHandler<CreateExaminationCommand, ExaminationRequest>
{
    public CreateExaminationCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork){}
    public async override Task<ResponseWrapper<int>> Handle(CreateExaminationCommand command, CancellationToken cancellationToken)
    {
        var result = command.Request;

        var model = result.Adapt<Examination>();
        await _unitOfWork.WriteRepositoryFor<Examination>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id, "Exam result has been recorded successfully.");
    }
}
