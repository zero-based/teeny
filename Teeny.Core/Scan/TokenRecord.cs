namespace Teeny.Core.Scan
{
    public class TokenRecord
    {
        public string Lexeme { get; set; }
        public Token Token { get; set; }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == GetType() && Equals((TokenRecord) obj);
        }

        public override int GetHashCode()
        {
            // ReSharper disable once BaseObjectGetHashCodeCallInGetHashCode
            return base.GetHashCode();
        }

        protected bool Equals(TokenRecord other)
        {
            return Lexeme == other.Lexeme && Token == other.Token;
        }
    }
}