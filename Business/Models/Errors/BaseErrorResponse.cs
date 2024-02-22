using System.Collections.Generic;

namespace App3.Business.Models.Errors;

public abstract class BaseErrorResponse
{
    public string Type
    {
        get; set;
    }
    public string Title
    {
        get; set;
    }
    public int Status
    {
        get; set;
    }
}
