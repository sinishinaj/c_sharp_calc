using System.ComponentModel;
using System.Globalization;
using System.Linq.Expressions;

namespace Calculator
{
  class Program
  {
    public static void Main(string[] args)
    {
      string eval = Parser.parseExpression("    2 add 2    add 2     add 3     ");
      Console.WriteLine("The result is " + eval);
    }
  }
}
