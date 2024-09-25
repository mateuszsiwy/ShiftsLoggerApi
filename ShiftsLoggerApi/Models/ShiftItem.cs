namespace ShiftsLoggerApi.Models
{
    public class ShiftItem
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? StartShift { get; set; }
        public string? EndShift { get; set; }
        public string? Duration { get; set; }
    }

    public class ShiftItemDTO
    {
        public string? Name { get; set; }
        public string? StartShift { get; set; }
        public string? EndShift { get; set; }
        public string? Duration { get; set; }
    }
}
