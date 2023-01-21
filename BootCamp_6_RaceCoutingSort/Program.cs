//  "Гонка" в параллельных вычислениях

const int THREADS_NUMBER = 4; // число потоков
const int N = 100000; // размер массива
object locker = new object();

//int[] array = {-10, -5, -9, 0, 2, 5, 1, 3, 1, 0, 1};
//int[] sortedArray = CountingSortExtended(array);
Random rand = new Random();
int[] resSeria = new int[N].Select(r => rand.Next(0, 5)).ToArray();
int[] resParallel = new int[N];
Array.Copy(resSeria, resParallel, N);

//Console.WriteLine(string.Join(' ', resSeria));

CountingSortExtended(resSeria);
PrepareParallelCoutingSort(resParallel);
Console.WriteLine(EqualityMatrix(resSeria, resParallel));

//Console.WriteLine(string.Join(' ', resSeria));
//Console.WriteLine(string.Join(' ', resParallel));


void PrepareParallelCoutingSort(int[] inputArray)
{
    int max = inputArray.Max();
    int min = inputArray.Min();

    int offset = -min;
    int[] counters = new int[max + offset + 1];

    int eachThreadCalc = N / THREADS_NUMBER;
    var threadParall = new List<Thread>();

    for (int i = 0; i < THREADS_NUMBER; i++)
    {
        int startPos = i * eachThreadCalc;
        int endPos = (i + 1) * eachThreadCalc;
        if (i == THREADS_NUMBER - 1) endPos = N;
        threadParall.Add(new Thread(() => CoutingSortParallel(inputArray, counters, offset, startPos, endPos)));
        threadParall[i].Start();
    }
    foreach (var thread in threadParall)
    {
        thread.Join();
    }
    int index = 0;
    for (int i = 0; i < counters.Length; i++)
    {
        for (int j = 0; j < counters[i]; j++)
        {
            inputArray[index] = i - offset;
            index++;
        }
    }
}


void CoutingSortParallel(int[] inputArray, int[] counters, int offset, int startPos, int endPos)
{
    for (int i = startPos; i < endPos; i++)
    {
        counters[inputArray[i] + offset]++;
    }
}



void CountingSortExtended(int[] inputArray)
{
    int max = inputArray.Max();
    int min = inputArray.Min();

    int offset = -min;

    int[] counters = new int[max + offset + 1];

    for (int i = 0; i < inputArray.Length; i++)
    {
        counters[inputArray[i] + offset]++;
    }

    int index = 0;
    for (int i = 0; i < counters.Length; i++)
    {
        for (int j = 0; j < counters[i]; j++)
        {
            inputArray[index] = i - offset;
            index++;
        }
    }
}

bool EqualityMatrix(int[] fmatrix, int[] smatrix)
{
    bool res = true;
    for (int i = 0; i < N; i++)
    {

        res = res && (fmatrix[i] == smatrix[i]);
    }
    return res;
}