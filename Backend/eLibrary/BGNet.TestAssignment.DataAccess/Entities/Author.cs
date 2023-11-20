﻿namespace BGNet.TestAssignment.DataAccess.Entities;

public class Author : IEntity
{
    public int Id { get; set; }
    public string FirstName { get; set; } = null!;
    public string LastName { get; set; } = null!;
    public DateTime DateOfBirth { get; set; }

    public ICollection<Book> Books { get; set; } = new List<Book>();
}
