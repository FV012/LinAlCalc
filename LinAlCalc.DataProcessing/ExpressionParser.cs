using System.Globalization;

namespace LinAlCalc.DataProcessing
{
    public class ExpressionParser
    {
        private static string _input;
        private static int _pos;

        public static ExpressionNode Parse(string input)
        {
            _input = input.Replace(" ", "").ToLower();
            _pos = 0;
            return ParseExpression();
        }

        private static ExpressionNode ParseExpression(int minPrecedence = 1)
        {
            var left = ParsePrimary();

            while (_pos < _input.Length)
            {
                string op = PeekOperator();

                if (op == null || GetPrecedence(op) < minPrecedence)
                    break;

                _pos += op.Length;

                var right = ParseExpression(GetPrecedence(op) + (IsRightAssociative(op) ? 0 : 1));
                left = new BinaryOpNode(op, left, right);
            }

            return left;
        }

        private static ExpressionNode ParsePrimary()
        {
            if (_pos >= _input.Length)
                throw new Exception("Unexpected end of input");

            if (_input[_pos] == '(')
            {
                _pos++;
                var expr = ParseExpression();
                if (_pos >= _input.Length || _input[_pos] != ')')
                    throw new Exception("Missing closing parenthesis");
                _pos++;
                return expr;
            }

            if (char.IsLetter(_input[_pos]))
            {
                string name = ParseIdentifier();

                if (_pos < _input.Length && _input[_pos] == '(')
                {
                    _pos++; // skip '('
                    var arg = ParseExpression();
                    if (_pos >= _input.Length || _input[_pos] != ')')
                        throw new Exception("Missing closing parenthesis in function");
                    _pos++;
                    return new UnaryOpNode(name, arg);
                }

                if (name == "pi") return new ConstantNode(Math.PI);
                if (name == "e") return new ConstantNode(Math.E);

                return new VariableNode(name);
            }

            if (char.IsDigit(_input[_pos]) || _input[_pos] == '.')
            {
                return new ConstantNode(ParseNumber());
            }

            if (_input[_pos] == '-')
            {
                _pos++;
                var expr = ParsePrimary();
                return new BinaryOpNode("*", new ConstantNode(-1), expr);
            }

            throw new Exception($"Unexpected character '{_input[_pos]}' at position {_pos}");
        }

        private static double ParseNumber()
        {
            int start = _pos;
            while (_pos < _input.Length && (char.IsDigit(_input[_pos]) || _input[_pos] == '.'))
                _pos++;
            var numStr = _input.Substring(start, _pos - start);
            if (!double.TryParse(numStr, NumberStyles.Any, CultureInfo.InvariantCulture, out var value))
                throw new Exception($"Invalid number: {numStr}");
            return value;
        }

        private static string ParseIdentifier()
        {
            int start = _pos;
            while (_pos < _input.Length && char.IsLetter(_input[_pos]))
                _pos++;
            return _input.Substring(start, _pos - start);
        }

        private static string PeekOperator()
        {
            string[] ops = { "^", "*", "/", "+", "-" };

            foreach (var op in ops)
            {
                if (_input.Substring(_pos).StartsWith(op))
                    return op;
            }

            return null;
        }

        private static int GetPrecedence(string op)
        {
            return op switch
            {
                "^" => 4,
                "*" or "/" => 3,
                "+" or "-" => 2,
                _ => 0
            };
        }

        private static bool IsRightAssociative(string op) => op == "^";
    }
}
