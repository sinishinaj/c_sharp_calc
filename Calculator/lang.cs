public static class Lang {	

	//public struct Option<T>
	/* {
		public static Option<T> None => default;
		public static Option<T> Some(T value) => new Option<T>(value);

		readonly bool isSome;
		readonly T value;

		Option(T value)
		{
				this.value = value;
				isSome = this.value is { };
		}

		public bool IsSome(out T value)
		{
				value = this.value;
				return isSome;
		}
	} */
	public class Num : Expression {
		public string tag {get{ return "Num";}}
		public int number {get; init;}
	}
	public class Add : Expression {
		public string tag {get{ return "Add";}}
		public Expression firstExpression {get; init;}
		public Expression secondExpression {get; init;}
	}

	public interface Expression
	{
		public string tag { get;}
	}

	public static int evalExpression(Expression expression){
		switch (expression.tag) {
			case "Num":
				Num num = (Num)expression;
				return num.number;
			case "Add":
				Add add = (Add)expression;
				return evalExpression(add.firstExpression) + evalExpression(add.secondExpression);
			default:
				return -405;
		};
	}
}
