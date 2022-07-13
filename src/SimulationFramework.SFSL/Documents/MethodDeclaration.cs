using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationFramework.SFSL.Documents;
internal record MethodNode(Token Type, Token Name, MethodParameterNode[] Parameters, BlockStatementNode Body) : DocumentNode
{
}
