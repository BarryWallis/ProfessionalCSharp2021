// See https://aka.ms/new-console-template for more information

using ReaderWriterLockSample;

using ReaderWriter readerWriter = new();
TaskFactory taskFactory = new(TaskCreationOptions.LongRunning, TaskContinuationOptions.None);
Task[] tasks = new Task[6];
tasks[0] = taskFactory.StartNew(readerWriter.WriterMethod, 1);
await Task.Delay(5);
tasks[1] = taskFactory.StartNew(readerWriter.ReaderMethod, 1);
tasks[2] = taskFactory.StartNew(readerWriter.ReaderMethod, 2);
tasks[3] = taskFactory.StartNew(readerWriter.WriterMethod, 2);
tasks[4] = taskFactory.StartNew(readerWriter.ReaderMethod, 3);
tasks[5] = taskFactory.StartNew(readerWriter.ReaderMethod, 4);
Task.WaitAll(tasks);
