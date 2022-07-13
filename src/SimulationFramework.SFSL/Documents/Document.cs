using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationFramework.SFSL.Documents;

public sealed class Document
{
    public string Source;
    public Token[] Tokens;
    public DocumentRoot RootNode;
}
