using SimulationFramework.SFSL.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationFramework.SFSL.Documents;
internal record UnaryExpression(Operator Operator, ExpressionNode Expression) : ExpressionNode
{
}
