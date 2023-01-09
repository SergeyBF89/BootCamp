// Вывести максимальную сумму m подряд элементов массива.
using System.Diagnostics; // тестирование
int size = 1000000;
int m = 30000;
int[] array = Enumerable.Range(1, size)
                        .Select(item => new Random().Next(10))
                        .ToArray(); // задали массив
//Console.WriteLine($"[{string.Join(", " , array)}]");    

Stopwatch sw = new(); // таймер времени
sw.Start(); // запускаем таймер
// Способ 1, медленный подсчет по времени
int max = 0;
for(int i = 0; i < array.Length - m; i++)
{
    int t = 0;
    for(int j = i; j < i + m; j++) t += array[j];
    if(t > max) max = t;
}
sw.Stop(); // останавливаем таймер
// Способ 2, быстрый подсчет по времени
int max1 = 0;
for(int j = 0; j < m; j++) max1 += array[j];
int t1 = max1;
for(int i = 1; i < array.Length - m; i++)
{
    t1 = t1 - array[i - 1] + array[i + (m - 1)];
    if(t1 > max1) max1 = t1;
}
sw.Stop(); // останавливаем таймер 
Console.WriteLine($"time = {sw.ElapsedMilliseconds}");
Console.WriteLine($"Способ 1: {max}");
Console.WriteLine($"Способ 2: {max1}");


