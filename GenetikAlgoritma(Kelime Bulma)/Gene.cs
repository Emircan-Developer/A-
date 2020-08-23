using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GenetikAlgoritma_Kelime_Bulma_
{

    class Gene
    {
        string[] wordList = new string[] { "a", "b", "c", "d", "e" };


        List<int> Fitness;
        Random rnd;

        List<Populate> nextPopulate;
        List<Populate> population;
        private int populationSize;
        string[] Aim = new string[] { "a", "b", "c" ,"e", "a", "b"};
        public Gene(int populationSize)
        {
            nextPopulate = new List<Populate>();
            population = new List<Populate>();
            Fitness = new List<int>();
            rnd = new Random();
            this.populationSize = populationSize;
            Init();
        }

        private void Init()
        {
            for (int i = 0; i < populationSize; i++)
            {
                population.Add(new Populate());
                population[i].Word = new string[Aim.Length];
            }
            for (int i = 0; i < population.Count; i++)
            {

                for (int j = 0; j < population[i].Word.Length ; j++) 
                    population[i].Word[j] = wordList[rnd.Next(0, wordList.Length)];
                        
    
            }
            int x = 0;
            while (founded == false)
            {
                calculateFitness();

                Crossover();
                if(nextPopulate.Count == 0)
                {

                }
                else {
                population = nextPopulate;
                }
            }
        }
        bool founded = false;
        void Crossover()
        {
            int bests = (90 * population.Count) / 100;
            nextPopulate = null;
            nextPopulate = new List<Populate>();


            nextPopulate.Add(population[bests ]);
            while (true)
            {
                if(nextPopulate.Count < populationSize)
                {
                    string[] newMember1 =  population[rnd.Next(bests)].Word;
                    string[] newMember2 =  population[rnd.Next(bests)].Word;
                    string[] crossingOveredMember = new string[newMember1.Length];
                    for(int i = 0; i < Aim.Length; i++)
                    {
                        int midPoint = rnd.Next(0, Aim.Length);
                        if(i > midPoint)
                        {
                            crossingOveredMember[i] = newMember2[i];
                        }
                        else if (i < midPoint){
                            crossingOveredMember[i] = newMember1[i];

                        }
                    }
                    Populate crossingOvered = new Populate();
                    crossingOvered.Word = crossingOveredMember;
                    nextPopulate.Add((crossingOvered));

                    Mutate(0.1);
                }

                else
                {
                    break;
                }
            }
        }
        void Mutate(double mutateRate)
        {
            
                foreach(Populate populate in nextPopulate)
                {
                if (rnd.NextDouble() < mutateRate)
                {
                    for (int i = 0; i < Aim.Length; i++)
                        populate.Word[i] = wordList[rnd.Next(wordList.Length)];
                }
        }
        }
        void calculateFitness()
        {
            for (int j = 0; j < population.Count; j++)
            {
                int rate = 0;
                for (int i = 0; i < population[j].Word.Length; i++)
                {
                    if (population[j].Word[i] == Aim[i])
                    {
                        rate++;
                    }
                }
                population[j].fitness = rate;
                Fitness.Add(population[j].fitness);
                if (population[j].fitness == Aim.Length)
                {
                    for(int i = 0;i < population[j].Word.Length;i++)
                        MessageBox.Show(population[j].Word[i]);
                    founded = true;
                    break;

                }

            }
            ShortAndAppend();
        }
        void ShortAndAppend()
        {
            for(int i = 1; i< population.Count; i++)
            {
                if (population[i-1].fitness > population[i].fitness)
                {
                    Populate temp = population[i];
                    population[i] = population[i - 1];
                    population[i - 1] = temp;
                }
            }
        }
    }
}
