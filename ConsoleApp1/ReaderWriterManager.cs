namespace ConsoleApp1;

public class ReaderWriterManager
{
    private readonly FileReaderWriter _filer;

    public ReaderWriterManager(FileReaderWriter readerWriter)
    {
        _filer = readerWriter;
    }

    public async Task<FileStream> ManageAsync(string file, int countOfTask, CancellationToken token)
    {
        var tasks = new Task[countOfTask];
        
        var fs = new FileStream(file, FileMode.Open, FileAccess.ReadWrite);

        var locker = new object();
        
        for (var i = 0; i < tasks.Length; i++)
        {
            if (i <= 10)
            {
                tasks[i] = _filer.ReadAsync(fs, token);
            }
            else
            {
                tasks[i] = _filer.WriteAsync(fs, token, locker);
            }
        }

        Task.WaitAll();

        return fs;
    }
}