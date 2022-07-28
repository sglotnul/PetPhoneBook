namespace PetPhoneBook
{
    public class EmailAddress
    {
        private string[] domens;
        public string Address { get; init; }
        public string Domen { get => string.Join('.', domens); } 
        public EmailAddress(string address, string[] domens)
        {
            if (address.Length == 0 || domens.Length < 2) throw new ValidationException("invalid email");
            foreach(char c in address)
            {
                if(!char.IsLetterOrDigit(c)) throw new ValidationException("invalid email");
            }
            this.domens = domens;
            Address = address;
        }
        public EmailAddress(string address, string domens) : this(address, domens.Split('.')) { }
        public EmailAddress(string email) : this(email.Split('@')[0], email.Split('@')[1]) { }
        public override string ToString()
        {
            return Address + '@' + Domen;
        }
    }
}
