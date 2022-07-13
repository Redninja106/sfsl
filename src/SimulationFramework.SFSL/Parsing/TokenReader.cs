using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationFramework.SFSL.Parsing;

internal sealed class TokenReader
{
    private Stack<Token> returned;
    private IEnumerator<Token> enumerator;
    private Token current;
    private int position;

    public bool IsAtEnd { get; private set; }

    public TokenReader(IEnumerable<Token> enumerable)
    {
        enumerator = enumerable.GetEnumerator();
    }

    public Token Read()
    {
        if (returned.Any())
        {
            return returned.Pop();
        }
        else
        {
            var result = current;

            if (enumerator.MoveNext())
                current = enumerator.Current;
            else
                IsAtEnd = true;

            return result;
        }
    }

    public Token Read(TokenPredicate predicate)
    {
        if (predicate(current))
            return Read();

        throw null;
        //return new Token(position, TokenKind.Unknown);
    }
    public Token Read(TokenKind kind) => Read(t => t.Kind == kind);
    public Token Read(Symbol symbol) => Read(t => Scanner.IsSymbol(t.Value, out var s) && s == symbol);
    public Token Read(Keyword keyword) => Read(t => Scanner.IsKeyword(t.Value, out var k) && k == keyword);

    public void PutBack(Token token)
    {
        returned.Push(token);
    }

    public Token Peek()
    {
        var result = Read();
        PutBack(result);
        return result;
    }
}
