using System.Formats.Asn1;
using System.Globalization;

namespace Calculator {
	public class Lang {
		public class Expression {
			public Abstract a;
		}
		public abstract class Abstract{
			abstract public string Tag { get; }
		}
		public class Num : Abstract {
			public override string Tag
			{
				get { return "Num"; }
			}
			public int a;
		}
		public class Add : Abstract {
			public override string Tag
			{
				get { return "Add"; }
			}
			public Expression a = new Expression();
			public Expression b = new Expression();
		}
		public static int evalExpression(Expression a){
			switch (a.a.Tag) {
				case "Num":
					Num num = (Num)a.a;
					return num.a;
				case "Add":
					Add add = (Add)a.a;
					return evalExpression(add.a) + evalExpression(add.b);
				default:
					return -405;
			};
		}
	}
}
