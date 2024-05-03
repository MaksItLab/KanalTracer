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
        /// Идентификатор магистрали
        /// </summary>
        public int MagisralId {  get; set; }

        public Connection(Component startComponent, Component endComponent)
        {
            this.startComponent = startComponent;
            this.endComponent = endComponent;
        }
    }
}
