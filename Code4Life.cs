// 13 place silver league

using System;
using System.Linq;
using System.IO;
using System.Text;
using System.Collections;
using System.Collections.Generic;



/**
 * Bring data on patient samples from the diagnosis machine to the laboratory with enough molecules to produce medicine!
 **/
class Player
{
    public class Project
    {
        public int CostA;
        public int CostB;
        public int CostC;
        public int CostD;
        public int CostE;

        public bool IsCostMet(int[] expertise)
        {
            return (CostA <= expertise[0] && CostB <= expertise[1] && CostC <= expertise[2] && CostD <= expertise[3] && CostE <= expertise[4]);
        }

        public int CanBeCompleted(Bot bot)
        {
            var samples = gameData.samples.Where(s => s.CarriedBy == 0);
            foreach (var sample in samples)
            {
                var expertise = bot.Expertise.ToArray();

                switch (sample.ExpGain)
                {
                    case "A": expertise[A]++; break;
                    case "B": expertise[B]++; break;
                    case "C": expertise[C]++; break;
                    case "D": expertise[D]++; break;
                    case "E": expertise[E]++; break;
                }

                var result = CostA <= expertise[0] && CostB <= expertise[B] && CostC <= expertise[C] && CostD <= expertise[D] && CostE <= expertise[E];

                if (result)
                {
                    Console.Error.WriteLine($"Project can be completed by sample {sample.SampleId}");
                    return sample.SampleId;
                }
            }
            return 0;
        }
    }
    public class Bot
    {
        public string CurrentLocation;
        public int[] Storage = new int[5];
        public int Eta;
        public int[] Expertise = new int[5];
    }
    public class GameData
    {
        public List<aSample> samples;
        public int[] Available = new int[5];

        public bool IsMoleculeAvailable(string molecule)
        {
            switch (molecule)
            {
                case "A":
                    return Available[0] > 0;
                case "B":
                    return Available[1] > 0;
                case "C":
                    return Available[2] > 0;
                case "D":
                    return Available[3] > 0;
                case "E":
                    return Available[4] > 0;
            }
            return false;
        }
    }




    static int A = 0;
    static int B = 1;
    static int C = 2;
    static int D = 3;
    static int E = 4;

    static Project[] projects;
    public static GameData gameData = new GameData();
    static void Main(string[] args)
    {
        Bot me = new Bot();
        Bot opponent = new Bot();

        string[] inputs;
        int projectCount = int.Parse(Console.ReadLine());
        projects = new Project[projectCount];
        for (int i = 0; i < projectCount; i++)
        {
            inputs = Console.ReadLine().Split(' ');

            var project = new Project()
            {
                CostA = int.Parse(inputs[0]),
                CostB = int.Parse(inputs[1]),
                CostC = int.Parse(inputs[2]),
                CostD = int.Parse(inputs[3]),
                CostE = int.Parse(inputs[4]),
            };

            projects[i] = project;
        }

        // game loop
        while (true)
        {

            for (int i = 0; i < 2; i++)
            {
                inputs = Console.ReadLine().Split(' ');
                string target = inputs[0];
                int teta = int.Parse(inputs[1]);
                int score = int.Parse(inputs[2]);
                int storageA = int.Parse(inputs[3]);
                int storageB = int.Parse(inputs[4]);
                int storageC = int.Parse(inputs[5]);
                int storageD = int.Parse(inputs[6]);
                int storageE = int.Parse(inputs[7]);
                int.Parse(inputs[8]);
                int.Parse(inputs[9]);
                int.Parse(inputs[10]);
                int.Parse(inputs[11]);
                int.Parse(inputs[12]);

                if (i == 0)
                {
                    me.CurrentLocation = target;
                    me.Storage[0] = storageA;
                    me.Storage[1] = storageB;
                    me.Storage[2] = storageC;
                    me.Storage[3] = storageD;
                    me.Storage[4] = storageE;
                    me.Eta = teta;

                    me.Expertise[A] = int.Parse(inputs[8]);
                    me.Expertise[B] = int.Parse(inputs[9]);
                    me.Expertise[C] = int.Parse(inputs[10]);
                    me.Expertise[D] = int.Parse(inputs[11]);
                    me.Expertise[E] = int.Parse(inputs[12]);
                }

                if (i == 1)
                {
                    opponent.CurrentLocation = target;
                    opponent.Storage[0] = storageA;
                    opponent.Storage[1] = storageB;
                    opponent.Storage[2] = storageC;
                    opponent.Storage[3] = storageD;
                    opponent.Storage[4] = storageE;
                    opponent.Eta = teta;

                    opponent.Expertise[A] = int.Parse(inputs[8]);
                    opponent.Expertise[B] = int.Parse(inputs[9]);
                    opponent.Expertise[C] = int.Parse(inputs[10]);
                    opponent.Expertise[D] = int.Parse(inputs[11]);
                    opponent.Expertise[E] = int.Parse(inputs[12]);
                }
            }
            inputs = Console.ReadLine().Split(' ');


            gameData.Available[A] = int.Parse(inputs[0]);
            gameData.Available[B] = int.Parse(inputs[1]);
            gameData.Available[C] = int.Parse(inputs[2]);
            gameData.Available[D] = int.Parse(inputs[3]);
            gameData.Available[E] = int.Parse(inputs[4]);

            int sampleCount = int.Parse(Console.ReadLine());

            gameData.samples = new List<aSample>(sampleCount);
            for (int i = 0; i < sampleCount; i++)
            {
                inputs = Console.ReadLine().Split(' ');

                var sample = new aSample
                {
                    SampleId = int.Parse(inputs[0]),
                    CarriedBy = int.Parse(inputs[1]),
                    Rank = int.Parse(inputs[2]),
                    ExpGain = inputs[3],
                    Health = int.Parse(inputs[4]),

                };
                sample.Cost[0] = int.Parse(inputs[5]);
                sample.Cost[1] = int.Parse(inputs[6]);
                sample.Cost[2] = int.Parse(inputs[7]);
                sample.Cost[3] = int.Parse(inputs[8]);
                sample.Cost[4] = int.Parse(inputs[9]);

                gameData.samples.Add(sample);
            }

            var mySamples = gameData.samples.Where(s => s.CarriedBy == 0).ToArray();




            if (me.CurrentLocation == "START_POS")
            {
                Console.WriteLine("GOTO SAMPLES ");
            }
            else if (me.Eta > 0)
            {
                Console.WriteLine("WAIT");
            }
            else if (me.CurrentLocation == "SAMPLES")
            {
                var loaded = mySamples.Count();
                if (me.Expertise.Sum() == 0 && loaded < 2)
                {
                    Console.WriteLine("CONNECT 1");
                }
                else if (me.Expertise.Sum() == 0 && loaded == 2)
                {
                    Console.WriteLine("GOTO DIAGNOSIS");
                }
                else if (me.Expertise.Sum() != 0 && loaded < 3)
                {
                    if (me.Expertise.Sum() < 5)
                    {
                        Console.WriteLine("CONNECT 1");
                    }
                    else if (me.Expertise.Sum() < 11)
                    {
                        Console.WriteLine("CONNECT 2");
                    }
                    else
                    {
                        Console.WriteLine("CONNECT 3");
                    }
                }
                else
                {
                    Console.WriteLine("GOTO DIAGNOSIS");
                }
            }
            else if (me.CurrentLocation == "DIAGNOSIS")
            {
                var undiagnosed = mySamples.Count(s => s.Health == -1);
                if (mySamples.Count() < 3 && gameData.samples.Any(s => s.CanCostBeCovered(me.Expertise, opponent) && s.CarriedBy == -1))
                {
                    // Get any already diagnosed samples.
                    var sample = gameData.samples.Where(s => s.CarriedBy == -1).OrderByDescending(s => s.Health).First(s => s.CanCostBeCovered(me.Expertise, opponent));
                    Console.WriteLine($"CONNECT {sample.SampleId}");
                }
                else if (undiagnosed > 0)
                {
                    // Diagnose sample
                    var sample = mySamples.First(s => s.Health == -1 && s.CanCostBeCovered(me.Expertise, opponent));
                    Console.WriteLine("CONNECT " + sample.SampleId);
                }
                else if (mySamples.Any(s => !s.CanCostBeCovered(me.Expertise, opponent)))
                {
                    // Dump samples we can't use (yet)
                    var sample = mySamples.First(s => !s.CanCostBeCovered(me.Expertise, opponent));
                    Console.Error.WriteLine($"Dumping sample " + sample.SampleId);
                    Console.WriteLine("CONNECT " + sample.SampleId);
                }
                else if (!mySamples.Any())
                {
                    Console.WriteLine("GOTO SAMPLES ");
                }
                else
                {
                    Console.WriteLine("GOTO MOLECULES");
                }
            }
            else if (me.CurrentLocation == "MOLECULES")
            {
                Console.Error.WriteLine($"Project 0 costs: ");

                Console.Error.WriteLine($"{projects[0].CostA}, {projects[0].CostB}, {projects[0].CostC}, {projects[0].CostD}, {projects[0].CostE}");
                Console.Error.WriteLine($"{me.Expertise[A]}, {me.Expertise[B]}, {me.Expertise[C]}, {me.Expertise[D]}, {me.Expertise[E]}");

                Console.Error.WriteLine($"Project 1 costs: ");
                Console.Error.WriteLine($"{projects[1].CostA}, {projects[1].CostB}, {projects[1].CostC}, {projects[1].CostD}, {projects[1].CostE}");
                Console.Error.WriteLine($"{me.Expertise[A]}, {me.Expertise[B]}, {me.Expertise[C]}, {me.Expertise[D]}, {me.Expertise[E]}");

                Console.Error.WriteLine($"Project 2 costs: ");
                Console.Error.WriteLine($"{projects[2].CostA}, {projects[2].CostB}, {projects[2].CostC}, {projects[2].CostD}, {projects[2].CostE}");
                Console.Error.WriteLine($"{me.Expertise[A]}, {me.Expertise[B]}, {me.Expertise[C]}, {me.Expertise[D]}, {me.Expertise[E]}");

                int sampleId = 0;
                var project = projects.FirstOrDefault(p => (sampleId = p.CanBeCompleted(me)) != 0 && sampleId != 0);
                if (project != null)
                {
                    mySamples.First(s => s.SampleId == sampleId).Health += 50;
                }

                if (mySamples.Any())
                    DumpSamples(mySamples);

                if (me.Storage.Sum() < 10 && mySamples.Where(s => !s.IsCostCovered(me.Storage, me.Expertise)).Any(s => s.CanCostBeCovered(me.Expertise, opponent)))
                {
                    string output = "GOTO SAMPLES";
                    Console.Error.WriteLine("Getting some molecules!");

                    var loaded = mySamples.OrderByDescending(s => s.Value(me));

                    var storage = me.Storage.ToArray();
                    var expertise = me.Expertise.ToArray();
                    foreach (var sample in loaded)
                    {
                        if (sample.IsCostCovered(storage, expertise))
                        {
                            Console.Error.WriteLine("CostCovered for: " + sample.SampleId);
                            for (int i = 0; i < 5; i++)
                            {
                                storage[i] = storage[i] - sample.Cost[i];
                                if (storage[i] < 0) { expertise[i] = expertise[i] + storage[i]; storage[i] = 0; }
                            }
                            continue;
                        }
                        Console.Error.WriteLine("Working on sample: " + sample.SampleId);

                        // Do we have all the molecules we need?
                        if (sample.Cost[A] > storage[0] + me.Expertise[A] && gameData.IsMoleculeAvailable("A"))
                        {
                            output = "CONNECT A";
                            break;
                        }
                        else if (sample.Cost[B] > storage[1] + me.Expertise[B] && gameData.IsMoleculeAvailable("B"))
                        {
                            output = "CONNECT B";
                            break;
                        }
                        else if (sample.Cost[C] > storage[2] + me.Expertise[C] && gameData.IsMoleculeAvailable("C"))
                        {
                            output = "CONNECT C";
                            break;
                        }
                        else if (sample.Cost[D] > storage[3] + me.Expertise[D] && gameData.IsMoleculeAvailable("D"))
                        {
                            output = "CONNECT D";
                            break;
                        }
                        else if (sample.Cost[E] > storage[4] + me.Expertise[E] && gameData.IsMoleculeAvailable("E"))
                        {
                            output = "CONNECT E";
                            break;
                        }
                    }

                    if (output == "GOTO SAMPLES" && gameData.samples.Any(s => s.IsCostCovered(me.Storage, me.Expertise)))
                    {
                        Console.WriteLine("GOTO LABORATORY");
                    }
                    else if (output == "GOTO SAMPLES" && mySamples.Count() == 3 && me.Storage.Sum() < 10 && mySamples.Any(s => s.CanCostBeCovered(me.Expertise, opponent)))
                    {
                        Console.WriteLine("WAIT");
                    }
                    else
                        Console.WriteLine(output);
                }
                else if (mySamples.Any(s => s.IsCostCovered(me.Storage, me.Expertise)))
                {
                    Console.WriteLine("GOTO LABORATORY");
                }

                else if (mySamples.Any(s => !s.CanCostBeCovered(me.Expertise, opponent)))
                {
                    Console.Error.WriteLine($"Can't meet cost");
                    Console.WriteLine("GOTO DIAGNOSIS");
                }
                else
                {
                    Console.Error.WriteLine("Default action!!!!!!!!");
                    Console.WriteLine("GOTO DIAGNOSIS");
                }
            }

            else if (me.CurrentLocation == "LABORATORY")
            {
                while (projects.Any(p => p.IsCostMet(me.Expertise)))
                {
                    Console.Error.WriteLine("SCIENCE !!!!");

                    // Remnove project
                    var p = projects.First(p => p.IsCostMet(me.Expertise));
                    p.CostA = 99;
                }
                if (mySamples.Count() == 0)
                {
                    Console.WriteLine("GOTO SAMPLES");
                }
                else
                if (mySamples.Any(s => s.IsCostCovered(me.Storage, me.Expertise)))
                {
                    var sample = mySamples.Where(s => s.IsCostCovered(me.Storage, me.Expertise)).OrderByDescending(s => s.Health).First();
                    Console.WriteLine("CONNECT " + sample.SampleId);
                }
                else
                {
                    if (me.Expertise.Sum() < 6 &&  mySamples.Any(s=> s.CanCostBeCovered(me.Expertise, opponent)))
                        Console.WriteLine("GOTO MOLECULES");
                    else
                    if (mySamples.Count() > 1 && mySamples.All(s=> s.CanCostBeCovered(me.Expertise, opponent)))
                        Console.WriteLine("GOTO MOLECULES");
                    else 
                    if (gameData.samples.Any(s => s.CarriedBy == -1 && s.CanCostBeCovered(me.Expertise, opponent) ))
                        Console.WriteLine("GOTO DIAGNOSIS");
                    else
                        Console.WriteLine("GOTO SAMPLES");
                }
            }
        }
    }

    private static void DumpSamples(IEnumerable<aSample> samples)
    {
        foreach (var sample in samples)
        {
            Console.Error.WriteLine($"id: {sample.SampleId}, CarriedBy: {sample.CarriedBy}, Health: {sample.Health}");
        }
    }

    public class aSample
    {
        public int SampleId;
        public int CarriedBy;
        public int Health;
        public int Rank;
        public string ExpGain;
        public int[] Cost = new int[5];

        public int TrueCost(Bot bot)
        {
            int costA = Math.Max(Cost[0] - bot.Expertise[A] - bot.Storage[A], 0);
            int costB = Math.Max(Cost[1] - bot.Expertise[B] - bot.Storage[B], 0);
            int costC = Math.Max(Cost[2] - bot.Expertise[C] - bot.Storage[C], 0);
            int costD = Math.Max(Cost[3] - bot.Expertise[D] - bot.Storage[D], 0);
            int costE = Math.Max(Cost[4] - bot.Expertise[E] - bot.Storage[E], 0);

            int cost = costA + costB + costC + costD + costE;

            Console.Error.WriteLine($"{SampleId} true cost: {cost}");
            return cost;
        }

        public double Value(Bot bot)
        {
            double value = 0.0;
            double cost = TrueCost(bot);
            if (cost == 0)
                value = Health * 2.0;
            else
                value = Health / cost;

            Console.Error.WriteLine($"{SampleId} value: {value}");

            return value;
        }

        public bool IsCostCovered(int[] storage, int[] expertise)
        {
            var costCovered = (Cost[0] <= storage[0] + expertise[A]
            && (Cost[1] <= storage[1] + expertise[B])
            && (Cost[2] <= storage[2] + expertise[C])
            && (Cost[3] <= storage[3] + expertise[D])
            && (Cost[E] <= storage[4] + expertise[E]));

            Console.Error.WriteLine($"{SampleId} Cost covered: {costCovered}");

            Console.Error.WriteLine($"{Cost[0]} ,{storage[0]} , {expertise[A]}");
            Console.Error.WriteLine($"{Cost[1]} , {storage[1]} , {expertise[B]}");
            Console.Error.WriteLine($"{Cost[2]} , {storage[2]} , {expertise[C]}");
            Console.Error.WriteLine($"{Cost[3]} , {storage[3]} , {expertise[D]}");
            Console.Error.WriteLine($"{Cost[4]} , {storage[4]} , {expertise[E]}");

            return costCovered;
        }

        // Checks if there is a theoretical possibility that cost can be covered, regardless of availability.
        public bool CanCostBeCovered(int[] expertise, Bot opponent)
        {
            var capacity = 10 + expertise.Sum();
            var required = Cost.Sum();
            if (required > capacity)
            {
                return false;
            }

            return
                Cost[A] <= 5 + expertise[A] &&
                Cost[B] <= 5 + expertise[B] &&
                Cost[C] <= 5 + expertise[C] &&
                Cost[D] <= 5 + expertise[D] &&
                Cost[E] <= 5 + expertise[E] ;
        }
    }
}
