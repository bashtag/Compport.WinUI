#nullable enable
using System;
using System.Collections.Generic;

namespace App3.Business.Models;

public enum CargoStatus
{
    Base,
    NotReceived,
    Received,
    Processing,
    Shipped,
    Delivered
}

public enum ComplaintStatus
{
    Open,
    Support,
    Technician,
    CargoToTecnician,
    CargoToCustomer,
    Closed
}

public class Complaint
{
    public int Id
    {
        get; set;
    }

    public int? UserId
    {
        get; set;
    }

    public User? User
    {
        get; set;
    }

    public string? PhoneNumber
    {
        get; set;
    }

    public int? DeviceId
    {
        get; set;
    }
    public Device? Device { get; set; }

    public DateTime? Timestamp { get; set; } = DateTime.Now;

    public string Description { get; set; } = string.Empty;

    public ICollection<Message> Messages { get; } = new List<Message>();

    public ComplaintStatus Status { get; set; } = ComplaintStatus.Open;

    public CargoStatus CargoStatus { get; set; } = CargoStatus.Base;

    public bool IsActive { get; set; } = true;
}
