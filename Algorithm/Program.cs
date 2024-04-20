﻿using KanalTracer;
using KanalTracer.Infrastructure;
using KanalTracer.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using WindowsFormsApp1.Infrastructure;


namespace Algorithm
{
	public class Program
	{
		//static Crystall_ELIB crystal = new Crystall_ELIB();
		
		static void Main(string[] args)
		{
			// Задаем параметры кристалла
			//crystal.countOfMagistrals = 4;
			//crystal.Lenght = 19;
			//crystal.Scheme = new Scheme();
			//crystal.Scheme.Connections = new List<Connection> {};

			//crystal.Scheme.Components = new List<Component>
			//{
			//	new Component {ComponentId = 1, Name="cpu1t", Position = new Position {X = 1, Y = 1 }, ConnectionComponentId  = 7},
			//	new Component {ComponentId = 2, Name="cpu4b", Position = new Position {X = 2, Y = -1 }, ConnectionComponentId  = 10},
			//	new Component {ComponentId = 3, Name="cpu3t", Position = new Position {X = 3, Y = 1 }, ConnectionComponentId  = 8},
			//	new Component {ComponentId = 4, Name="cpu2b", Position = new Position {X = 4, Y = -1 }, ConnectionComponentId  = 6},
			//	new Component {ComponentId = 5, Name="cpu5t", Position = new Position {X = 14, Y = 1 }, ConnectionComponentId  = 9},
			//	new Component {ComponentId = 6, Name="cpu2t", Position = new Position {X = 6, Y = 1 }, ConnectionComponentId  = 4},
			//	new Component {ComponentId = 7, Name="cpu1b", Position = new Position {X = 11, Y = -1 }, ConnectionComponentId  = 1},
			//	new Component {ComponentId = 8, Name="cpu3t", Position = new Position {X = 11, Y = 1 }, ConnectionComponentId  = 3},
			//	new Component {ComponentId = 9, Name="cpu5b", Position = new Position {X = 17, Y = -1 }, ConnectionComponentId  = 5},
			//	new Component {ComponentId = 10, Name="cpu4t", Position = new Position {X = 18, Y = 1 }, ConnectionComponentId  = 2},
			//};

			//for (int i = 0; i < crystal.countOfMagistrals; i++)
			//{
			//	crystal.Magistrals.Add(new Magistral(i+1, crystal.Lenght));
			//}



			// Размещаем в разброс соединения
			// Задаем параметры генетического алгоритма
			int populationSize = 1000;
			double mutationRate = 0.1;
			int generations = 100;
			int genomeLength = 5; // Длина генома
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
					Console.Write(item.ELInMagistral[i] + " ");
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

				if (compStart.Position.X < compEnd.Position.X)
				{
					for (int i = compStart.Position.X - 1; i < compEnd.Position.X; i++)
					{
						currentMagistral.ELInMagistral[i] = compStart.ComponentId;
					}
					Connection.Path[crystal.Magistrals[idMag - 1]] = new int[2] { compStart.Position.X, compEnd.Position.X };
				}
				else if (compStart.Position.X > compEnd.Position.X)
				{
					for (int i = compEnd.Position.X - 1; i > compStart.Position.X; i++)
					{
						currentMagistral.ELInMagistral[i] = compStart.ComponentId;
					}
					Connection.Path[crystal.Magistrals[idMag - 1]] = new int[2] { compEnd.Position.X, compStart.Position.X };
				}
				successConnecting = true;
				crystal.Magistrals[idMag - 1] = currentMagistral;
				crystal.Scheme.Connections.Add(new Connection(compStart, compEnd));

				


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

		//соединение
		Crystall_ELIB Connecting(Crystall_ELIB crystal)
		{
			Component comp1 = new Component();
			Component comp2 = new Component();
			// кол-во компонентов/2 = кол-во соединений
			for (int i = 0; i < crystal.Scheme.Components.Count / 2; i++)
			{
				//выбрали 1-ый компонент
				for (int j = 0; j < crystal.Scheme.Components.Count; j++)
				{
					comp1 = crystal.Scheme.Components[j];
					// выполняется, если компонент не соединен
					if (!comp1.IsConnected)
					{
						for (int m = 0; m < crystal.Scheme.Components.Count; m++)
						{
							if (crystal.Scheme.Components[m].ComponentId == comp1.ConnectionComponentId)
							{
								comp2 = crystal.Scheme.Components[m];
								break;
							}
						}

						PlaceConnection(comp1, comp2, crystal);

						
						
						comp1.IsConnected = true;
						comp2.IsConnected = true;
						break;
					}


				}
			}
			Console.WriteLine("Вывод результатов");
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
			int crossoverPoint = random.Next(0, genomeLength);
			Crystall_ELIB child = new Crystall_ELIB();

			//Процесс скрещивания


			//for (int i = 0; i < crossoverPoint; i++)
			//{
			//	child[i] = parent1[i];
			//}

			//for (int i = crossoverPoint; i < genomeLength; i++)
			//{
			//	child[i] = parent2[i];
			//}

			return child;
		}

		// Мутация
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
		// Генетический алгоритм
		public Crystall_ELIB Run()
		{
			List<Crystall_ELIB> population = GenerateInitialPopulation();

			for (int generation = 0; generation < generations; generation++)
			{
				int[] fitnessValues = EvaluatePopulation(population);
				int bestSolution = fitnessValues.Max();
				int minIndex = Array.IndexOf(fitnessValues, bestSolution);

				Console.WriteLine($"Generation {generation}: Best solution = {bestSolution}");

				

				List<Crystall_ELIB> newPopulation = new List<Crystall_ELIB>();

				for (int i = 0; i < populationSize; i += 2)
				{
					int parentIndex1 = RouletteWheelSelection(fitnessValues);
					int parentIndex2 = RouletteWheelSelection(fitnessValues);

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

		// Рулеточный отбор
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
