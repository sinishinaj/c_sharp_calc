using System.Linq.Expressions;

namespace Calculator
{
  class Program
  {
    public static void Main(string[] args)
    {
      int eval = Lang.evalExpression(constructComplex());
      Console.WriteLine("The result is " + eval);
    }
    public static Lang.Expression constructSimple(){
      Lang.Expression expression = new Lang.Expression();
      Lang.Add b = new Lang.Add();
      Lang.Num c = new Lang.Num();
      Lang.Num d = new Lang.Num();
      c.a = 2;
      d.a = 2;
      b.a.a = c;
      b.b.a = d;
      expression.a = b;
      return expression;
    }
    public static Lang.Expression constructComplex(){
      Lang.Expression expression = new Lang.Expression();
      Lang.Add b = new Lang.Add();
      Lang.Num c = new Lang.Num();
      Lang.Add d = new Lang.Add();
      Lang.Num e = new Lang.Num();
      Lang.Num f = new Lang.Num();
      c.a = 2;
      e.a = 3;
      f.a = 5;
      d.a.a = e;
      d.b.a = f;
      b.a.a = c;
      b.b.a = d;
      expression.a = b;
      return expression;
    }
  }
}
