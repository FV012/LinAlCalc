using System.Globalization;

namespace LinAlCalc.DataProcessing
{
    public class ExpressionPrinter
    {
        public static string Print(ExpressionNode node)
        {
            switch (node)
            {
                case ConstantNode c:
                    return c.Value.ToString(CultureInfo.InvariantCulture);
                case VariableNode v:
                    return v.Name;
                case BinaryOpNode b:
                    string left = PrintWithBrackets(b.Left, b.Op);
                    string right = PrintWithBrackets(b.Right, b.Op);
                    return $"{left} {b.Op} {right}";
                case UnaryOpNode u:
                    return $"{u.FunctionName}({Print(u.Argument)})";
                default:
                    return "?";
            }
        }

        private static string PrintWithBrackets(ExpressionNode node, string parentOp)
        {
            if (node is BinaryOpNode child)
            {
                // Добавить скобки если приоритет меньше, чем у родителя
                if (NeedsBrackets(child.Op, parentOp))
                    return $"({Print(child)})";
                return Print(child);
            }

            return Print(node);
        }

        private static int OpPrecedence(string op)
        {
            return op switch
            {
                "^" => 3,
                "*" or "/" => 2,
                "+" or "-" => 1,
                _ => 0
            };
        }

        private static bool NeedsBrackets(string childOp, string parentOp)
        {
            return OpPrecedence(childOp) < OpPrecedence(parentOp);
        }
    }
}
