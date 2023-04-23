namespace Reflection
{
    public class Market
    {
        public double ktb1y { get; set; }
        public double ktb3y { get; set; }
        // ... other ktb variables up to ktb20y
    }

    public class Greeks
    {
        public DateTime date { get; set; }
        public string seg { get; set; }
        public double ktb1y { get; set; }
        public double ktb3y { get; set; }
        // ... other ktb variables up to ktb20y
    }

    public class Result
    {
        public double SumProduct { get; set; }
    }

   

    class Program
    {
        static void Main()
        {
            var market = new Market { ktb1y = 1.0, ktb3y = 2.0 };
            var greeks = new Greeks { ktb1y = 3.0, ktb3y = 4.0 };
            var calculator = new Calculator();
            var result= Calculator.Calculate(market, greeks);
            Console.WriteLine(result.SumProduct); 
        }   
     }

    public class Calculator
    {
        public static Result Calculate(Market market, Greeks greeks)
        {
            var result = new Result();
            var marketProps = market.GetType().GetProperties();
            var greeksProps = greeks.GetType().GetProperties();

            foreach (var marketProp in marketProps)
            {
                if (marketProp.Name.StartsWith("ktb"))
                {
                    var matchingGreekProp = greeksProps.FirstOrDefault(p => p.Name == marketProp.Name);
                    if (matchingGreekProp != null)
                    {
                        var marketValue = (double)marketProp.GetValue(market);
                        var greekValue = (double)matchingGreekProp.GetValue(greeks);
                        result.SumProduct += marketValue * greekValue;
                    }
                }
            }

            return result;
        }
    }
}

    
