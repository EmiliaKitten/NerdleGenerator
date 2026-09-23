using System.Collections;

namespace Nerdle;

public class NerdleGenerator
{
    private Random rnd;
    private ArrayList puzzle;

    public NerdleGenerator()
    {
        rnd = new Random();
        puzzle = new ArrayList();
        this.Next();
    }

    public ArrayList getPuzzle()
    {
        return this.puzzle;
    }

    public void Next()
    {
        (ArrayList numbers, ArrayList operators) LHS;
        string? r;

        int ComputeLength((ArrayList numbers, ArrayList operators) e, string result)
        {
            int total = 0;
            foreach (string n in e.numbers) total += n.Length;
            foreach (string op in e.operators) total += op.Length;
            total += 1; 
            total += result.Length;
            return total;
        }

        do
        {
            LHS = this.LHS();
            r = this.RHS(LHS);

        } while (r == null || int.Parse(r) <= 0 || ComputeLength(LHS, r) != 8);

        puzzle = new ArrayList();
        puzzle.Add((string)LHS.numbers[0]);
        for (int i = 1; i < LHS.numbers.Count; i++)
        {
            puzzle.Add((string)LHS.operators[i - 1]);
            puzzle.Add((string)LHS.numbers[i]);
        }
        puzzle.Add("=");
        puzzle.Add(r);
    }

    private (ArrayList numbers, ArrayList operators) LHS()
    {
        ArrayList numbers = this.getRandNumbers();
        ArrayList operators = new ArrayList();

        for (int i = 0; i < numbers.Count - 1; i++)
        {
            operators.Add(this.RandOperator());
        }

        return (numbers, operators);
    }

    private string? RHS((ArrayList numbers, ArrayList operators) e)
    {
        Dictionary<string, Func<int, int, int?>> solveMap = new()
        {
            ["+"] = (n, m) => n + m,
            ["-"] = (n, m) => n - m,
            ["*"] = (n, m) => n * m,
            ["/"] = (n, m) => (m != 0 && n % m == 0) ? n / m : (int?)null
        };

        int n = int.Parse((string)e.numbers[0]);

        for (int i = 1; i < e.numbers.Count; i++)
        {
            int m = int.Parse((string)e.numbers[i]);
            string op = (string)e.operators[i - 1];

            int? result = solveMap[op](n, m);

            if (result == null)
            {
                return null;
            }

            n = result.Value;
        }

        return n.ToString();
    }

    private string RandNumber()
    {
        int size = rnd.Next(1, 4);

        int min = (int)Math.Pow(10, size - 1);
        int max = (int)Math.Pow(10, size);

        return rnd.Next(min, max).ToString();
    }

    private ArrayList getRandNumbers()
    {
        ArrayList result = new ArrayList(); 
        int n = rnd.Next(2, 4);

        for (int i = 0; i < n; i++)
        {
            result.Add(this.RandNumber());
        }

        return result;
    }

    private static readonly string[] SYMBOLEN = ["+", "-", "*", "/"];
    private string RandOperator()
    {
        return SYMBOLEN[rnd.Next(SYMBOLEN.Length)];
    }
}
