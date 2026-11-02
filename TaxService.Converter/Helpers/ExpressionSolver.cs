using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Linq;

namespace TaxService.Manager.Helpers
{
    public static class ExpressionSolver
    {
        static readonly Dictionary<string, int> Precedence = new Dictionary<string, int>
        {
            { "+", 1 },
            { "-", 1 },
            { "*", 2 },
            { "/", 2 },
            { "u-", 3 } // Unary minus has highest priority
        };

        public static decimal Evaluate(string expression)
        {
            if (string.IsNullOrWhiteSpace(expression)) return 0;

            expression = expression.Trim();
            if (expression.StartsWith("="))
                expression = expression.Substring(1);

            ValidateScopes(expression);
            expression = ConvertPercent(expression);

            // Updated Pipeline
            var tokens = Tokenize(expression).ToList();
            var rpn = ToRpn(tokens);
            return EvalRpn(rpn);
        }

        static string ConvertPercent(string expr)
        {
            return Regex.Replace(expr, @"(\d+(\.\d+)?)%", m =>
            {
                decimal v = decimal.Parse(m.Groups[1].Value, CultureInfo.InvariantCulture);
                return (v / 100m).ToString(CultureInfo.InvariantCulture);
            });
        }

        static IEnumerable<string> Tokenize(string expr)
        {
            var token = "";
            for (int i = 0; i < expr.Length; i++)
            {
                char c = expr[i];

                if (char.IsDigit(c) || c == '.')
                {
                    token += c;
                    continue;
                }

                if (!string.IsNullOrEmpty(token))
                {
                    yield return token;
                    token = "";
                }

                if (char.IsWhiteSpace(c)) continue;

                // Handle Unary Minus: if '-' is at start or after '(' or another operator
                if (c == '-')
                {
                    bool isUnary = (i == 0 || expr[i - 1] == '(' || (Precedence.ContainsKey(expr[i - 1].ToString()) && expr[i - 1] != ')'));
                    if (isUnary)
                    {
                        yield return "u-";
                        continue;
                    }
                }

                yield return c.ToString();
            }

            if (!string.IsNullOrEmpty(token))
                yield return token;
        }

        static List<string> ToRpn(IEnumerable<string> tokens)
        {
            var output = new List<string>();
            var ops = new Stack<string>();

            foreach (var t in tokens)
            {
                if (decimal.TryParse(t, NumberStyles.Any, CultureInfo.InvariantCulture, out _))
                {
                    output.Add(t);
                }
                else if (t == "(")
                {
                    ops.Push(t);
                }
                else if (t == ")")
                {
                    while (ops.Count > 0 && ops.Peek() != "(")
                        output.Add(ops.Pop());

                    if (ops.Count == 0) throw new Exception("Mismatched parentheses");
                    ops.Pop();
                }
                else if (Precedence.ContainsKey(t))
                {
                    while (ops.Count > 0 && ops.Peek() != "(" && Precedence.ContainsKey(ops.Peek()) && Precedence[ops.Peek()] >= Precedence[t])
                    {
                        output.Add(ops.Pop());
                    }
                    ops.Push(t);
                }
            }

            while (ops.Count > 0)
            {
                if (ops.Peek() == "(") throw new Exception("Mismatched parentheses");
                output.Add(ops.Pop());
            }

            return output;
        }

        static decimal EvalRpn(List<string> tokens)
        {
            var stack = new Stack<decimal>();

            foreach (var t in tokens)
            {
                // Fixed: Correct TryParse signature for C# 7.3
                if (decimal.TryParse(t, NumberStyles.Any, CultureInfo.InvariantCulture, out decimal v))
                {
                    stack.Push(v);
                }
                else if (t == "u-")
                {
                    stack.Push(-stack.Pop());
                }
                else
                {
                    var b = stack.Pop();
                    var a = stack.Pop();

                    switch (t)
                    {
                        case "+": stack.Push(a + b); break;
                        case "-": stack.Push(a - b); break;
                        case "*": stack.Push(a * b); break;
                        case "/": stack.Push(a / b); break;
                        default: throw new InvalidOperationException("Unknown operator");
                    }
                }
            }

            return stack.Pop();
        }

        static void ValidateScopes(string expr)
        {
            int depth = 0;
            foreach (var c in expr)
            {
                if (c == '(') depth++;
                if (c == ')') depth--;
                if (depth < 0) throw new Exception("Invalid parentheses order");
            }
            if (depth != 0) throw new Exception("Unbalanced parentheses");
        }
    }
}