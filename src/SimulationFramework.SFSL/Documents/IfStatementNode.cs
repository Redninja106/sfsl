using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationFramework.SFSL.Documents;
internal record IfStatementNode(ExpressionNode Condition, StatementNode ConditionStatement, StatementNode ElseStatement) : StatementNode
{
}
