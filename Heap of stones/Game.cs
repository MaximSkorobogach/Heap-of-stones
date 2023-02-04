using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heap_of_stones.Enums;
using Heap_of_stones.Interfaces;
using Heap_of_stones.Models;

namespace Heap_of_stones
{
	/// <summary>
	///Класс описывающий игру, содержит свойства игры такие как количество камней в кучах, игроки и их данные, последний сходивший игрок и т.д.
	/// </summary>
	class Game
	{
		/// <summary>
		/// Класс камней, содержит данные об камнях в куче 1 и 2
		/// </summary>
		public HeapStone Stones { get; set; }
		/// <summary>
		/// Счетчик ходов
		/// </summary>
		public int MoveCount { get; set; } = 0;
		/// <summary>
		/// Количество камней для победы
		/// </summary>
		public int WinCount { get; set; } = 74;
		/// <summary>
		/// Список игроков
		/// </summary>
		public List<Player> Players { get; set; }
		/// <summary>
		/// Победитель
		/// </summary>
		public Player Winner { get; set; }
		/// <summary>
		/// Последний сходивший игрок
		/// </summary>
		public Player LastPlayer { get; set; }
		/// <summary>
		/// Конструктор класса Game
		/// </summary>
		/// <param name="startStones"> Количество стартовых камней</param>
		public Game(HeapStone startStones)
		{
			Stones = startStones;
		}
		/// <summary>
		/// Метод передающий в консоль статус текущей игры. Сколько камней в кучах, кто сейчас ходит, количество ходов и т.д.
		/// </summary>
		/// <param name="player">Текущий игрок</param>
		private void ShowStatus(Player player)
			=> Console.WriteLine(Environment.NewLine +
			                     $"Куча камней: ({Stones.FirstHeap},{Stones.SecondHeap}), " +
								 $"вместе: {Stones.FirstHeap + Stones.SecondHeap}, " +
								 $"ходов: {MoveCount}, " +
								 $"текущий ход у игрока: {player.Name}, " +
								 $"наибольшее кол-во ходов может быть: {WinCount - ( Stones.FirstHeap + Stones.SecondHeap )}");
		/// <summary>
		/// Метод запускающий игру
		/// </summary>
		public void Start()
		{
			Players = new List<Player>()
			{
				new Computer("Петя"),
				new Computer("Ваня")
			};

			Players[0].Current = true;

			Console.WriteLine($"Игра началась. Начальное количество камней: ({Stones.FirstHeap},{Stones.SecondHeap})");

			Console.WriteLine($"Первый играет: {Players[0].Name}");

			for (int index = 0; index < Players.Count;)
			{
				if (Players[index].Current)
				{
					ShowStatus(Players[index]);

					if (Players[index].GetType() == typeof(Human))
					{
						Stones = MovingHuman(( Players[index] as Human )!, Stones);
					}
					else
					{
						Stones = MovingComputer(( Players[index] as Computer )!, Stones);
					}

					LastPlayer = Players[index];
					Players[index].Current = false;

					MoveCount++;

					if (Stones.FirstHeap + Stones.SecondHeap > WinCount)
					{
						Winner = Players[index];
						break;
					}

					index = index == Players.Count - 1 ? 0 : index + 1;

					Players[index].Current = true;
				}
				else
				{
					index++;
				}
			}

			Console.WriteLine($"Победитель игры: {Winner.Name}");
			Console.WriteLine($"Куча камней: ({Stones.FirstHeap},{Stones.SecondHeap})");

		}
		/// <summary>
		/// Метод описывающий ход компьютера. Если компьютер не видит победы прямо сейчас, то выбирает наибольший из возможных.
		/// </summary>
		/// <param name="computer">Класс игрока (компьютера)</param>
		/// <param name="stones">Текущая стопка камней</param>
		/// <returns></returns>
		private HeapStone MovingComputer(Computer computer, HeapStone stones)
		{
			HeapStone[] options = new HeapStone[4];
			HeapStone newStones = new HeapStone(stones.FirstHeap, stones.SecondHeap);

			options[0] = computer.Move(newStones, PositionHeap.FirstHeap, PlayerOption.Additions);
			options[1] = computer.Move(newStones, PositionHeap.SecondHeap, PlayerOption.Doubling);
			options[2] = computer.Move(newStones, PositionHeap.FirstHeap, PlayerOption.Doubling);
			options[3] = computer.Move(newStones, PositionHeap.SecondHeap, PlayerOption.Additions);

			HeapStone[][] nextMoves = new HeapStone[4][];

			for (int i = 0; i < nextMoves.Length; i++)
			{
				nextMoves[i] = new HeapStone[4];

				nextMoves[i][0] = computer.Move(options[i], PositionHeap.FirstHeap, PlayerOption.Additions);
				nextMoves[i][1] = computer.Move(options[i], PositionHeap.SecondHeap, PlayerOption.Doubling);
				nextMoves[i][2] = computer.Move(options[i], PositionHeap.FirstHeap, PlayerOption.Doubling);
				nextMoves[i][3] = computer.Move(options[i], PositionHeap.SecondHeap, PlayerOption.Additions);
			}

			HeapStone bestMove = null;

			for (int i = 0; i < options.Length; i++)
			{
				if (options[i].FirstHeap + options[i].SecondHeap > WinCount)
				{
					Console.WriteLine(Environment.NewLine + $"{computer.Name} нашел выигрышный ход");
					Console.WriteLine("Возможные ходы компьютера: ");

					for (int j = 0; j < options.Length; j++)
					{
						Console.Write($"({options[j].FirstHeap},{options[j].SecondHeap}) ");
					}

					Console.WriteLine($"Компьютер выбрал ход: ({options[i].FirstHeap},{options[i].SecondHeap})");

					return options[i];
				}

				bool losing = false;

				for (int j = 0; j < nextMoves[i].Length; j++)
				{
					if (nextMoves[i][j].FirstHeap + nextMoves[i][j].SecondHeap > WinCount)
					{
						losing = true;
						break;
					}
				}

				if (!losing)
				{
					if (bestMove == null)
					{
						bestMove = options[i];
					}
					else
					{
						if (bestMove.FirstHeap + bestMove.SecondHeap < options[i].FirstHeap + options[i].SecondHeap)
						{
							bestMove = options[i];
						}
					}
				}
			}

			Console.WriteLine($"Возможные ходы {computer.Name}: ");

			for (int j = 0; j < options.Length; j++)
			{
				Console.Write($"({options[j].FirstHeap},{options[j].SecondHeap}) ");
			}

			Console.WriteLine();

			if (bestMove == null)
			{
				Console.WriteLine(Environment.NewLine + $"{computer.Name} понял, что следующий ход проигрышный при любом варианте, поэтому ходит наименьшим ходом");

				HeapStone minMove = options[0];

				for (int i = 0; i < options.Length; i++)
				{
					if (minMove.FirstHeap + minMove.SecondHeap > options[i].FirstHeap + options[i].SecondHeap)
					{
						minMove = options[i];
					}
				}
				
				return minMove;
			}

			Console.WriteLine($"{computer.Name} выбрал ход: ({bestMove.FirstHeap},{bestMove.SecondHeap})");

			return bestMove;
		}
		/// <summary>
		/// Метод описывающий ход игрока.
		/// </summary>
		/// <param name="human">Класс игрока</param>
		/// <param name="stones">Текущая стопка камней</param>
		/// <returns></returns>
		private HeapStone MovingHuman(Human human, HeapStone stones)
		{
			Console.WriteLine("Выберите кучу (написать 1 или 2)");

			PositionHeap position = (PositionHeap)CheckAnswer();

			Console.WriteLine("Выберите действие, 1 - умножение кучи на 2; 2 - добавление к куче единицы");

			PlayerOption option = (PlayerOption)CheckAnswer();

			return human.Move(stones, position, option);
		}
		/// <summary>
		/// Вспомогательный метод для проверки ответа игрока.
		/// </summary>
		/// <returns></returns>
		private int CheckAnswer()
		{
			while (true)
			{
				int number;

				try
				{
					number = Convert.ToInt32(Console.ReadLine());
				}
				catch
				{
					Console.WriteLine("Неверный ввод. Введите 1 или 2");
					continue;
				}

				if (number != 1 & number != 2)
				{
					Console.WriteLine("Неверный ввод. Введите 1 или 2");
					continue;
				}

				return number;
			}
		}
	}
}
