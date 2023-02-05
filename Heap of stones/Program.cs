using System.Data;
using System.Diagnostics;
using Heap_of_stones.Models;

namespace Heap_of_stones
{
	internal class Program
	{
		public static int Min => 1;
		public static int Max => 74;

		static void Main(string[] args)
		{
			Console.WriteLine("Введите количество начальных камней в куче (писать через запятую, например: 5,5)");

			// Переменная для хранения введенных пользователем данных
			int[] stones = new int[2];

			// Считываем строку, делим ее по запятой и преобразуем в массив
			try
			{
				stones = Console.ReadLine().Split(',').Select(int.Parse).ToArray();
			}
			// Если пользователь ввел неверные данные
			catch
			{
				Console.WriteLine("Ошибка");
				Environment.Exit(0);
			}

			// Входные данные ограничены Min и Max (1 и 74 соответственно) 
			if (( stones[0] <= Min || stones[0] >= Max ) || ( stones[1] <= Min || stones[1] >= Max ))
			{
				Console.WriteLine("1 <= S1, S2 <= 74");
				Environment.Exit(0);
			}

			// Создаем объект класса HeapStone, который хранит количество камней в кучах
			HeapStone startStones = new HeapStone(stones[0], stones[1]);

			// Создаем экземпляр класса игры, передаём туда входные данные
			Game game = new Game(startStones);

			// Запускаем игру
			game.Start();
		}
	}

	
	

	



}