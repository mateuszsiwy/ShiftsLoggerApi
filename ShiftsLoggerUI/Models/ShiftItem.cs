





using System.Text.Json.Serialization;

public class ShiftItem
{
    [property: JsonPropertyName("id")] public int Id { get; set; }
    [property: JsonPropertyName("name")] public string? Name { get; set; }
    [property: JsonPropertyName("startShift")] public string? StartShift { get; set; }
    [property: JsonPropertyName("endShift")] public string? EndShift { get; set; }
    [property: JsonPropertyName("duration")] public string? Duration { get; set; }
}