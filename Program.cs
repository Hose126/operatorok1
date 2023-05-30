namespace Operatorok
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Muvelet> muveletek = new List<Muvelet>();

            //1. feladat
            using (StreamReader fajl = new StreamReader("Datas\\kifejezesek.txt"))
            {
                while (!fajl.EndOfStream)
                {
                    muveletek.Add(new Muvelet(fajl.ReadLine()));
                }
            }

            //2. feladat
            Console.WriteLine($"2. feladat: Kifejezések száma: {muveletek.Count}");

            //3. feladat
            Console.WriteLine($"3. feladat: Kifejezések maradékos osztásssal: {muveletek.Count(x => x.Operat == "mod")}");

            //4. feladat
            int index = 0;
            bool talalat = false;
            do
            {
                talalat = muveletek[index].OperandA % 10 == 0 && muveletek[index].OperandB % 10 == 0;
                index++;
            } while (!talalat);
            Console.WriteLine($"4. feladat: {(talalat ? "Van" : "Nincs")} ilyen kifejezés!");

            //5. feladat
            Console.WriteLine("5. feladat: Statisztika");
            muveletek.Where(x => x.Operat == "+" || x.Operat == "-" || x.Operat == "*" || x.Operat == "/" || x.Operat == "div" || x.Operat == "mod").GroupBy(x => x.Operat).ToList().ForEach(y => Console.WriteLine($"\t\t{y.Key}\t->\t{y.Count()} db"));

            //7. feladat
            string input = String.Empty;
            do
            {
                if (!string.IsNullOrEmpty(input))
                {
                    try
                    {
                        Muvelet muvelet = new Muvelet(input);
                        Console.WriteLine($"\t{muvelet.ToString()}");
                    }
                    catch (Exception)
                    {
                        Console.WriteLine("\tHibásan bevitt adatsor!");
                    }
                }
                Console.Write("7. feladat: Kérek egy kifejezést (pl.: 1 + 1): ");
                input = Console.ReadLine();
            } while (input != "vége");

            //8. feladat
            using (StreamWriter fajl = new StreamWriter("eredmenyek.txt"))
            {
                muveletek.ForEach(x => fajl.WriteLine(x.ToString()));
            }
            Console.WriteLine("8. feladat: eredmenyek.txt");
            Console.ReadKey();
        }
        class Muvelet
        {
            int operandA, operandB;
            string operat;

            public int OperandA { get => operandA; set => operandA = value; }
            public int OperandB { get => operandB; set => operandB = value; }
            public string Operat { get => operat; set => operat = value; }

            //6. feladat
            public string Result
            {
                get
                {
                    try
                    {
                        switch (Operat)
                        {
                            case "mod":
                                return (OperandA % OperandB).ToString();
                            case "/":
                                return (OperandA * 1.00 / OperandB).ToString();
                            case "div":
                                return (OperandA / OperandB).ToString();
                            case "-":
                                return (OperandA - OperandB).ToString();
                            case "*":
                                return (OperandA * OperandB).ToString();
                            case "+":
                                return (OperandA + OperandB).ToString();
                            default:
                                return "Hibás operátor!";
                        }
                    }
                    catch (Exception)
                    {
                        return "Egyéb hiba!";
                    }
                }
            }

            public Muvelet(string adatsor)
            {
                string[] adatok = adatsor.Split(' ');
                OperandA = int.Parse(adatok[0].Trim());
                operat = adatok[1].Trim();
                OperandB = int.Parse(adatok[2].Trim());
            }

            public override string ToString()
            {
                return $"{OperandA} {Operat} {OperandB} = {Result}";
            }
        }
    }
}