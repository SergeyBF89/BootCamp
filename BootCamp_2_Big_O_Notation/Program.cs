/*
int n = 5;
int[] array = new int[n];
for(int i = 0; i < n; i++)
    array[i] = Convert.ToInt32(Console.ReadLine());
Console.WriteLine("[" + string.Join(", ", array) + "]");    
//Console.WriteLine(array[3]);
// Сложность алгоритма О(1)       О - большая буква О    (1) - одно действие
int sum = 0;
for(int i = 0; i < n; i++)
    sum += array[i];
Console.WriteLine(sum);
// Сложность алгоритма О(n)      n - кол-во элементов массива
*/

// таблица умножения
// способ 1
/*
int n = Convert.ToInt32(Console.ReadLine());
for(int i = 1; i <= n; i++)
{
    for(int j = 1; j <= n; j++)
    {
        Console.Write(i * j);
        Console.Write("\t");
    }
    Console.WriteLine();
}
*/
// способ 2(с помощью матрицы)

int n = Convert.ToInt32(Console.ReadLine());
int[, ] matrix = new int[n, n];
for(int i = 0; i < n ; i++)
{
    for(int j = i; j < n; j++)
    {
        matrix[i, j] = (i + 1) * (j + 1);
        matrix[j, i] = (i + 1) * (j + 1);
    }
}
for(int i = 0; i < n; i++)
{
    for(int j = 0; j < n; j++)
    {
        Console.Write(matrix[i, j]);
        Console.Write("\t");
    }
    Console.WriteLine();
}
// сложность алгоритма О(n^2 / 2)       