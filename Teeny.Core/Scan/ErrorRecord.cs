namespace Teeny.Core.Scan
{
    public class ErrorRecord
    {
        public string Lexeme { get; set; }
        public Error Error { get; set; }

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

        private bool Equals(ErrorRecord other)
        {
            return Lexeme == other.Lexeme && Error == other.Error;
        }
    }
}
