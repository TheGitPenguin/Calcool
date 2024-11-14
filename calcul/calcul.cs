namespace calcul{
    public class Calcul{
        private Calcul[] _Numbers;
        private string _Type;

        private double _Number;

        static private Dictionary <string, Func<Calcul, Calcul, double?, double>> _Operations = new Dictionary<string, Func<Calcul, Calcul, double?, double>>{
            {"number", (a, b, c) => c.Value},
            {"+", (a, b, c) => a.GetResult() + b.GetResult()},
            {"-", (a, b, c) => a.GetResult() - b.GetResult()},
            {"*", (a, b, c) => a.GetResult() * b.GetResult()},
            {"/", (a, b, c) => a.GetResult() / b.GetResult()},
            {"sin", (a, b, c) => (double)Math.Sin(c.Value)},
            {"cos", (a, b, c) => (double)Math.Cos(c.Value)},
            {"tan", (a, b, c) => (double)Math.Tan(c.Value)},
            {"sqrt", (a, b, c) => (double)Math.Sqrt(c.Value)}
        };

        static private string[] _TypesWithTwoNumbers = new string[]{"+", "-", "*", "/"};
        static private string[] _TypesWithOneNumber = new string[]{"sin", "cos", "tan", "sqrt", "number"};

        public Calcul(string type, Calcul a = null, Calcul b = null, double number = 0){
            if (!_Operations.ContainsKey(type) && type != "number"){
                throw new Exception("Type not found");
            }
            _Type = type;

            if (_TypesWithTwoNumbers.Contains(type)){
                _Numbers = new Calcul[2];
                _Numbers[0] = a;
                _Numbers[1] = b;
            }
            else if (_TypesWithOneNumber.Contains(type)){
                _Number = number;
            }
        }

        public double GetResult(){
            if (_TypesWithOneNumber.Contains(_Type)){
                return _Operations[_Type](null, null, _Number);
            }
            else if (_Numbers.Length == 2){
                return _Operations[_Type](_Numbers[0], _Numbers[1], null);
            }
            throw new Exception("Error");
        }
    }
}

