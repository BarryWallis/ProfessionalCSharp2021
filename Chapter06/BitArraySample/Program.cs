// See https://aka.ms/new-console-template for more information

using System.Collections;

using BitArraySample;

BitArray bitArray1 = new(9);
bitArray1.SetAll(true);
bitArray1.Set(1, false);
bitArray1[5] = false;
bitArray1[7] = false;
Console.WriteLine($"Initialized: {bitArray1.GetBitsFormat()}");
Console.WriteLine($"NOT {bitArray1.GetBitsFormat()}");
bitArray1.Not();
Console.WriteLine($" =  {bitArray1.GetBitsFormat()}");
Console.WriteLine();

BitArray bitArray2 = new(bitArray1);
bitArray2[0] = true;
bitArray2[1] = false;
bitArray2[4] = true;
Console.WriteLine($"   {bitArray1.GetBitsFormat()}");
Console.WriteLine($"OR {bitArray2.GetBitsFormat()}");
bitArray1.Or(bitArray2);
Console.WriteLine($"=  {bitArray1.GetBitsFormat()}");
Console.WriteLine();

Console.WriteLine($"    {bitArray2.GetBitsFormat()}");
Console.WriteLine($"AND {bitArray1.GetBitsFormat()}");
bitArray2.And(bitArray1);
Console.WriteLine($"=   {bitArray2.GetBitsFormat()}");
Console.WriteLine();

Console.WriteLine($"    {bitArray1.GetBitsFormat()}");
Console.WriteLine($"XOR {bitArray2.GetBitsFormat()}");
bitArray1.Xor(bitArray2);
Console.WriteLine($"=   {bitArray1.GetBitsFormat()}");
Console.WriteLine();
