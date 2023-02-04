using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Heap_of_stones.Enums;
using Heap_of_stones.Interfaces;

namespace Heap_of_stones.Models
{
	/// <summary>
	/// Класс определяющий игрока человека (если требуется)
	/// </summary>
	class Human : Player
	{
		// Наследуется от абстрактного класса Player, наследует все методы (доп логика не требуется)
		public Human(string name) : base(name)
		{
		}
	}
}
