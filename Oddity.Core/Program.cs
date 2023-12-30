using System.Linq;

namespace Oddity.Core;
public class Program
{
	public static void Main()
	{
		var file = ReadFile();
		Console.WriteLine($"File size: {file.Length}");
		//PrintByteFile(file);
		var countMatrix = GetByteCountMatrix(file);
		var encodingMap = GenerateEncodingMap(countMatrix);

		PrintCountMatrix(encodingMap);
		var columnResult = GetColumnEncode(file, encodingMap);
		var compressedFile = ConvertToBoolArray(columnResult);
		SaveBoolArrayToFile(compressedFile);
	}

	static byte[] GetColumnEncode(byte[] byteArray, Dictionary<byte, Counter> countResult)
	{
		var columnResult = new List<byte>();

		foreach (var byteValue in byteArray)
		{
			var counter = countResult[byteValue];
			columnResult.Add((byte)counter.Column);
		}

		return columnResult.ToArray();
	}
	static Dictionary<byte, Counter> GenerateEncodingMap(int[] byteCountMatrix)
	{
		Dictionary<byte, Counter> byteCountDict = new Dictionary<byte, Counter>();

		for (int x = 0; x < byteCountMatrix.Length; x++)
		{
			byteCountDict[(byte)(x)] = new Counter()
			{
				Byte = (byte)(x),
				Count = byteCountMatrix[x]
			};
		}
		var counter = 0;
		return byteCountDict.OrderByDescending(kv => kv.Value.Count).ThenByDescending(kv => kv.Key).ToDictionary(kv => kv.Key, kv =>
		{
			kv.Value.Column = counter / 16;
			kv.Value.Row = counter % 16;
			counter++;
			return kv.Value;
		});
	}

	public static void PrintCountMatrix(Dictionary<byte, Counter> countResult)
	{
		var counter = 1;
		for (int x = 1; x <= 16; x++)
		{
			for (int y = 1; y <= 16; y++)
			{
				var current = countResult.Take(counter++).Last();
				Console.WriteLine(current.ToString());
			}
			Console.WriteLine("\n");
		}
	}

	public static void PrintByteFile(byte[] byteArray)
	{
		foreach (var b in byteArray)
		{

			Console.WriteLine(b);
		}
	}

	public static int[] GetByteCountMatrix(byte[] byteArray)
	{
		int[] byteCountMatrix = new int[256]; // 8 by 32 matrix for 256 values

		foreach (byte b in byteArray)
		{
			byteCountMatrix[b]++;
		}

		return byteCountMatrix;
	}

	public static byte[] ReadFile()
	{
		return File.ReadAllBytes("./files/original.png");
		// return File.ReadAllBytes("./files/compressed.sint");
	}

	public static void SaveBoolArrayToFile(bool[] boolArray)
	{
		byte[] byteArray = new byte[(boolArray.Length + 7) / 8];

		for (int i = 0; i < boolArray.Length; i++)
		{
			if (boolArray[i])
			{
				byteArray[i / 8] |= (byte)(1 << (i % 8));
			}
		}

		File.WriteAllBytes("./files/compressed.sint", byteArray);
	}

	public static bool[] ConvertToBoolArray(byte[] byteArray)
	{
		bool[] boolArray = new bool[byteArray.Length * 4];

		for (int i = 0; i < byteArray.Length; i++)
		{
			int value = byteArray[i];
			for (int j = 0; j < 4; j++)
			{
				int bitPosition = i * 4 + j;
				boolArray[bitPosition] = ((value >> (j * 4)) & 0x01) != 0;
			}
			Console.WriteLine($"b = {byteArray[i]} \t [{boolArray[i * 4]}, {boolArray[i * 4 + 1]}, {boolArray[i * 4 + 2]}, {boolArray[i * 4 + 3]}]");
		}

		return boolArray;
	}
}
