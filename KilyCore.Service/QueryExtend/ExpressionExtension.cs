using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
/// <summary>
/// 作者：刘泽华
/// 时间：2018年5月29日12点01分
/// </summary>
namespace KilyCore.Service.QueryExtend
{
    /// <summary>
    /// Linq表达式拓展
    /// </summary>
    public class ExpressionExtension
    {
        /// <summary>
        /// 表达式树
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="property">字段</param>
        /// <param name="param">值</param>
        /// <param name="QueryType">类型</param>
        /// <returns></returns>
        public static Expression<Func<TEntity, bool>> GetExpression<TEntity>(string property, object param, ExpressionEnum QueryType)
        {
            ParameterExpression Parameter = Expression.Parameter(typeof(TEntity), "t");
            if (typeof(TEntity).GetProperty(property) == null)
            {
                throw new Exception("字段名不存在，请检查！");
            }
            MemberExpression Left = Expression.Property(Parameter, typeof(TEntity).GetProperty(property));
            ConstantExpression Right = Expression.Constant(param, param.GetType());
            Expression Filter = null;
            switch (QueryType)
            {
                case ExpressionEnum.Like:
                    Filter = Expression.Call(Left, typeof(String).GetMethod("Contains"), Right);
                    break;
                case ExpressionEnum.NotLike:
                    Filter = Expression.Not(Expression.Call(Left, typeof(String).GetMethod("Contains"), Right));
                    break;
                case ExpressionEnum.Equals:
                    Filter = Expression.Equal(Left,Right);
                    break;
                case ExpressionEnum.NotEquals:
                    Filter=Expression.NotEqual(Left, Right);
                    break;
                case ExpressionEnum.GreaterThan:
                    Filter=Expression.GreaterThan(Left, Right);
                    break;
                case ExpressionEnum.GreaterThanOrEqual:
                    Filter =Expression.GreaterThanOrEqual(Left, Right);
                    break;
                case ExpressionEnum.LessThan:
                    Filter=Expression.LessThan(Left, Right);
                    break;
                case ExpressionEnum.LessThanOrEqual:
                    Filter = Expression.LessThanOrEqual(Left, Right);
                    break;
                default:
                    Filter = Expression.Equal(Left, Right);
                    break;
            }
            return Expression.Lambda<Func<TEntity, bool>>(Filter, Parameter);
        }
    }
}
