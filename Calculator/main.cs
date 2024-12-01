namespace Calculator
{
  class Program
  {
    public static void Main(string[] args)
    {
      int eval = Lang.evalExpression(Parser.parseExpression("   2    add    3 +4 plus 60 add    5"));
      Console.WriteLine("The result is " + eval);
    }
  }
}
