namespace ConsoleApp1;

class Program
{
    static async Task Main(string[] args)
    {
        var fileName = "file.txt";
        
        FileChecker(fileName);

        var readerWriter = new FileReaderWriter();
        
        var manager = new ReaderWriterManager(readerWriter);
        
        var fs = await manager.ManageAsync(fileName, 15, GetToken());
        
        Console.ReadLine();

        await fs.DisposeAsync();
    }

    static CancellationToken GetToken()
    {
        return new CancellationTokenSource().Token;
    }

    static void FileChecker(string fileName)
    {
        if (!File.Exists(fileName))
        {
            File.Create(fileName);
        }
    }
}