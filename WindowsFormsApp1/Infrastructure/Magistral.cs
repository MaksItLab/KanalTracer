using System.Collections.Generic;

namespace KanalTracer.Infrastructure
{
	/// <summary>
	/// Класс магистрали на кристалле СБИС
	/// </summary>
	public class Magistral
	{
		/// <summary>
		/// Номер магистрали 
		/// </summary>
		private readonly int _id;

		/// <summary>
		/// Длина магистрали
		/// </summary>
		private readonly int _lenght;

		/// <summary>
		/// Список позиций на магистрали
		/// </summary>
		public List<int> ELInMagistral = new List<int>();

		/// <summary>
		/// Свойство длины магистрали
		/// </summary>
		public int Lenght { get { return _lenght; } }
		/// <summary>
		/// Свойство номера магистрали
		/// </summary>
		public int ID { get { return _id; } }


		/// <summary>
		/// Конструктор с 2 параметарми
		/// </summary>
		/// <param name="id">Номер магистрали</param>
		/// <param name="lenght">Длина магистрали</param>
		public Magistral(int id, int lenght)
		{
			_id = id;
			_lenght = lenght;
		}

	}
}
