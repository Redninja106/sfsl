using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationFramework.SFSL.Documents;
public record DocumentRoot : DocumentNode
{
    public readonly List<DocumentNode> Children = new();

    public void AddChild(DocumentNode node)
    {
        if (Children.Contains(node))
            return;

        Children.Add(node);
    }
}
