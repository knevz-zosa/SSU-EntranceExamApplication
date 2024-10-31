namespace Common.Wrapper;
public class ResponseWrapper<T>
{
    public bool IsSuccessful { get; set; }
    public List<string> Messages { get; set; } = new List<string>();
    public T Data { get; set; }

    public ResponseWrapper<T> Success(T data, string message = null)
    {
        IsSuccessful = true;
        Messages = message == null ? new List<string>() : new List<string> { message };
        Data = data;

        return this;
    }

    public ResponseWrapper<T> Failed(string message)
    {
        IsSuccessful = false;
        Messages = new List<string> { message };
        return this;
    }

    public void EnsureSuccess()
    {
        if (!IsSuccessful)
        {
            throw new ResponseException(string.Join(", ", Messages));
        }
    }
}
public class ResponseException : Exception
{
    public ResponseException(string message) : base(message)
    {
    }
}
