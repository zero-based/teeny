namespace Teeny.Core.Parse.Validation
{
    internal class NullFieldError : IGuardError
    {
        public string FieldName { get; set; }
    }
}