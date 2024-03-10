using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsFormsApp1.Infrastructure
{
	/// <summary>
	/// Класс позиции компонента
	/// </summary>
	public class Position
	{
		/// <summary>
		/// Координата по оси X
		/// </summary>
		public int X;
		/// <summary>
		/// Координата по оси Y. Отрицательное значение - принадлежит к нижней части кристалла, положительное - к верхней.
		/// </summary>
		public int Y;

		public override string ToString()
		{
			return $"({X};{Y})";
		}
	}
}
