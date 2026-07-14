namespace WebApplicationOperaLublin.Exceptions;

public class InvalidArtistCategoryException(int category) : Exception($"Kategoria artysty '{category}' nie istnieje.");