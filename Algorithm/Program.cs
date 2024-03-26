using System;
using System.Collections.Generic;
using System.Linq;


namespace Algorithm
{
	public class Program
	{
		static void Main(string[] args)
		{
			// Задаем параметры генетического алгоритма
			int populationSize = 1000;
			double mutationRate = 0.1;
			int generations = 10000;
			int genomeLength = 500; // Длина генома
			Func<double[], double> fitnessFunction = (genome) =>
			{
				// Пример функции приспособленности: сумма генов
				double sum = 0;
				foreach (var gene in genome)
				{
					sum += gene;
				}
				return sum;
			};

			// Создаем экземпляр генетического алгоритма
			GeneticAlgorithm ga = new GeneticAlgorithm(populationSize, mutationRate, generations, genomeLength, fitnessFunction);

			// Запускаем генетический алгоритм
			double[] result = ga.Run();

			// Выводим результат
			Console.WriteLine("Best solution found:");
			foreach (var gene in result)
			{
				Console.Write(gene + " ");
			}
			Console.WriteLine();
			
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
		Func<double[], double> fitnessFunction;

		public GeneticAlgorithm(int populationSize, double mutationRate, int generations, int genomeLength, Func<double[], double> fitnessFunction)
		{
			this.populationSize = populationSize;
			this.mutationRate = mutationRate;
			this.generations = generations;
			this.genomeLength = genomeLength;
			this.fitnessFunction = fitnessFunction;
		}

		// Генерация начальной популяции
		private List<double[]> GenerateInitialPopulation()
		{
			List<double[]> population = new List<double[]>();

			for (int i = 0; i < populationSize; i++)
			{
				double[] genome = new double[genomeLength];
				for (int j = 0; j < genomeLength; j++)
				{
					genome[j] = random.NextDouble();
				}
				population.Add(genome);
			}

			return population;
		}

		// Оценка приспособленности
		private double[] EvaluatePopulation(List<double[]> population)
		{
			double[] fitnessValues = new double[populationSize];
			for (int i = 0; i < populationSize; i++)
			{
				fitnessValues[i] = fitnessFunction(population[i]);
			}
			return fitnessValues;
		}

		// Скрещивание
		private double[] Crossover(double[] parent1, double[] parent2)
		{
			int crossoverPoint = random.Next(0, genomeLength);
			double[] child = new double[genomeLength];

			for (int i = 0; i < crossoverPoint; i++)
			{
				child[i] = parent1[i];
			}

			for (int i = crossoverPoint; i < genomeLength; i++)
			{
				child[i] = parent2[i];
			}

			return child;
		}

		// Мутация
		private void Mutate(ref double[] genome)
		{
			for (int i = 0; i < genomeLength; i++)
			{
				if (random.NextDouble() < mutationRate)
				{
					genome[i] += (random.NextDouble() * 2 - 1) * 0.1; // Мутация на случайную величину от -0.1 до 0.1
					genome[i] = Math.Min(Math.Max(genome[i], 0), 1); // Гарантируем, что значения генов находятся в диапазоне [0, 1]
				}
			}
		}
		// Генетический алгоритм
		public double[] Run()
		{
			List<double[]> population = GenerateInitialPopulation();

			for (int generation = 0; generation < generations; generation++)
			{
				double[] fitnessValues = EvaluatePopulation(population);
				double maxFitness = fitnessValues.Max();
				int maxIndex = Array.IndexOf(fitnessValues, maxFitness);

				Console.WriteLine($"Generation {generation}: Max Fitness = {maxFitness}");

				if (maxFitness == double.PositiveInfinity)
				{
					return population[maxIndex];
				}

				List<double[]> newPopulation = new List<double[]>();

				for (int i = 0; i < populationSize; i += 2)
				{
					int parentIndex1 = RouletteWheelSelection(fitnessValues);
					int parentIndex2 = RouletteWheelSelection(fitnessValues);

					double[] child1 = Crossover(population[parentIndex1], population[parentIndex2]);
					double[] child2 = Crossover(population[parentIndex2], population[parentIndex1]);

					Mutate(ref child1);
					Mutate(ref child2);

					newPopulation.Add(child1);
					newPopulation.Add(child2);
				 }

				population = newPopulation;
			}

			return population[Array.IndexOf(EvaluatePopulation(population), EvaluatePopulation(population).Max())];
		}

		// Рулеточное отбор
		private int RouletteWheelSelection(double[] fitnessValues)
		{
			double totalFitness = fitnessValues.Sum();
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
