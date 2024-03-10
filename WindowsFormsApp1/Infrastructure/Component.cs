using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Infrastructure;

namespace KanalTracer.Services
{
	/// <summary>
	/// Класс компонента кристалла СБИС
	/// </summary>
	public class Component
	{
		/// <summary>
		/// Позиция компонента
		/// </summary>
		public Position Position { get; set; }
		/// <summary>
		/// Название компонента
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Уникальный идентификатор компонента
		/// </summary>
		public int ConnectionId { get; set; }
		/// <summary>
		/// Подключаемый компонент
		/// </summary>
		public Component ConnectionComponent { get; set; }


		public Component() { }
	}
}
