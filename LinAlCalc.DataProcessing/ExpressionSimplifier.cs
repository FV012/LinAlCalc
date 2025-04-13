namespace LinAlCalc.DataProcessing
{
    public class ExpressionSimplifier
    {
        public static ExpressionNode Simplify(ExpressionNode node)
        {
            switch (node)
            {
                case BinaryOpNode bin:
                    var left = Simplify(bin.Left);
                    var right = Simplify(bin.Right);

                    // Пример: x + 0 → x
                    if (bin.Op == "+" && right is ConstantNode c1 && c1.Value == 0)
                        return left;
                    if (bin.Op == "+" && left is ConstantNode c2 && c2.Value == 0)
                        return right;

                    // Пример: x * 0 → 0
                    if (bin.Op == "*" && ((left is ConstantNode c3 && c3.Value == 0) || (right is ConstantNode c4 && c4.Value == 0)))
                        return new ConstantNode(0);

                    // Пример: x * 1 → x
                    if (bin.Op == "*" && right is ConstantNode c5 && c5.Value == 1)
                        return left;
                    if (bin.Op == "*" && left is ConstantNode c6 && c6.Value == 1)
                        return right;

                    // Пример: x^0 → 1
                    if (bin.Op == "^" && right is ConstantNode c7 && c7.Value == 0)
                        return new ConstantNode(1);
                    if (bin.Op == "^" && right is ConstantNode c8 && c8.Value == 1)
                        return left;

                    return new BinaryOpNode(bin.Op, left, right);

                case UnaryOpNode un:
                    var arg = Simplify(un.Argument);

                    // Пример: sin^2(x) + cos^2(x) → 1 (здесь это нужно распознать на уровне выше)
                    // Можно расширить позже

                    return new UnaryOpNode(un.FunctionName, arg);

                default:
                    return node;
            }
        }
    }
}
