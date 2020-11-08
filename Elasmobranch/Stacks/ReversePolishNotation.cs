using System.Collections.Generic;
using System.Data;

namespace Elasmobranch.Stacks
{
    public class ReversePolishNotation
    {
        private readonly CustomStack<decimal> _stack;
        private readonly string[] _tokens;
        
        public ReversePolishNotation(string input)
        {
            // split the string using spaces
            _tokens = input.Split(" ");
            
            // the stack should not exceed the number of total tokens
            _stack = new CustomStack<decimal>(_tokens.Length);
        }

        public decimal Evaluate()
        {
            foreach (var token in _tokens)
            {
                if (decimal.TryParse(token, out var number))
                {
                    _stack.Push(number);
                }
                else
                {
                    switch (token)
                    {
                        case "+":
                            _stack.Push(_stack.Pop() + _stack.Pop());
                            break;
                        case "-":
                            // x y - means subtract y from x; consider the order of the stack
                            number = _stack.Pop();
                            _stack.Push(_stack.Pop() - number);
                            break;
                        case "*":
                            _stack.Push(_stack.Pop() * _stack.Pop());
                            break;
                        case "/":
                            // x y / means divide x by y; consider the order of the stack
                            number = _stack.Pop();
                            _stack.Push(_stack.Pop() / number);
                            break;
                        // reverse divide operator, x y \ means divide y by x
                        case "\\":
                            _stack.Push(_stack.Pop() / _stack.Pop());
                            break;
                        default:
                            throw new EvaluateException($"Invalid token {token}.");
                    }
                }
            }

            return _stack.Pop();
        }
    }
}