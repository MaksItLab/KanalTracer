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
		/// Уникальный идентификатор компонента
		/// </summary>
		public int ComponentId { get; set; }
		/// <summary>
		/// Позиция компонента
		/// </summary>
		public Position Position { get; set; }
		/// <summary>
		/// Название компонента
		/// </summary>
		public string Name { get; set; }
		/// <summary>
		/// Идентификатор подключаемого компонента
		/// </summary>
		public int ConnectionComponentId { get; set; }
		/// <summary>
		/// Проверяет, соединен компонент или нет
		/// </summary>
		public bool IsConnected { get; set; } = false;


		public Component() { }
	}
}
