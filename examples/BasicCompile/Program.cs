using SimulationFramework.SFSL;
using System.Numerics;

Compiler c = new();
c.AddDocument(@"o (world) { ++ / * ""hello  !"" 12 8  }", "test.sfsl");
c.Compile(CompilerTarget.HLSL, 0);