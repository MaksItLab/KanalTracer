using KanalTracer.Infrastructure;
using System.Collections.Generic;


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
		public int countOfMagistrals { get; set; }
		/// <summary>
		/// Количество свободных магистралей в канале кристалла СБИС
		/// </summary>
		public int countOfFreeMagistrals { get; set; }
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
		public List<Magistral> Magistrals { get; set; } = new List<Magistral>();

    }
}
