namespace PetPhoneBook
{
    public struct PreviewEntry
    {
        public string Phone { get; init; }
        public string Name { get; init; }
        public PreviewEntry(string phone, string name)
        {
            Phone = phone;
            Name = name;
        }
    }
}
