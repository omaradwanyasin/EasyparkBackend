using System;

public class ReservationModel
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string UserId { get; set; }
    public string GarageId { get; set; }
    public DateTime ReservationDate { get; set; }
    public string Status { get; set; }
    public string Comments { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }

    // Additional properties
    public string VehicleType { get; set; }
    public bool IsConfirmed { get; set; }
    public DateTime? ConfirmedAt { get; set; }

    // Navigation properties
    public User User { get; set; }
    public Garage Garage { get; set; }
}

public class User
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Email { get; set; }
    public string PhoneNumber { get; set; }
}

public class Garage
{
    public string Id { get; set; }
    public string Name { get; set; }
    public string Address { get; set; }
    public int Capacity { get; set; }
}
