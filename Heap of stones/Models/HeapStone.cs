using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Heap_of_stones.Models
{
	/// <summary>
	/// Класс определяющий кучу камней.
	/// </summary>
	class HeapStone
    {
        /// <summary>
        /// Свойство определяющее первую кучу камней
        /// </summary>
        public int FirstHeap { get; set; }
        /// <summary>
        /// Свойство определяющее вторую кучу камней
        /// </summary>
        public int SecondHeap { get; set; }
        /// <summary>
        /// Конструктор класса, принимает первую и вторую кучу типа int
        /// </summary>
        /// <param name="firstHeap">Первая куча</param>
        /// <param name="secondHeap">Вторая куча</param>
        public HeapStone(int firstHeap, int secondHeap)
        {
            FirstHeap = firstHeap;
            SecondHeap = secondHeap;
        }

    }
}
