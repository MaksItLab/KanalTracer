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
		public IEnumerable<Component> Components { get; set; }
		public IEnumerable<Connection> Connections { get; set; }
		public IEnumerable<Magistral> Magistrals { get; set; }







    }
}
