using System.Linq.Expressions;
using Tessa.Education.BLL.Utils;

namespace Tessa.Education.BLL
{
    public static class ExpressionBuilder
    {
        public static Expression<Func<T, bool>> AndFilterExpression<T>(Expression<Func<T, bool>> expr1, Expression<Func<T, bool>> expr2)
        {
            var paramExpr = Expression.Parameter(typeof(T));
            var exprBody = Expression.And(expr1.Body, expr2.Body);
            exprBody = (BinaryExpression)new ParameterReplacer(paramExpr).Visit(exprBody);
            var finalExpr = Expression.Lambda<Func<T, bool>>(exprBody, paramExpr);
            return finalExpr;
        }
    }
}
