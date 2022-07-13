﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SimulationFramework.SFSL.Documents;
internal record VariableNode(Token Type, Token Name, ExpressionNode Initializer) : StatementNode
{
}