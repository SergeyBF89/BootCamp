const int N = 1000; // размер матрицы
const int THREAD_NUMBER = 10; // кол-во потоков

int[,] serialMulRes = new int[N, N]; // будет лежать результат умножения матриц в однопотоке
int[,] threadMulRes = new int[N, N]; // будет лежать результат параллельного умножения матриц

int[,] firstMatrix = MatrixGeneration(N, N);
int[,] secondMatrix = MatrixGeneration(N, N);

SerialMatrixMul(firstMatrix, secondMatrix);
PrepareParallelMatrixMul(firstMatrix, secondMatrix);
Console.WriteLine(EqualityMatrix(serialMulRes, threadMulRes));

// метод заполнения
int[,] MatrixGeneration(int rows, int columns)
{
    Random _rand = new Random();
    int[,] res = new int[rows, columns];
    for(int i = 0; i < res.GetLength(0); i++)
    {
        for(int j = 0; j < res.GetLength(1); j++)
        {
            res[i, j] = _rand.Next(-100, 100);
        }
    }
    return res;
}

// метод умножения матриц друг на друга
void SerialMatrixMul(int[,] a, int[,] b)
{
    if(a.GetLength(1) != b.GetLength(0)) throw new Exception("Нельзя умножать такие матрицы");
    for(int i = 0; i < a.GetLength(0); i++)
    {
        for(int j = 0; j < b.GetLength(1); j++)
        {
            for(int k = 0; k < b.GetLength(0); k++)
            {
                threadMulRes[i, j] += a[i, k] * b[k, j];
            }
        }
    }
}

// метод подготовки к параллельным вычеслениям
void PrepareParallelMatrixMul(int[,] a, int[,] b)
{
    if(a.GetLength(1) != b.GetLength(0)) throw new Exception("Нельзя умножать такие матрицы");
    int eachThreadCalc = N / THREAD_NUMBER;
    var threadsList = new List<Thread>();
    for(int i = 0; i < THREAD_NUMBER; i++)
    {
        int startPos = i * eachThreadCalc;
        int endPos = (i + 1) * eachThreadCalc;
        // если последний поток
        if(i == THREAD_NUMBER - 1) endPos = N;
        threadsList.Add(new Thread(() => ParallelMatrixMul(a, b, startPos, endPos)));
        threadsList[i].Start();
    }
    for(int i = 0; i < THREAD_NUMBER; i++)
    {
        threadsList[i].Join();
    }
}

// метод параллельного вычесления
void ParallelMatrixMul(int[,] a, int[,] b, int startPos, int endPos)
{
    for(int i = startPos; i < endPos; i++)
    {
        for(int j = 0; j < b.GetLength(1); j++)
        {
            for(int k = 0; k < b.GetLength(0); k++)
            {
                serialMulRes[i, j] += a[i, k] * b[k, j];
            }
        }
    }
}
// метод сравнивания матриц
bool EqualityMatrix(int[,] fmatrix, int[,] smatrix)
{
    bool res = true;
    for(int i = 0; i < fmatrix.GetLength(0); i++)
    {
        for(int j = 0; j < smatrix.GetLength(1); j++)
        {
            res = res && (fmatrix[i, j] == smatrix[i, j]);
        }
    }
    return res;
}