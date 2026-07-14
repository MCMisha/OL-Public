namespace WebApplicationOperaLublin.Exceptions;

public class ContactSectionNotFoundException(int sectionId) : Exception($"Contact section {sectionId} not found");