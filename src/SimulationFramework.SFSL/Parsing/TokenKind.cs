using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationFramework.SFSL.Parsing;

public enum TokenKind
{
    Unknown = 0,
    Identifier,
    StringLiteral,
    NumericLiteral,
    Symbol,
    Keyword,
    Comment,
}
