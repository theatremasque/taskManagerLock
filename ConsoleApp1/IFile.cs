namespace ConsoleApp1;

public interface IFile
{
    Task ReadAsync(FileStream stream, CancellationToken token);
    
    Task WriteAsync(FileStream stream, CancellationToken token, object syncObject);
}