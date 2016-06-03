using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace SySInventory.Core.Common.Extensions
{
    public static class ReflectionExtensions
    {
        public static bool HasAttribute<T>(this MemberInfo member) where T : Attribute
        {
            return member.GetCustomAttributes(typeof(T), true).Length > 0;
        }

        public static T GetAttribute<T>(this MemberInfo member) where T : Attribute
        {
            return member.GetAttributes<T>().FirstOrDefault();
        }

        public static IEnumerable<T> GetAttributes<T>(this MemberInfo member) where T : Attribute
        {
            return Attribute.GetCustomAttributes(member, typeof(T)).Cast<T>();
        }

        public static PropertyInfo GetProperty(this LambdaExpression property)
        {
            return (PropertyInfo)property.Body.GetMemberExpression().Member;
        }

        public static string GetPropertyName(this LambdaExpression property)
        {
            var currentMember = property.Body.TryGetMemberExpression();
            var propertyNameChain = new List<string>();
            while (currentMember != null)
            {
                propertyNameChain.Add(currentMember.Member.Name);
                currentMember = currentMember.Expression.TryGetMemberExpression();
            }
            propertyNameChain.Reverse();
            return string.Join(".", propertyNameChain.ToArray());
        }

        public static MemberExpression TryGetMemberExpression(this Expression expression)
        {
            MemberExpression memberExpression = null;
            switch (expression.NodeType)
            {
                case ExpressionType.Convert:
                    {
                        var body = (UnaryExpression)expression;
                        memberExpression = body.Operand as MemberExpression;
                    }
                    break;
                case ExpressionType.MemberAccess:
                    memberExpression = expression as MemberExpression;
                    break;
            }
            return memberExpression;
        }

        public static MemberExpression GetMemberExpression(this Expression expression)
        {
            var memberExpression = expression.TryGetMemberExpression();
            if (memberExpression == null) throw new ArgumentException("Not a member access", "expression");
            return memberExpression;
        }

        public static MethodInfo GetMethod(this LambdaExpression expression)
        {
            return ((MethodCallExpression)expression.Body).Method;
        }
    }
}
