using System.Globalization;
using System.Linq.Expressions;

public class Parser {
  public static bool isDigit(char a){
    switch(a){
      case '1':
        return true;
      case '2':
        return true;
      case '3':
        return true;
      case '4':
        return true;
      case '5':
        return true;
      case '6':
        return true;
      case '7':
        return true;
      case '8':
        return true;
      case '9':
        return true;
      case '0':
        return true;
    }
    return false;
  }
  public static bool isOperand(char a){
    switch(a){
      case '+':
        return true;
    }
    return false;
  }
  public static string cleanExpression(string a){
    string pure = "";
    for (int ci = 0; ci < a.Length; ci++) 
    {
      if (isDigit(a[ci]) || isOperand(a[ci])){
        pure+=a[ci];
      } 
    }
    return pure;
  }
  public static (string digits, string tail) parseDigits(string a){
    string digits = "";
    string tail = "";
    for (int ci = 0; ci < a.Length; ci++) 
    {
      if (!isDigit(a[ci])){
        tail = a;
        tail = tail.Remove(0, ci);
        break;
      }
      digits+=a[ci];
    }
    return (digits, tail);
  }
	public static (Lang.Expression expression, string tail) parseSimple(string a){
    Lang.Expression expression = new Lang.Expression();
    // Read left number.
    var parse = parseDigits(a);
    Lang.Num leftNum = constructNum(int.Parse(parse.digits));
    expression.a = leftNum;
    // Check if it's just one number.
    if (parse.tail == ""){
      return (expression,"");
    }
    // Proceed  with right hand.
    var parseR = parseRight(expression, parse.tail);
    return (parseR.expression, parseR.tail);
  }
  public static (Lang.Expression expression, string tail) parseRight(Lang.Expression leftExp, string a){
    Lang.Expression expression = new Lang.Expression();
    // Read right number, and cut-off operand.
    a = a.Remove(0,1);
    var parse = parseDigits(a);
    Lang.Num rightNum = constructNum(int.Parse(parse.digits));
    // Construct expression.
    Lang.Add add = new Lang.Add();
    add.a = leftExp;
    add.b.a = rightNum;
    expression.a = add;
    return (expression, parse.tail);
  }
  public static (Lang.Expression expression, string tail) parseComplex(Lang.Expression inExpression, string a){
    var parse = parseRight(inExpression, a);
    if (parse.tail!=""){
      parse = parseComplex(parse.expression, parse.tail);
      return (parse.expression, parse.tail);
    }
    return (parse.expression, "");
  }
  public static Lang.Num constructNum(int a){
    Lang.Num b = new Lang.Num();
    b.a = a;
    return b;
  }
  public static Lang.Expression parseExpression(string a){
    Lang.Expression expression = new Lang.Expression();

    // Purify input.
    string pure = cleanExpression(a);
    
    // Attempt simple expression.
    var parse = parseSimple(pure);
    expression = parse.expression;

    // If complex, attempt complex expression.
    if (parse.tail!=""){
      parse = parseComplex(expression, parse.tail);
    }
    return parse.expression;
	}
}