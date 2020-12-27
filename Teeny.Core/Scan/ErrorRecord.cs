namespace Teeny.Core.Scan
{
    public class ErrorRecord
    {
        public string Lexeme { get; set; }
        public ErrorType ErrorType { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((ErrorRecord)obj);
        }

        public override int GetHashCode()
        {
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        }

        protected bool Equals(ErrorRecord other)
        {
            return Lexeme == other.Lexeme && ErrorType == other.ErrorType;
        }
    }
}
