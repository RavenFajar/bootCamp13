namespace CalculatorLib{
    public class Calculator{
        public Calculator(){
            Console.WriteLine("Welcome");
        }
        static Calculator()
        {
            Console.WriteLine("Static constructor called");
        }

        public int Add(int a, int b){
            return a+b;
        }
        public double Add(double a, double b){
            return a+b;
        }
        public int Multiply(int a, int b){
            return a*b;
        }
        public  int Subtraction(int a, int b){
            return a-b;
        }
        public int Division(int a, int b){
            return a/b;
        }
    }

}