using System;
using System.Linq;
using System.Linq.Expressions;

namespace Envisia.Library.Extensions
{
    public static class LambdaExtensions
    {
        public static string GetName<TTarget, TValue>(this Expression<Func<TTarget, TValue>> expression)
        {
            var body = (MemberExpression)expression.Body;

            return body.Member.Name; //text
        }

        public static TValue GetValue<TTarget, TValue>(this Expression<Func<TTarget, TValue>> expression, TTarget target)
        {
            var name = expression.GetName();
            var property = target.GetType().GetProperties().First(x => x.Name == name);

            return (TValue)property.GetValue(target, null);            
        }

        public static void SetValue<TTarget, TValue>(this Expression<Func<TTarget, TValue>> expression, TTarget target, TValue value)
        {
            var name = expression.GetName();
            var property = target.GetType().GetProperties().First(x => x.Name == name);

            property.SetValue(target, value);
        }
    }
}
