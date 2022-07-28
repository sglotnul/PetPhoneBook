namespace PetPhoneBook
{
    public class PhoneNumber
    {
        public string Prefix { get; init; }
        public string Number { get; init; }
        public PhoneNumber(string prefix, string number)
        {
            if (!Int64.TryParse(number, out _) || !int.TryParse(prefix, out int p)) throw new ValidationException("invalid phone number");
            if (p < 1 || p > 1900 || number.Length != 10) throw new ValidationException("invalid phone number");
            Prefix = prefix;
            Number = number;
        }

        public PhoneNumber(string phone)
        {
            Prefix = phone.Substring(0, phone.Length - 10);
            Number = phone.Substring(phone.Length - 10);
        }
        public override string ToString()
        {
            return Prefix + Number;
        }
    }
}
