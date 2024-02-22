namespace App3.Business.Models.DTOs;

public class AddMessageToComplaintDTO
{
    public Complaint complaint
    {
        get; set;
    }
    public Message message
    {
        get; set;
    }
}
