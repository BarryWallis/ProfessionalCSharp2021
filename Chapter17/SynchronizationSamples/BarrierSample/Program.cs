// See https://aka.ms/new-console-template for more information

using System.Text;

const int AlphabetSize = 'z' - 'a' + 1;

const int numberOfTasks = 2;
const int partitionSize = 1_000_000;
const int loops = 5;
Dictionary<int, int[][]> taskResults = new();
List<string>[] data = new List<string>[loops];

for (int i = 0; i < loops; i++)
{
    data[i] = new(FillData(partitionSize * numberOfTasks));
}

using Barrier barrier = new(1);
LogBarrierInformation("initial participants in barrier", barrier);
for (int i = 0; i < numberOfTasks; i++)
{
    _ = barrier.AddParticipant();
    int jobNumber = i;
    taskResults.Add(i, new int[loops][]);
    for (int loop = 0; loop < loops; loop++)
    {
        taskResults[i][loop] = new int[AlphabetSize];
    }

    Console.WriteLine($"Main - starting task job {jobNumber}");
    _ = Task.Run(() => CalculationInTask(jobNumber, partitionSize, barrier, data, loops, taskResults[jobNumber]));
}

for (int loop = 0; loop < loops; loop++)
{
    LogBarrierInformation("main task, start signaling and wait", barrier);
    barrier.SignalAndWait();
    LogBarrierInformation("main task waiting completed", barrier);
    int[][] resultCollection1 = taskResults[0];
    int[][] resultCollection2 = taskResults[1];
    IEnumerable<int> resultCollection
        = resultCollection1[loop].Zip(resultCollection2[loop], (c1, c2) => c1 + c2);
    char ch = 'a';
    int sum = 0;
    foreach (int x in resultCollection)
    {
        Console.WriteLine($"{ch++}, count: {x}");
        sum += x;
    }
    LogBarrierInformation($"main task finished loop {loop}, sum: {sum}", barrier);
}
Console.WriteLine("finished all iterations");

static IEnumerable<string> FillData(int size)
{
    const int capacity = 6;
    Random random = new();
    return Enumerable.Range(0, size).Select(i => GetString(random, capacity));
}

static string GetString(Random random, int capacity)
{

    StringBuilder stringBuilder = new(capacity);
    for (int i = 0; i < capacity; i++)
    {
        _ = stringBuilder.Append((char)(random.Next(AlphabetSize) + 'a'));
    }

    return stringBuilder.ToString();
}

static void LogBarrierInformation(string info, Barrier barrier)
    => Console.WriteLine($"Task {Task.CurrentId}: {info}. {barrier.ParticipantCount} current " +
       $"and {barrier.ParticipantsRemaining} remaining participants, phase {barrier.CurrentPhaseNumber}");

static void CalculationInTask(int jobNumber,
                              int partitionSize,
                              Barrier barrier,
                              IList<string>[] collection,
                              int loops,
                              int[][] results)
{
    LogBarrierInformation($"{nameof(CalculationInTask)} started", barrier);

    for (int i = 0; i < loops; i++)
    {
        List<string> data = new(collection[i]);
        int start = jobNumber * partitionSize;
        int end = start + partitionSize;
        Console.WriteLine($"Task {Task.CurrentId} in loop {i}: partition from {start} to {end}");

        for (int j = start; j < end; j++)
        {
            char c = data[j][0];
            results[i][c - 'a']++;
        }
        Console.WriteLine($"Calculation completed from task {Task.CurrentId} in loop {i}. {results[i][0]} times " +
            $"a, {results[i][25]} times z");
        LogBarrierInformation("sending signal and wait for all", barrier);
        barrier.SignalAndWait();
        LogBarrierInformation("waiting completed", barrier);
    }

    barrier.RemoveParticipant();
    LogBarrierInformation("finished task, removed participant", barrier);
}
