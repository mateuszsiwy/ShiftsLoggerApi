
using Microsoft.EntityFrameworkCore;
using System;


namespace ShiftsLoggerApi.Models
{
    public class ShiftContext: DbContext 
    {
        public ShiftContext(DbContextOptions<ShiftContext> options) : base(options) { }

        public DbSet<ShiftItem>? ShiftItems { get; set; } = null;

    }
}
