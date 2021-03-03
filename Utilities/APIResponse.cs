using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

public class ApiResponse<T>
{
    public bool IsSucceed { get; set; }

    public List<string> ErrorMessages { get; set; }

    public Exception Exception { get; set; }

    public T Data { get; set; }
    public string Message {get; set;}
}