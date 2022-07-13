using SimulationFramework.SFSL.Parsing;
using System;
using System.Buffers;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationFramework.SFSL;

internal static class Scanner
{
    public static IEnumerable<Token> Scan(string source)
    {
        return MergeOperators(ScanNoOperators(source));
    }
    
    private static IEnumerable<Token> ScanNoOperators(string source)
    {

        int start = 0;
        int current = 0;
        ReadOnlySpan<char> tokenSpan;

        for (; current < source.Length; current++)
        {
            tokenSpan = source.AsSpan(start..current);
            char c = source[current];

            var zeroLength = start == current;
            var continued = ContinuesToken(tokenSpan, c);

            if (!continued && !zeroLength)
            {
                if (tokenSpan.Length > 0 && !tokenSpan.IsWhiteSpace())
                    yield return new Token(source, start..current, CategorizeToken(tokenSpan), char.IsWhiteSpace(c));

                start = current;
            }
        }

        tokenSpan = source.AsSpan(start..(current));

        if (tokenSpan.Length > 0 && !tokenSpan.IsWhiteSpace())
            yield return new Token(source, start..(current), CategorizeToken(tokenSpan), true);
        
    }

    private static bool ContinuesToken(ReadOnlySpan<char> token, char continuation)
    {
        if (IsIdentifier(token))
            return IsIdentifier(continuation.ToString()) || char.IsNumber(continuation);

        if (IsNumericLiteral(token))
            return char.IsDigit(continuation);

        // single line comment
        if (token == "/" && continuation == '/')
            return true;
        if (token.StartsWith("//"))
            return token[^1] is not '\n';

        // multi line comment
        if (token == "/" && continuation == '*')
            return true;
        if (token.StartsWith("/*"))
            return !token.EndsWith("*/");

        // string literal
        if (token.StartsWith("\""))
            return token.Length <= 1 || token[^1] is not '"';

        if (IsOperator((token.ToString() + continuation).ToArray()))
            return true;

        return false;
    }

    private static TokenKind CategorizeToken(ReadOnlySpan<char> token)
    {
        if (IsKeyword(token, out _)) return TokenKind.Keyword;
        if (IsIdentifier(token)) return TokenKind.Identifier;
        if (IsNumericLiteral(token)) return TokenKind.NumericLiteral;
        if (IsComment(token)) return TokenKind.Unknown;
        if (IsSymbol(token, out _)) return TokenKind.Symbol;
        if (IsStringLiteral(token)) return TokenKind.StringLiteral;
        return TokenKind.Unknown;
    }

    public static bool IsIdentifier(ReadOnlySpan<char> token)
    {
        if (token.Length is 0)
            return false;

        if (!char.IsLetter(token[0]) && token[0] != '_')
            return false;

        for (int i = 1; i < token.Length; i++)
        {
            if (!char.IsLetterOrDigit(token[i]) && token[0] != '_')
                return false;
        }

        return true;
    }

    public static bool IsNumericLiteral(ReadOnlySpan<char> token)
    {
        if (token.Length is 0)
            return false;

        for (int i = 0; i < token.Length; i++)
        {
            if (!char.IsDigit(token[i]))
                return false;
        }

        return true;
    }

    public static bool IsComment(ReadOnlySpan<char> token)
    {
        if (token.StartsWith("//"))
            return token[^1] is '\n';

        if (token.StartsWith("/*"))
            return token.EndsWith("*/");

        return false;
    }

    public static bool IsKeyword(ReadOnlySpan<char> token, out Keyword keyword)
    {
        for (int i = 0; i < token.Length; i++)
        {
            if (!char.IsLower(token[i]))
            {
                keyword = 0;
                return false;
            }
        }

        return Enum.TryParse(token, true, out keyword);
    }

    public static bool IsSymbol(ReadOnlySpan<char> token, out Symbol symbol)
    {
        if (token.Length is not 1)
        {
            symbol = 0;
            return false;
        }
        
        symbol = (Symbol)token[0];
        return Enum.IsDefined(symbol);   
    }

    public static bool IsStringLiteral(ReadOnlySpan<char> token)
    {
        if (token.Length < 2)
            return false;

        if (token[0] != '"')
            return false;

        if (token[^1] != '"')
            return false;

        return true;
    }

    public static bool IsOperator(ReadOnlySpan<char> token, out Operator op)
    {
        op = OperatorExtensions.GetOperator(token);
        return Enum.IsDefined(op);
    }
}