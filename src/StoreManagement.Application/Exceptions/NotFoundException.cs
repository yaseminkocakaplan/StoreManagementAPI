namespace StoreManagement.Application.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message)
    {
    }

    public NotFoundException(string name, object key) 
        : base($"'{name}' kaydı ({key}) bulunamadı.")
    {
    }
}