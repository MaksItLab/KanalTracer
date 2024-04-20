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
				countOfFreeMagistrals = 5,
				countOfMagistrals = 5,
				Lenght = 19,
				Scheme = new Scheme()
				{
					Connections = new List<Connection> { },
					Components = new List<Component>
				{
					new Component { ComponentId = 1, Name = "cpu1t", Position = new Position { X = 1, Y = 1 }, ConnectionComponentId = 7 },
					new Component { ComponentId = 2, Name = "cpu4b", Position = new Position { X = 2, Y = -1 }, ConnectionComponentId = 10 },
					new Component { ComponentId = 3, Name = "cpu3t", Position = new Position { X = 3, Y = 1 }, ConnectionComponentId = 8 },
					new Component { ComponentId = 4, Name = "cpu2b", Position = new Position { X = 4, Y = -1 }, ConnectionComponentId = 6 },
					new Component { ComponentId = 5, Name = "cpu5t", Position = new Position { X = 14, Y = 1 }, ConnectionComponentId = 9 },
					new Component { ComponentId = 6, Name = "cpu2t", Position = new Position { X = 6, Y = 1 }, ConnectionComponentId = 4 },
					new Component { ComponentId = 7, Name = "cpu1b", Position = new Position { X = 11, Y = -1 }, ConnectionComponentId = 1 },
					new Component { ComponentId = 8, Name = "cpu3t", Position = new Position { X = 11, Y = 1 }, ConnectionComponentId = 3 },
					new Component { ComponentId = 9, Name = "cpu5b", Position = new Position { X = 17, Y = -1 }, ConnectionComponentId = 5 },
					new Component { ComponentId = 10, Name = "cpu4t", Position = new Position { X = 18, Y = 1 }, ConnectionComponentId = 2 },
				},
				},
				Magistrals =
				{
					new Magistral(1, 19),
					new Magistral(2, 19),
					new Magistral(3, 19),
					new Magistral(4, 19),
					new Magistral(5, 19),

				},

			};

			return crystall;
		}
		

	}
}
