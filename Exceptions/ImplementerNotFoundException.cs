namespace WebApplicationOperaLublin.Exceptions;

public class ImplementerNotFoundException(int implementerId) : Exception($"Implementer {implementerId} not found");