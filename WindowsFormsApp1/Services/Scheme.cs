using System.Collections.Generic;
using KanalTracer.Infrastructure;
using KanalTracer.Services;

namespace KanalTracer
{
	/// <summary>
	/// Схема кристалла СБИС
	/// </summary>
	public class Scheme
	{
		/// <summary>
		/// Компоненты, размещаемые на части кристалла
		/// </summary>
		public List<Component> Components { get; set; }
		/// <summary>
		/// Соединения, планируемые в схеме
		/// </summary>
		public List<Connection> Connections { get; set; }
	}
}
