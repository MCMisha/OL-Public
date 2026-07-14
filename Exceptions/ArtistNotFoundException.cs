namespace WebApplicationOperaLublin.Exceptions;

public class ArtistNotFoundException(int artistId) : Exception($"Artist with id {artistId} not found");