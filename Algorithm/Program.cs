using KanalTracer;
using KanalTracer.Infrastructure;
using KanalTracer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using WindowsFormsApp1.Infrastructure;


namespace Algorithm
{
	public class Program
	{
		
		static void Main(string[] args)
		{
			
			// Размещаем в разброс соединения
			// Задаем параметры генетического алгоритма
			int populationSize = 15;
			double mutationRate = 0.1;
			int generations = 100;
			int genomeLength = 6; // Длина генома
			Func<Crystall_ELIB, int> fitnessFunction = (crystal) =>
			{
				// Пример функции приспособленности: сумма генов
				int maxMagistrals = 0;
				maxMagistrals = crystal.countOfFreeMagistrals;
				return maxMagistrals;
			};

			// Создаем экземпляр генетического алгоритма
			GeneticAlgorithm ga = new GeneticAlgorithm(populationSize, mutationRate, generations, genomeLength, fitnessFunction);

			// Запускаем генетический алгоритм
			Crystall_ELIB result = ga.Run();

			// Выводим результат
			Console.WriteLine("Best solution found:");
			
			Console.WriteLine(result.countOfFreeMagistrals);

		}


		

		
	}


	public class GeneticAlgorithm
	{
		// Генератор случайных чисел
		private Random random = new Random();

		// Размер популяции
		private int populationSize;

		// Вероятность мутации
		private double mutationRate;

		// Количество поколений
		private int generations;

		// Длина генома (количество генов)
		private int genomeLength;

		// Функция приспособленности
		Func<Crystall_ELIB, int> fitnessFunction;

		public GeneticAlgorithm(int populationSize, double mutationRate, int generations, int genomeLength, Func<Crystall_ELIB, int> fitnessFunction)
		{
			this.populationSize = populationSize;
			this.mutationRate = mutationRate;
			this.generations = generations;
			this.genomeLength = genomeLength;
			this.fitnessFunction = fitnessFunction;
		}

		static void Print(Crystall_ELIB crys)
		{
			foreach (var item in crys.Magistrals)
			{
				for (int i = 0; i < item.ELInMagistral.Length; i++)
				{
					if (item.ELInMagistral[i] != 0)
					{
						Console.ForegroundColor = ConsoleColor.Green;
						Console.Write($"{item.ELInMagistral[i],2} ");
						Console.ResetColor();
					}
					else Console.Write($"{item.ELInMagistral[i],2} ");

				}
				Console.WriteLine();
			}
		}

		public static int ChooseMagistral(Component compFirst, Component compSecond, Crystall_ELIB crystal)
		{
			Random rnd = new Random();
			bool success = false;
			int idMag = 0;
			while (!success)
			{
				Thread.Sleep(5);
				idMag = rnd.Next(crystal.countOfMagistrals) + 1;
				for (int i = 0; i < crystal.countOfMagistrals; i++)
				{
					if (crystal.Magistrals[i].ID == idMag)
					{
						if (compFirst.Position.X > compSecond.Position.X && CheckMagistral(crystal.Magistrals[i], compSecond.Position.X, compFirst.Position.X))
						{
							success = true;
							break;
						}
						else if (CheckMagistral(crystal.Magistrals[i], compFirst.Position.X, compSecond.Position.X))
						{
							success = true;
							break;
						}
					}
				}
			}

			return idMag;
		}

		static void PlaceConnection(Component compStart, Component compEnd, Crystall_ELIB crystal)
		{
			bool successConnecting = false;
			Magistral currentMagistral = null;

			int idMag = ChooseMagistral(compStart, compEnd, crystal);

			for (int i = 0; i < crystal.countOfMagistrals; i++)
			{
				if (crystal.Magistrals[i].ID == idMag)
				{
					currentMagistral = crystal.Magistrals[i];
				}
			}

			while (!successConnecting)
			{
				
				if (crystal.Magistrals[idMag - 1].ELInMagistral.Sum() == 0)
				{
					crystal.countOfFreeMagistrals -= 1;
				}


				successConnecting = true;
				crystal.Magistrals[idMag - 1] = currentMagistral;

				Connection newConn = new Connection(compStart, compEnd);
				newConn.MagisralId = idMag;

				if (compStart.Position.X < compEnd.Position.X)
				{
					for (int i = compStart.Position.X - 1; i < compEnd.Position.X; i++)
					{
						currentMagistral.ELInMagistral[i] = compStart.ComponentId;
					}
					currentMagistral.Path[newConn] = new int[2] { compStart.Position.X, compEnd.Position.X };
				}
				else if (compStart.Position.X > compEnd.Position.X)
				{
					for (int i = compEnd.Position.X - 1; i > compStart.Position.X; i++)
					{
						currentMagistral.ELInMagistral[i] = compStart.ComponentId;
					}
					currentMagistral.Path[newConn] = new int[2] { compEnd.Position.X, compStart.Position.X };
				}
				
				crystal.Scheme.Connections.Add(newConn);
				crystal.Magistrals[idMag - 1].Connections.Add(newConn);
				
			}
		}

		public static bool CheckMagistral(Magistral magistral, int firstPoint, int secondPoint)
		{
			int sum = 0;
			for (int i = firstPoint; i <= secondPoint; i++)
			{
				if (magistral.ELInMagistral[i] != 0) sum++;
			}

			if (sum == 0) return true;
			else return false;
		}

		/// <summary>
		/// Возвращает компоненты с уникальными номерами, отсортированными по увеличению координаты X
		/// </summary>
		/// <param name="components"></param>
		/// <returns></returns>
		public List<Component> GetDistinctComponents (List<Component> components)
		{
			List<Component> comps = new List<Component>();
			
			List<string> allNames = components.Select(p => p.Name).ToList();
			List<string> names = new List<string>();

			foreach (var name in allNames)
			{
				if (!names.Contains(name))
				{
					names.Add(name);
				}
			}

			foreach (var item in components)
			{
				if(names.Contains(item.Name))
				{
					comps.Add(item);
					names.Remove(item.Name);
				}
			}


			return comps;
		}

		/// <summary>
		/// Соединяет компоненты на кристалле СБИС
		/// </summary>
		/// <param name="crystal"></param>
		/// <returns></returns>
		Crystall_ELIB Connecting(Crystall_ELIB crystal)
		{
			List<Component> allComponent = crystal.Scheme.Components.OrderBy(p => p.Position.X).ToList();
			
			List<Component> components = GetDistinctComponents(allComponent);
			

			//Component comp1 = new Component();
			//Component comp2 = new Component();

			foreach (var component in components)
			{
				Component compConnection = allComponent.FirstOrDefault(c => c.ComponentId == component.ConnectionComponentId);
				PlaceConnection(component, compConnection, crystal);

				component.IsConnected = true;
				compConnection.IsConnected = true;
				//Print(crystal);
                //Console.WriteLine("-----------------------------------------------------------");
            }

			
			//Console.WriteLine("Вывод результатов");
			//Print(crystal);
           
            return crystal;
		}

		// Генерация начальной популяции
		private List<Crystall_ELIB> GenerateInitialPopulation()
		{
			List<Crystall_ELIB> population = new List<Crystall_ELIB>();

			for (int i = 0; i < populationSize; i++)
			{
				Crystall_ELIB solution = Data.CreateCrystal();
				solution = Connecting(solution);
				population.Add(solution);
			}

			return population;
		}

		// Оценка приспособленности
		private int[] EvaluatePopulation(List<Crystall_ELIB> population)
		{
			int[] fitnessValues = new int[populationSize];

			for (int i = 0; i < populationSize; i++)
			{
				fitnessValues[i] = fitnessFunction(population[i]);
			}
			return fitnessValues;
		}

		// Скрещивание
		private Crystall_ELIB Crossover(Crystall_ELIB parent1, Crystall_ELIB parent2)
		{
			int crossoverPoint = random.Next(0, parent1.Scheme.Components.Count/2);
			Crystall_ELIB child = Data.CreateCrystal();
			HashSet<Connection> hashDistinctConnections = new HashSet<Connection>();


			//сортировка родителей по магистралям
			parent1 = SortMagistrals(parent1);
			parent2 = SortMagistrals(parent2);
			Console.WriteLine("Отсортированный родитель 1: ");
			Print(parent1);
			Console.WriteLine("Отсортированный родитель 2: ");
			Print(parent2);


			//Процесс скрещивания, данные берутся от первого родителя
			for (int i = 0; i < crossoverPoint; i++)
			{
				child.Scheme.Connections.Add(new Connection(parent1.Scheme.Connections[i].startComponent, parent1.Scheme.Connections[i].endComponent) { });
				child.Scheme.Connections[i].MagisralId = parent1.Scheme.Connections[i].MagisralId;
				hashDistinctConnections.Add(child.Scheme.Connections[i]);
			}

			child = FillElInMagistralFromParentFirst(ref child, parent1, crossoverPoint);


			//Процесс скрещивания, данные берутся от второго родителя
			for (int i = crossoverPoint; i < genomeLength; i++)
			{
				if (!hashDistinctConnections.Contains(parent2.Scheme.Connections[i]))
				{
					
					child.Scheme.Connections.Add(new Connection(parent2.Scheme.Connections[i].startComponent, parent2.Scheme.Connections[i].endComponent) { });
					child.Scheme.Connections[i].MagisralId = parent2.Scheme.Connections[i].MagisralId;
					hashDistinctConnections.Add(child.Scheme.Connections[i]);
				}
			}

			child = FillElInMagistralFromParentSecond(ref child, parent2, crossoverPoint);


			Console.WriteLine("Ребенок: ");
            Print(child);
			return child;
		}

		public Crystall_ELIB SortMagistrals(Crystall_ELIB crystal)
		{
			Crystall_ELIB crystalSorted = Data.CreateCrystal();
			List<Connection> connections = crystal.Scheme.Connections.Select(m => m).OrderBy(m => m.startComponent.Position.X).ToList();
			foreach (var item in connections)
			{
				crystalSorted.Scheme.Connections.Add(new Connection(item.startComponent, item.endComponent) { MagisralId = item.MagisralId});
			}

			//crystalSorted.Scheme.Connections = connections; // присваивается ссылка

			for (int i = 0; i < connections.Count; i++)
			{
				var query = crystal.Scheme.Connections.Where(m => m.MagisralId == connections[i].MagisralId).ToList();
				if (query.Count > 1 && query.IndexOf(connections[i]) != 0) 
				{
					var conId = crystalSorted.Scheme.Connections.FirstOrDefault(con => con.startComponent == query[0].startComponent);
					crystalSorted.Scheme.Connections[i].MagisralId = conId.MagisralId;

					Component compStart = connections[i].startComponent;
					Component compEnd = connections[i].endComponent;

					if (compStart.Position.X < compEnd.Position.X)
					{
						for (int j = compStart.Position.X - 1; j < compEnd.Position.X; j++)
						{
							crystalSorted.Magistrals[conId.MagisralId-1].ELInMagistral[j] = compStart.ComponentId;
						}
						crystalSorted.Magistrals[conId.MagisralId-1].Path[crystalSorted.Scheme.Connections[i]] = new int[2] { compStart.Position.X, compEnd.Position.X };
					}
					else if (compStart.Position.X > compEnd.Position.X)
					{
						for (int j = compEnd.Position.X - 1; j > compStart.Position.X; j++)
						{
							crystalSorted.Magistrals[query[0].MagisralId-1].ELInMagistral[j] = compStart.ComponentId;
						}
						crystalSorted.Magistrals[query[0].MagisralId-1].Path[crystalSorted.Scheme.Connections[i]] = new int[2] { compEnd.Position.X, compStart.Position.X };
					}
					crystalSorted.Magistrals[query[0].MagisralId-1].Connections.Add(connections[i]);
                    Console.WriteLine($"----------------------------Итерация {i}----------------------------");
                    Print(crystalSorted);
				}
				else
				{
					crystalSorted.Scheme.Connections[i].MagisralId = i + 1;

					Component compStart = connections[i].startComponent;
					Component compEnd = connections[i].endComponent;

					if (compStart.Position.X < compEnd.Position.X)
					{
						for (int j = compStart.Position.X - 1; j < compEnd.Position.X; j++)
						{
							crystalSorted.Magistrals[i].ELInMagistral[j] = compStart.ComponentId;
						}
						crystalSorted.Magistrals[i].Path[crystalSorted.Scheme.Connections[i]] = new int[2] { compStart.Position.X, compEnd.Position.X };
					}
					else if (compStart.Position.X > compEnd.Position.X)
					{
						for (int j = compEnd.Position.X - 1; j > compStart.Position.X; j++)
						{
							crystalSorted.Magistrals[i].ELInMagistral[j] = compStart.ComponentId;
						}
						crystalSorted.Magistrals[i].Path[crystalSorted.Scheme.Connections[i]] = new int[2] { compEnd.Position.X, compStart.Position.X };
					}
					crystalSorted.Magistrals[i].Connections.Add(connections[i]);

					Console.WriteLine($"----------------------------Итерация {i}----------------------------");
					Print(crystalSorted);
				}

			}

			return crystalSorted;
		}

		public Crystall_ELIB FillElInMagistralFromParentSecond(ref Crystall_ELIB crys, Crystall_ELIB parent, int count)
		{

			List<int> idMags = crys.Scheme.Connections.Select(m => m.MagisralId).ToList(); // LINQ запрос на вытягивание всех id магистралей

			//List<Magistral> magistrals = parent.Magistrals.Where(m => idMags.Contains(m.ID)).ToList(); // LINQ запрос на вытягивание магистралей, в которые были добавлены соединения

			for (int i = count; i < idMags.Count; i++)
			{
				var magistral = parent.Magistrals.FirstOrDefault(m => m.ID == idMags[i]);

				var conn = parent.Scheme.Connections.FirstOrDefault(id => id.MagisralId == magistral.ID);

				int[] arrayStartEndPoints = magistral.Path[conn];
				int start = arrayStartEndPoints[0];
				int end = arrayStartEndPoints[1];



				for (int j = start - 1; j < end; j++)
				{
					crys.Magistrals[parent.Scheme.Connections[i].MagisralId - 1].ELInMagistral[j] = conn.startComponent.ComponentId;
				}



				//Array.Copy(parent.Magistrals[parent.Scheme.Connections[i].MagisralId - 1].ELInMagistral, crys.Magistrals[crys.Scheme.Connections[i].MagisralId - 1].ELInMagistral, crys.Lenght);
				
				Console.WriteLine($"Ребенок, итерация: {i}: ");
				Print(crys);
			}
			Console.WriteLine("Ребенок : ");
			Print(crys);

			return crys;
		}


		/// <summary>
		/// Заполняет магистрали кристалла от первого родителя
		/// </summary>
		/// <param name="crys"></param>
		/// <param name="parent"></param>
		/// <param name="count"></param>
		/// <returns></returns>
		public Crystall_ELIB FillElInMagistralFromParentFirst(ref Crystall_ELIB crys, Crystall_ELIB parent, int count)
		{
			
			List<int> idMags = crys.Scheme.Connections.Select(m => m.MagisralId).ToList(); // LINQ запрос на вытягивание id магистралей, в которые были добавлены соединения

			//List<Magistral> magistrals = parent.Magistrals.Where(m => idMags.Contains(m.ID)).ToList(); // LINQ запрос на вытягивание магистралей, в которые были добавлены соединения

			for (int i = 0; i < count; i++)
			{
				var magistral = parent.Magistrals.FirstOrDefault(m => m.ID == idMags[i]);

				var conn = parent.Scheme.Connections.FirstOrDefault(id => id.MagisralId == magistral.ID);

				int[] arrayStartEndPoints = magistral.Path[conn];
				int start = arrayStartEndPoints[0];
				int end = arrayStartEndPoints[1];



				for (int j = start-1; j < end; j++)
				{
					crys.Magistrals[parent.Scheme.Connections[i].MagisralId - 1].ELInMagistral[j] = conn.startComponent.ComponentId;
				}



				//Array.Copy(parent.Magistrals[parent.Scheme.Connections[i].MagisralId - 1].ELInMagistral, crys.Magistrals[crys.Scheme.Connections[i].MagisralId - 1].ELInMagistral, crys.Lenght);
				
				//Console.WriteLine($"Ребенок, итерация: {i}: ");
				//Print(crys);
			}
            Console.WriteLine("Ребенок : ");
            Print(crys);
			
			return crys;
		}

		/// <summary>
		/// Мутация
		/// </summary>
		/// <param name="genome"></param>
		private void Mutate(ref Crystall_ELIB genome)
		{
			//процесс мутации


			//for (int i = 0; i < genomeLength; i++)
			//{
			//	if (random.NextDouble() < mutationRate)
			//	{
			//		genome[i] += (random.NextDouble() * 2 - 1) * 0.1; // Мутация на случайную величину от -0.1 до 0.1
			//		genome[i] = Math.Min(Math.Max(genome[i], 0), 1); // Гарантируем, что значения генов находятся в диапазоне [0, 1]
			//	}
			//}
		}


		/// <summary>
		/// Генетический алгоритм
		/// </summary>
		/// <returns></returns>
		public Crystall_ELIB Run()
		{
			List<Crystall_ELIB> population = GenerateInitialPopulation();

			for (int generation = 0; generation < generations; generation++)
			{
				int[] fitnessValues = EvaluatePopulation(population);
				int bestSolution = fitnessValues.Max();
				int minIndex = Array.IndexOf(fitnessValues, bestSolution);

				Console.WriteLine($"Generation {generation}: Best solution = {bestSolution}");
				Print(population[minIndex]);
				

				List<Crystall_ELIB> newPopulation = new List<Crystall_ELIB>();

				for (int i = 0; i < populationSize; i += 2)
				{
					int parentIndex1 = RouletteWheelSelection(fitnessValues);
					int parentIndex2 = RouletteWheelSelection(fitnessValues);

                    Console.WriteLine("Parent 1:");
					Print(population[parentIndex1]);
					Console.WriteLine("Parent 2:");
					Print(population[parentIndex2]);

                    Crystall_ELIB child1 = Crossover(population[parentIndex1], population[parentIndex2]);
					Crystall_ELIB child2 = Crossover(population[parentIndex2], population[parentIndex1]);

					Mutate(ref child1);
					Mutate(ref child2);

					newPopulation.Add(child1);
					newPopulation.Add(child2);
				 }

				population = newPopulation;
			}

			return population[Array.IndexOf(EvaluatePopulation(population), EvaluatePopulation(population).Min())];
		}

		/// <summary>
		/// Рулеточный отбор	
		/// </summary>
		/// <param name="fitnessValues"></param>
		/// <returns></returns>
		private int RouletteWheelSelection(int[] fitnessValues)
		{
			int totalFitness = fitnessValues.Sum();
			double randomValue = random.NextDouble() * totalFitness;
			double sum = 0;

			for (int i = 0; i < fitnessValues.Length; i++)
			{
				sum += fitnessValues[i];
				if (sum >= randomValue)
				{
					return i;
				}
			}

			return fitnessValues.Length - 1;
		}
	}
}
