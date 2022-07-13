using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationFramework.SFSL.Parsing;
internal static class OperatorExtensions
{
    public static Operator GetOperator(ReadOnlySpan<char> token)
    {
        return token.ToString() switch
        {
            "+" => Operator.Add,
            "-" => Operator.Subtract,
            "*" => Operator.Multiply,
            "/" => Operator.Divide,
            "=" => Operator.Assign,
            _ => (Operator)(-1),
        };
    }

    public static OperatorKind GetKind(this Operator op)
    {
        switch (op)
        {
            case Operator.Add:
            case Operator.Subtract:
            case Operator.Multiply:
            case Operator.Divide:
                return OperatorKind.Binary;
            case Operator.Assign:
                break;
            default:
                break;
        }
        
        throw new Exception();
    }

    public static OperatorPrecedence GetPrecedence(this Operator op)
    {
        switch (op)
        {
            case Operator.Add:
            case Operator.Subtract:
                return OperatorPrecedence.Additive;
            case Operator.Multiply:
            case Operator.Divide:
                return OperatorPrecedence.Multiplicative;
            case Operator.Assign:
                return OperatorPrecedence.Assignment;
            default:
                break;
        }

        throw new Exception();
    }
}
