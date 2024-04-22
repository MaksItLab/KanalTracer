using KanalTracer;
using KanalTracer.Infrastructure;
using KanalTracer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsFormsApp1.Infrastructure;

namespace Algorithm
{
	public class Data
	{
		public static Crystall_ELIB CreateCrystal ()
		{
			Crystall_ELIB crystall = new Crystall_ELIB()
			{
				countOfFreeMagistrals = 10,
				countOfMagistrals = 10,
				Lenght = 30,
				Scheme = new Scheme()
				{
					Connections = new List<Connection> { },
					Components = new List<Component>
				{
					new Component { ComponentId = 1, Name = "cpu1", Position = new Position { X = 1, Y = 1 }, ConnectionComponentId = 7 },
					new Component { ComponentId = 2, Name = "cpu4", Position = new Position { X = 2, Y = -1 }, ConnectionComponentId = 10 },
					new Component { ComponentId = 3, Name = "cpu3", Position = new Position { X = 3, Y = 1 }, ConnectionComponentId = 8 },
					new Component { ComponentId = 4, Name = "cpu2", Position = new Position { X = 4, Y = -1 }, ConnectionComponentId = 6 },
					new Component { ComponentId = 5, Name = "cpu5", Position = new Position { X = 14, Y = 1 }, ConnectionComponentId = 9 },
					new Component { ComponentId = 6, Name = "cpu2", Position = new Position { X = 6, Y = 1 }, ConnectionComponentId = 4 },
					new Component { ComponentId = 7, Name = "cpu1", Position = new Position { X = 11, Y = -1 }, ConnectionComponentId = 1 },
					new Component { ComponentId = 8, Name = "cpu3", Position = new Position { X = 11, Y = 1 }, ConnectionComponentId = 3 },
					new Component { ComponentId = 9, Name = "cpu5", Position = new Position { X = 17, Y = -1 }, ConnectionComponentId = 5 },
					new Component { ComponentId = 10, Name = "cpu4", Position = new Position { X = 18, Y = 1 }, ConnectionComponentId = 2 },
					new Component { ComponentId = 11, Name = "cpu6", Position = new Position { X = 15, Y = -1 }, ConnectionComponentId = 12 },
					new Component { ComponentId = 12, Name = "cpu6", Position = new Position { X = 8, Y = 1 }, ConnectionComponentId = 11 },
				},
				},
				Magistrals =
				{
					new Magistral(1, 30),
					new Magistral(2, 30),
					new Magistral(3, 30),
					new Magistral(4, 30),
					new Magistral(5, 30),
					new Magistral(6, 30),
					new Magistral(7, 30),
					new Magistral(8, 30),
					new Magistral(9, 30),
					new Magistral(10, 30),
					//new Magistral(11, 19),
					//new Magistral(12, 19),
					//new Magistral(13, 19),
					//new Magistral(14, 19),
					//new Magistral(15, 19),
					//new Magistral(16, 19),
					//new Magistral(17, 19),
					//new Magistral(18, 19),
					//new Magistral(19, 19),
					//new Magistral(20, 19),
				},

			};

			return crystall;
		}
		

	}
}
