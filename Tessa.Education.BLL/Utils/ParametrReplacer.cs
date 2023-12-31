﻿using System.Linq.Expressions;

namespace Tessa.Education.BLL.Utils
{
    internal class ParameterReplacer
     : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;

        protected override Expression VisitParameter(ParameterExpression node)
        {
            return base.VisitParameter(_parameter);
        }

        internal ParameterReplacer(ParameterExpression parameter)
        {
            _parameter = parameter;
        }

    }
}
