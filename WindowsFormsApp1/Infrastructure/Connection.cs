using KanalTracer.Infrastructure;
using System.Collections.Generic;


namespace KanalTracer.Services
{
    /// <summary>
    /// Класс соединения на кристалле СБИС
    /// </summary>
	public class Connection
	{
        /// <summary>
        /// Начальный компонент
        /// </summary>
		public Component startComponent;
        /// <summary>
        /// Конечный компонент
        /// </summary>
		public Component endComponent;
        /// <summary>
        /// Словарь, в котором содержится путь соединения вида "magistral" : [1,4].
        /// </summary>
        public static Dictionary<Magistral, int[]> Path = new Dictionary<Magistral, int[]>();

        public Connection(Component startComponent, Component endComponent)
        {
            this.startComponent = startComponent;
            this.endComponent = endComponent;
        }
    }
}
