namespace WebApplicationOperaLublin.Exceptions;

public class PerformanceEventNotFoundException(int performanceEventId)
    : Exception($"PerformanceEvent with id {performanceEventId} was not found");