using System;

namespace Infrastructure.APIResponce;

public class Responce<T>
{
    public bool IsSucced { get; set; }
    public string? Message { get; set; }
    public T? Data { get; set; }
    public ErrorType ErrorType { get; set; }

    public static Responce<T> Ok(T? data, string? message = null)=>new()
    {
        
    
            IsSucced = true,
            Message = message,
            Data = data
    };
    
   
   
    public static Responce<T> Fail(string message,ErrorType errorType=ErrorType.Internal)=>new()
    {

        IsSucced = false,
        Message = message,
        ErrorType=errorType
    };
    
}
