using SimulationFramework.SFSL.Documents;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationFramework.SFSL.Parsing;
internal class Parser
{
    private readonly TokenReader reader;
    private readonly DocumentRoot rootNode;
    public Parser(TokenReader reader, DocumentRoot rootNode)
    {
        this.reader = reader;
        this.rootNode = rootNode;
    }

    public void Parse()
    {
        while (!reader.IsAtEnd)
        {
            rootNode.AddChild(ParseTopLevelDeclaration());
        }
    }

    private DocumentNode ParseTopLevelDeclaration()
    {
        var firstToken = reader.Read();
        
        if (firstToken.GetKeyword() is Keyword.Struct)
        {
            return ParseStruct(firstToken);
        }
        else if (firstToken.Kind is TokenKind.Identifier)
        {
            return ParseVariableOrMethod(firstToken);
        }

        throw new Exception();
    }

    private DocumentNode ParseVariableOrMethod(Token type)
    {
        var nameToken = reader.Read(TokenKind.Identifier);

        var paren = reader.Peek();
        if (paren.GetSymbol() is Symbol.OpenParen)
        {
            return ParseMethod(type, nameToken);
        }
        else
        {
            return ParseVariable(type, nameToken);
        }
    }

    private VariableNode ParseVariable(Token type, Token name)
    {
        var equalsOrSemicolon = reader.Read();
        ExpressionNode expr = null;

        if (equalsOrSemicolon.GetSymbol() is Symbol.Equal)
        {
            expr = ParseExpression();
            equalsOrSemicolon = reader.Read();
        }

        if (equalsOrSemicolon.GetSymbol() is not Symbol.Semicolon)
        {
            throw new Exception();
        }

        return new(type, name, expr);
    }

    private MethodNode ParseMethod(Token type, Token name)
    {
        var openParen = reader.Read(Symbol.OpenParen);
        var parameters = ParseMethodParameterList();
        var closeParen = reader.Read(Symbol.CloseParen);
        var body = ParseBlockStatement();

        return new(type, name, parameters, body);
    }

    private MethodParameterNode[] ParseMethodParameterList()
    {
        // parses: int a, float b, Vector2 c
        List<MethodParameterNode> parameters = new();

        while (true)
        {
            parameters.Add(ParseMethodParameter());
            
            if (reader.Peek().GetSymbol() is Symbol.Comma)
            {
                reader.Read();
            }
            else
            {
                break;
            }
        }

        return parameters.ToArray();
    }
    
    private MethodParameterNode ParseMethodParameter()
    {
        return new(reader.Read(TokenKind.Identifier), reader.Read(TokenKind.Identifier));
    }

    private StructNode ParseStruct(Token structKeyword)
    {
        throw new NotImplementedException();
    }

    private ExpressionNode ParseExpression(OperatorPrecedence precedence = 0)
    {
        Token operand = reader.Peek();
        ExpressionNode result;

        while (Operator.IsOperator(operand.Value) && Operator.GetPrecedence(operand.Value) >= precedence)
        {
            operand = reader.Read();

            var right = ParseExpression(Operator.GetPrecedence(operand.Value));
            

            operand = reader.Peek();
        }
    }
    
    private StatementNode ParseStatement()
    {
        var first = reader.Read();
        
        if (first.GetKeyword() is Keyword.If)
        {
            return ParseIfStatement(first);
        }
        if (first.Kind is TokenKind.Identifier)
        {
            if (reader.Peek().Kind is TokenKind.Identifier)
            {
                return ParseVariable(first, reader.Read());
            }
            else
            {
                reader.PutBack(first);
            }
        }
    }

    private IfStatementNode ParseIfStatement(Token ifKeyword)
    {
        var openParen = reader.Read(Symbol.OpenParen);
        var cond = ParseExpression();
        var closeParen = reader.Read(Symbol.CloseParen);
        var statement = ParseStatement();

        StatementNode elseStatement = null;
        if (reader.Peek().GetKeyword() is Keyword.Else)
        {
            reader.Read();
            elseStatement = ParseStatement();
        }
        
        return new(cond, statement, elseStatement);
    }

    private BlockStatementNode ParseBlockStatement()
    {
        List<StatementNode> statements = new();
        var openBracket = reader.Read(Symbol.OpenBracket);
        while (reader.Peek().GetSymbol() is not Symbol.CloseBracket)
        {
            statements.Add(ParseStatement());
        }
        var closeBracket = reader.Read(Symbol.CloseBracket);
        return new BlockStatementNode(statements.ToArray());
    }
}
