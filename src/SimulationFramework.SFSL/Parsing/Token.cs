using SimulationFramework.SFSL.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationFramework.SFSL;

public readonly struct Token
{
    public string Source { get; init; }
    public Range Range { get; init; }
    public TokenKind Kind { get; init; }
    public bool HasTrailingWhitespace { get; init; }

    public ReadOnlySpan<char> Value => Source.AsSpan(Range);
 
    public Token(string source, Range range, TokenKind kind, bool hasTrailingWhitespace)
    {
        this.Source = source;
        this.Range = range;
        this.Kind = kind;
        this.HasTrailingWhitespace = hasTrailingWhitespace;
    }

    public override string ToString()
    {
        return $"{Kind} '{Value}' {HasTrailingWhitespace}";
    }

    public Keyword GetKeyword()
    {
        if (Kind is TokenKind.Keyword && Scanner.IsKeyword(Value, out var keyword))
            return keyword;
        
        return (Keyword)(-1);
    }
    
    public Symbol GetSymbol()
    {
        if (Kind is TokenKind.Symbol && Scanner.IsSymbol(Value, out var symbol))
            return symbol;
        
        return (Symbol)(-1);
    }
}
