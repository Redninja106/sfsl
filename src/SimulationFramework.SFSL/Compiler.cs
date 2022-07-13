using SimulationFramework.SFSL.Documents;
using SimulationFramework.SFSL.Parsing;

namespace SimulationFramework.SFSL;

public sealed class Compiler
{
    private readonly CompilationContext context = new();

    public Document AddDocumentFromFile(string path)
    {
        return AddDocument(File.ReadAllText(path), Path.GetFileName(path));
    }

    public Document AddDocument(string source, string name)
    {
        Document result = new();

        var tokens = Scanner.Scan(source);

        tokens = tokens.Where(t=>t.Kind is not TokenKind.Comment);

        var parser = new Parser(new TokenReader(tokens), result.RootNode);

        return null;
    }

    public Compilation Compile(CompilerTarget target, CompilerOptions options)
    {
        return context.MakeResult();
    }
}