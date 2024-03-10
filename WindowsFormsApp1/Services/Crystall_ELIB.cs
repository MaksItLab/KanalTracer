using KanalTracer.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KanalTracer
{
	/// <summary>
	/// Кристалл СБИС
	/// </summary>
	public class Crystall_ELIB
	{
		/// <summary>
		/// Количество магистралей в канале кристалла СБИС
		/// </summary>
		public int countOfMagistrals;
		/// <summary>
		/// Длинна канала
		/// </summary>
		public int Lenght { get; set; }
		/// <summary>
		/// Схема соединений элементов канала
		/// </summary>
		public Scheme Scheme { get; set; }
		/// <summary>
		/// Магистрали, размещаемые в канале СБИС
		/// </summary>
		public IEnumerable<Magistral> Magistrals { get; set; }




	}
}
