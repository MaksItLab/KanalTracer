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
		public IEnumerable<Component> Components { get; set; }
		/// <summary>
		/// Соединения, планируемые в схеме
		/// </summary>
		public IEnumerable<Connection> Connections { get; set; }







	}
}
