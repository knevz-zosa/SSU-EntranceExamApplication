using ApplicationLayer.Features.BaseCQS;
using ApplicationLayer.IRepositories;
using Common.Requests;
using Common.Responses;
using Common.Wrapper;
using Domain.Entities;
using Mapster;
using Microsoft.EntityFrameworkCore;

namespace ApplicationLayer.Features.Registration.Undergrad.Commands;

public class CreatePersonalInformationCommand : BaseCreateCommand<PersonalInformationRequest>
{
    public CreatePersonalInformationCommand(PersonalInformationRequest request)
    {
        Request = request;
    }
}
public class CreatePersonalInformationCommandHandler : BaseCreateCommandHandler<CreatePersonalInformationCommand, PersonalInformationRequest>
{
    public CreatePersonalInformationCommandHandler(IUnitOfWork<int> unitOfWork) : base(unitOfWork) { }
    public override async Task<ResponseWrapper<int>> Handle(CreatePersonalInformationCommand command, CancellationToken cancellationToken)
    {
        int applicantId = command.Request.ApplicantId;
        var applicant = await _unitOfWork.ReadRepositoryFor<Applicant>().Entities
             .Include(a => a.Schedule)
             .FirstOrDefaultAsync(a => a.Id == applicantId, cancellationToken);
        string schoolYear = applicant.Schedule.SchoolYear;

        var result = command.Request;
        var trimmedFirstName = result.FirstName.Trim().ToLower();
        var trimmedMiddleName = result.MiddleName?.Trim().ToLower();
        var trimmedLastName = result.LastName.Trim().ToLower();


        var isRegistered = await _unitOfWork.ReadRepositoryFor<Registered>().Entities
            .Include(x => x.Applicant)
                .ThenInclude(x => x.PersonalInformation)
            .AsNoTracking()
            .AnyAsync(a =>
                a.Applicant.PersonalInformation.FirstName.Trim().ToLower() == trimmedFirstName &&
                (a.Applicant.PersonalInformation.MiddleName == null
                    ? trimmedMiddleName == null
                    : a.Applicant.PersonalInformation.MiddleName.Trim().ToLower() == trimmedMiddleName) &&
                a.Applicant.PersonalInformation.LastName.Trim().ToLower() == trimmedLastName &&
                a.Applicant.PersonalInformation.DateofBirth.Date == result.DateofBirth.Date &&
                a.Applicant.Schedule.SchoolYear == schoolYear, cancellationToken);

        if (isRegistered)
            return new ResponseWrapper<int>().Failed(message: "Applicant with this name already exists.");

        var model = result.Adapt<PersonalInformation>();

        model.FirstName = model.FirstName.Trim();
        model.MiddleName = model.MiddleName?.Trim();
        model.LastName = model.LastName.Trim();

        await _unitOfWork.WriteRepositoryFor<PersonalInformation>().CreateAsync(model);
        await _unitOfWork.CommitAsync(cancellationToken);

        return new ResponseWrapper<int>().Success(model.Id);
    }
}

