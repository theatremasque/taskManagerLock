private bool isWriting = false;
private int writeIntervalMiliseconds = 2000;
private Random random = new Random();

public Task StartWriterAsync() {
    var pauseIntervalMiliseconds = random.Next(1,9) * 1000;

    return Task.Run(() => {
        while (true) {
            if (isWriting) {
                Console.WriteLine("Already writing!!!!");
            }
            //write
            isWriting = true;
            Console.WriteLine("writing");
            System.Threading.Thread.Sleep(writeIntervalMiliseconds);
            isWriting = false;

            // rest
            System.Threading.Thread.Sleep(pauseIntervalMiliseconds);
        }
    });
}

public Task StartReaderAsync() {
    var pauseIntervalMiliseconds = random.Next(1,5) * 1000;

    return Task.Run(() => {
        while (true) {
            if (isWriting) {
                Console.WriteLine("Cannot read when writing!!!!");
            }

            Console.WriteLine("reading");

            // rest
            System.Threading.Thread.Sleep(pauseIntervalMiliseconds);
        }
    });
}

var tasks = new [] {
    StartReaderAsync(),
    StartReaderAsync(),
    StartReaderAsync(),
    StartReaderAsync(),
    StartWriterAsync(),
    StartWriterAsync()
};

Task.WaitAll(tasks);

код вище імітує декілька процесів запису і читання, які запускаються із різними інтервалами. Ми можемо паралельно читати скільки завгодно, проте під час запису ми не можемо ні читати, ні писати. Зараз після запуску час від часу з'являються записи в консолі про те, що ми намагаємось робити запис і читання одночасно.
Задача полягає в тому, щоб синхронізувати ці потоки таким чином, щоб одночасно відбувався або лише один процес запису, або скільки завгодно процесів читання. Як зробиш - можеш підняти кількість читачів до 10 і кількість записів до 4, просто для приколу.
Обмеження:
читання і запис мають залишатися асинхронними 
