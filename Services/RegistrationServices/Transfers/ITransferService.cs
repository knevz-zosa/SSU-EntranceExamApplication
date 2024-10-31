using Common.Requests;
using Common.Wrapper;

namespace Services.RegistrationServices.Transfers;
public interface ITransferService
{
    Task<ResponseWrapper<int>> Create(TransferRequest request);

}
