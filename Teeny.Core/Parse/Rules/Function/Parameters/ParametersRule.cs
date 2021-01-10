namespace Teeny.Core.Parse.Rules.Function.Parameters
{
    public class ParametersRule : BaseRule
    {
        public ParametersRule(ParameterRule parameter, ExtraParameterRule extraParameter)
        {
            Parameter = parameter;
            ExtraParameter = extraParameter;
        }

        public ParameterRule Parameter { get; set; }
        public ExtraParameterRule ExtraParameter { get; set; }
    }
}