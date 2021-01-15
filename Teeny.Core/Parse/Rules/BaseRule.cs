using System.Collections.Generic;
using System.Reflection;
using Teeny.Core.Parse.Validation;

namespace Teeny.Core.Parse.Rules
{
    public abstract class BaseRule : Node
    {
        public RuleGuard Guard { get; } = new RuleGuard();

        public override string Name => GetType().Name;

        public override IEnumerable<Node> Children
        {
            get
            {
                var properties = GetType()
                    .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);

                foreach (var property in properties)
                    if (typeof(BaseRule).IsAssignableFrom(property.PropertyType))
                    {
                        var child = (BaseRule) property.GetValue(this);
                        if (child != null)
                            yield return child;
                    }
                    else if (typeof(TerminalNode).IsAssignableFrom(property.PropertyType))
                    {
                        var child = (TerminalNode) property.GetValue(this);
                        if (child != null)
                            yield return child;
                    }
                    else if (typeof(IEnumerable<BaseRule>).IsAssignableFrom(property.PropertyType))
                    {
                        var children = (IEnumerable<BaseRule>) property.GetValue(this);
                        if (children == null) continue;
                        foreach (var child in children)
                            if (child != null)
                                yield return child;
                    }
            }
        }
    }
}