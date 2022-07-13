using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationFramework.SFSL.Parsing;
internal enum Operator
{
    // unary
    //PlusPlus,
    //MinusMinus,
    //Positive,
    //Negate,

    // binary arithmetic
    Add,
    Subtract,
    Multiply,
    Divide,
    //Modulus,

    // comparason
    //LessThan,
    //GreaterThan,
    //Equality,
    //Inequality,
    //LessThenEqual,
    //GreaterThenEqual,

    //Call,
    //Index,
    //Dot,

    //// boolean/bitwise
    //ShiftLeft,
    //ShiftRight,
    //LogicalAnd,
    //LogicalOr,
    //LogicalNot,
    //LogicalXor,
    //BitwiseNot,
    //BitwiseAnd,
    //BitwiseOr,
    //BitwiseXor,

    //// Assignment
    Assign,
    //PlusEqual,
    //SubtractEqaul,
    //MultiplyEqual,
    //DivideEqual,
    //ModulusEqual,
    //BitwiseAndEqual,
    //BitwiseOrEqual,
    //BitwiseXorEqual,
    //ShiftLeftEqual,
    //ShiftRightEqual,
}
