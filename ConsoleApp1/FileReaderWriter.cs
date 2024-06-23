namespace ConsoleApp1;

public class FileReaderWriter : IFile
{
    private bool _isWriting;
    
    public async Task ReadAsync(FileStream stream, CancellationToken token)
    {
        var pauseInMilliseconds = Random.Shared.Next(1, 9) * 1000;
        
        await Task.Run(() => {
            while (true)
            {
                if (!_isWriting && stream.CanRead) // observe the status of file
                {
                    var read = stream.ReadByte();
                
                    Console.WriteLine($"reading: {read}");
                }
                
                Task.Delay(pauseInMilliseconds, token); // hold to read
            }
            
        }, token);
        
    }

    public async Task WriteAsync(FileStream stream, CancellationToken token, object syncObject)
    {
        var pauseInMilliseconds = Random.Shared.Next(1, 5) * 1000;
        
        await Task.Run(() =>
        {
            while (true) 
            {
                if (_isWriting)
                {
                    Console.WriteLine("already writing!");
                }

                lock (syncObject)
                {
                    _isWriting = true; // critical section start
                    
                    if (_isWriting && !stream.CanWrite) 
                    {
                        Task.Delay(pauseInMilliseconds, token); // hold to write
                    }
                    else
                    {
                        stream.WriteByte((byte)Random.Shared.Next(1,100));
                        
                        Console.WriteLine("writing");
                        
                        Task.Delay(2000, token);
                    }
                    
                    _isWriting = false; // critical section end
                }
            }
        }, token);
    }
}