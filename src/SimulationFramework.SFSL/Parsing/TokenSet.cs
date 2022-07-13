using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationFramework.SFSL.Parsing;

internal delegate bool TokenPredicate(Token token);

//internal class TokenSet
//{
//    public TokenSet(Action<Builder> init)
//    {
//        var builder = new Builder();
//        init(builder);
//    }

//    public bool Match(Token token)
//    {

//    }

//    public class Builder
//    {
//        Builder AddKind(TokenKind kind);
//        Builder AddKeyword(TokenKind kind);
//    }
//}
