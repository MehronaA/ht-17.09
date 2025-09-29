using System;

namespace Infrastructure.APIResponce;

public class PagedResponse<T> : Responce<T>
{
    public int Page { get; set; }
    public int Size { get; set; }
    public int TotalCount { get; set; }
    public int TotalPage { get; set; }
    public static PagedResponse<T> Ok(T? data, int page, int size, int totalCount, string? message = null) => new()
    {
        IsSucced = true,
        Data = data,
        Message = message,
        Page = page,
        Size = size,
        TotalCount = totalCount,
        TotalPage = (int)Math.Ceiling((double)totalCount / size)
    };
    public static PagedResponse<T> Fail(string message, ErrorType errorType)
    { 
        IsSucced = false,
        Message = message,
        ErrorType=errorType
    }
  






}
