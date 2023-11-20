﻿using System.Text.Json.Serialization;

namespace BGNet.TestAssignment.DataAccess.Entities;

public class User : IEntity
{
    public int Id { get; set; }
    [JsonIgnore]
    public string Password { get; set; } = null!;
    public string Username { get; set; } = null!;
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public string Address { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }
}
