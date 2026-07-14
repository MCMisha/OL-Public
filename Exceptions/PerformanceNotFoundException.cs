namespace WebApplicationOperaLublin.Exceptions;

public class PerformanceNotFoundException(int performanceId)
    : Exception($"Performance with id {performanceId} was not found");