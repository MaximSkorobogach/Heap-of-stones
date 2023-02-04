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
	/// Класс определяющий логику компьютера
	/// </summary>
    internal class Computer : Player
    {
	    // Наследуется от абстрактного класса Player, наследует все методы (доп логика не требуется)
	    public Computer(string name) : base(name)
	    {
	    }
    }
}
