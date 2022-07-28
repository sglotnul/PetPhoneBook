namespace PetPhoneBook
{
    public struct Entry
    {
        public PhoneNumber Phone { get; init; }
        public string Name { get; init; }
        public string Surname { get; init; } = "";
        public EmailAddress? Email { get; init; } = null;
        public Entry(PhoneNumber phone, string name)
        {
            Phone = phone;
            Name = name;
        }
        public Entry(PhoneNumber phone, string name, string surname="", EmailAddress? email=null)
        {
            Phone = phone;
            Name = name;
            Surname = surname;
            Email = email;
        }
    }
}
