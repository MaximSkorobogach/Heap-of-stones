using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heap_of_stones.Enums;
using Heap_of_stones.Models;

namespace Heap_of_stones.Interfaces
{
	/// <summary>
	/// Интерфейс определяющий игрока
	/// </summary>
	abstract class Player
    {
		/// <summary>
		/// Имя игрока (для интерфейса)
		/// </summary>
        public string Name { get; set; }
		/// <summary>
		/// Свойство определяющее является ли ход для данного игрока текущим
		/// </summary>
        public bool Current { get; set; } = false;
		/// <summary>
		/// Конструктор класса игрока
		/// </summary>
		/// <param name="name">Имя игрока</param>
		protected Player(string name)
		{
			Name = name;
		}
		/// <summary>
		/// Метод описывающий логику игры в камни
		/// </summary>
		/// <param name="stones">Текущее колво камней</param>
		/// <param name="position">Выбранная позиция (первая или вторая куча)</param>
		/// <param name="option">Выбранная опция (умножение на 2 или прибавление на 1)</param>
		/// <returns>Новая куча камней</returns>
		/// <exception cref="NotImplementedException">Выбрана неверная опция</exception>
		public HeapStone Move(HeapStone stones, PositionHeap position, PlayerOption option) => option switch
		{
			PlayerOption.Doubling => OptionDoubling(stones, position),
			PlayerOption.Additions => OptionAdditions(stones, position),
			_ => throw new NotImplementedException()
		};
	    /// <summary>
		/// Метод описывающий логику для умножения кучи на 2
		/// </summary>
		/// <param name="stones">Текущее кол во камней</param>
		/// <param name="position">Выбранная позиция в куче</param>
		/// <returns>Новое кол во камней</returns>
		/// <exception cref="NotImplementedException">Выбранная неверная позиция</exception>
		private HeapStone OptionDoubling(HeapStone stones, PositionHeap position) => position switch
		{
			PositionHeap.FirstHeap => new HeapStone(stones.FirstHeap * 2, stones.SecondHeap),
			PositionHeap.SecondHeap => new HeapStone(stones.FirstHeap, stones.SecondHeap * 2),
			_ => throw new NotImplementedException()
		};
	    /// <summary>
	    /// Метод описывающий логику для прибавление кучи на 1
	    /// </summary>
	    /// <param name="stones">Текущее кол во камней</param>
	    /// <param name="position">Выбранная позиция в куче</param>
	    /// <returns>Новое кол во камней</returns>
	    /// <exception cref="NotImplementedException">Выбранная неверная позиция</exception>
		private HeapStone OptionAdditions(HeapStone stones, PositionHeap position) => position switch
		{
			PositionHeap.FirstHeap => new HeapStone(stones.FirstHeap + 1, stones.SecondHeap),
			PositionHeap.SecondHeap => new HeapStone(stones.FirstHeap, stones.SecondHeap + 1),
			_ => throw new NotImplementedException()
		};
	}
}
