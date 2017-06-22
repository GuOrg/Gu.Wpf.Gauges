namespace Gu.Gauges
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    public static class NameOf
    {
        public static string Property(Expression<Func<object>> property)
        {
            var pathVisitor = new PathVisitor();
            pathVisitor.Visit(property);
            return string.Join(".", pathVisitor.Properties.Select(x => x.Name));
        }

        internal class PathVisitor : ExpressionVisitor
        {
            internal readonly List<PropertyInfo> Properties = new List<PropertyInfo>();

            protected override Expression VisitMember(MemberExpression node)
            {
                this.Properties.Insert(0, node.Member as PropertyInfo);
                return base.VisitMember(node);
            }
        }
    }
}
