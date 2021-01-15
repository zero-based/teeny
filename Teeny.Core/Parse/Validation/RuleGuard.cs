using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Teeny.Core.Parse.Rules;
using Teeny.Core.Scan;

namespace Teeny.Core.Parse.Validation
{
    public class RuleGuard
    {
        public ICollection<IGuardError> Errors { get; } = new List<IGuardError>();

        private static (string name, T value) GetNameValue<T>(Expression<Func<T>> expression)
        {
            var memberExpression = expression.Body as MemberExpression;
            var name = memberExpression?.Member.Name;
            var value = expression.Compile()();
            return (name, value);
        }

        public T NonNull<T>(Expression<Func<T>> field)
        {
            var (name, value) = GetNameValue(field);
            if (value == null)
                Errors.Add(new NullFieldError
                {
                    FieldName = name
                });

            return value;
        }

        public TerminalNode OneOf(Expression<Func<TerminalNode>> field, params Token[] validTokens)
        {
            var (name, value) = GetNameValue(field);

            if (validTokens.All(token => value?.Token != token))
                Errors.Add(new InvalidTokenError
                {
                    FieldName = name,
                    FieldValue = value,
                    ExpectedTokens = validTokens
                });

            return value;
        }
    }
}