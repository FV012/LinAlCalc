namespace LinAlCalc.DataProcessing
{
    public abstract class ExpressionNode { }

    public class ConstantNode : ExpressionNode
    {
        public double Value;
        public ConstantNode(double value) => Value = value;
    }

    public class VariableNode : ExpressionNode
    {
        public string Name;
        public VariableNode(string name) => Name = name;
    }

    public class BinaryOpNode : ExpressionNode
    {
        public string Op;
        public ExpressionNode Left, Right;

        public BinaryOpNode(string op, ExpressionNode left, ExpressionNode right)
        {
            Op = op; Left = left; Right = right;
        }
    }

    public class UnaryOpNode : ExpressionNode
    {
        public string FunctionName;
        public ExpressionNode Argument;

        public UnaryOpNode(string functionName, ExpressionNode arg)
        {
            FunctionName = functionName; Argument = arg;
        }
    }
}
