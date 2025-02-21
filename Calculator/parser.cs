using System.Diagnostics;

public static class Parser {	
  public static (Lang.Num number, string tail) parseNumber(string expression){
    for (int i = 0; i < expression.Length; i++){
      var character = expression[i];
      if (!Char.IsDigit(character)){
        var numString = expression.Substring(0, i);
        var tailString = expression.Substring(i, expression.Length-i);
        if (numString.Length <= 0){
          return(null, "");
        }
        var parsedNumber = new Lang.Num{number = int.Parse(numString)};
        return (parsedNumber, tailString);
      }
    }
    bool success = int.TryParse(expression, out var number);
    if (!success)
    {
      return (null, "");
    }
    return (new Lang.Num{number = number}, "");
  }
  public static string removeSpaces(string expression){
    for (int i = 0; i < expression.Length; i++){
      var character = expression[i];
      if (character!=' '){
        var tailString = expression.Substring(i, expression.Length-i);
        return tailString;
      }
    }
    return "";
  }
  public static (bool valid, string tail) parseAdd(string expression){
    var getOperation = expression.Substring(0, 3);
    var tail = expression.Substring(3,expression.Length-3);
    if (getOperation == "add"){
      return (true, tail);
    }
    return (false, "");
  }
  public static (Lang.Expression add, string tail) parseSimpleExpression(string expression){
    var step1 = removeSpaces(expression);
    var step2 = parseNumber(step1);
    var leftNum = step2.number;
    var step3 = removeSpaces(step2.tail);
    if (step3.Length<3){
      return (leftNum, "");
    }
    var step4 = parseAdd(step3);
    if (!step4.valid){
      return (null, "");
    }
    var step5 = removeSpaces(step4.tail);
    var step6 = parseNumber(step5);
    var step7 = removeSpaces(step6.tail);
    var rightNum = step6.number;
    var add = new Lang.Add{firstExpression = leftNum, secondExpression = rightNum};
    return (add, step7);
  }
  public static (Lang.Expression expression, string tail) parseRight(Lang.Expression leftExp, string expression){
    var step1 = removeSpaces(expression);
    if (step1.Length<3){
      return (null, "");
    }
    var step2 = parseAdd(step1);
    if (!step2.valid){
      return (null, "");
    }
    var step3 = removeSpaces(step2.tail);
    var step4 = parseNumber(step3);
    var step5 = removeSpaces(step4.tail);
    return (new Lang.Add {firstExpression = leftExp, secondExpression = step4.number}, step5);
  }
  public static (Lang.Expression expression, string tail) parseComplex(Lang.Expression expression, string tail){
    // Add simple expression, and recurse with parsing the right-hand arguments until the tail is gone.
    var parse = parseRight(expression, tail);
    if (parse.tail!=""){
      parse = parseComplex(parse.expression, parse.tail);
      return (parse.expression, parse.tail);
    }
    return (parse.expression, "");
  }

  public static string parseExpression(string expression){
    var add = parseSimpleExpression(expression);
    if (add.add == null){
      return "an error. Invalid expression.";
    }
    if (add.tail!=""){
      var complexAdd = parseComplex(add.add, add.tail);
      if (complexAdd.expression == null){
        return "an error. Invalid expression.";
      }
      return ""+Lang.evalExpression(complexAdd.expression);
    }
    return ""+Lang.evalExpression(add.add);
  }
}
