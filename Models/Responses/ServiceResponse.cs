using PrototypeUserService.Enums;
namespace PrototypeUserService.Models.Responses;

public class ServiceResponse<T>
{
    public T? Data { get; set; }                        //The data that is returned.
    public bool Success => Error == ErrorCode.None;     //True if success (error code is none).
    public ErrorCode Error { get; set; }                //Sets the error code to origin of error or none.
    public string? Message { get; set; }                //Additional message of the error code.


    public ServiceResponse(T data)
    {
        Data = data;
        Error = ErrorCode.None;
    }

    public ServiceResponse(string SuccesMessage)
    {
        Message = SuccesMessage;
        Error = ErrorCode.None;
    }

    public ServiceResponse(ErrorCode error, string? message = "something went wrong")
    {
        Data = default;
        Error = error;
        Message = message;
    }
}