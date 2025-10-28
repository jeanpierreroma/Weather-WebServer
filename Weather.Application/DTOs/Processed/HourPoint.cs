namespace Weather.Application.DTOs.Processed;

public readonly record struct HourPoint<T>(DateTime Timestamp, T Value);