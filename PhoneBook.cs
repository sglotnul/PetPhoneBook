using System.Data.SqlClient;

namespace PetPhoneBook
{
    internal class PhoneBook
    {
        DbConnection connection;
        private List<T> Fetch<T>(string query, Func<SqlDataReader,T> zip)
        {
            connection.Open();
            SqlDataReader reader = connection.Fetch(query);
            List<T> results = new List<T>();
            while (reader.Read())
            {
                results.Add(zip(reader));
            }
            reader.Close();
            connection.Close();
            return results;
        }

        private void Execute(string query)
        {
            connection.Open();
            try 
            {
                connection.Execute(query);
            }
            finally
            {
                connection.Close();
            }         
        }

        public PhoneBook()
        {
            string? connectionString = Environment.GetEnvironmentVariable("MyDbConnectionString", EnvironmentVariableTarget.User);
            if (connectionString == null)
            {
                throw new ArgumentNullException("unable to connect to db");
            }
            connection = new DbConnection(connectionString);
        }

        public void Insert(Entry entry)
        {
            string query = string.Format("insert into entries values ('{0}','{1}',{2},{3});", 
                entry.Phone.ToString(),
                entry.Name,
                (!string.IsNullOrEmpty(entry.Surname) ? $"'{entry.Surname}'" : "null"),
                (entry.Email != null ? $"'{entry.Email.ToString()}'" : "null")
            );

            Execute(query);
        }

        public void Update(string phone, Entry entry)
        {
            string query = string.Format("update entries set phone='{0}',name='{1}',surname={2},email={3} where phone='{4}';",
                entry.Phone.ToString(),
                entry.Name,
                (!string.IsNullOrEmpty(entry.Surname) ? $"'{entry.Surname}'" : "null"),
                (entry.Email != null ? $"'{entry.Email.ToString()}'" : "null"),
                phone
            );

            Execute(query);
        }

        public void Delete(string phone)
        {
            string query = $"delete from entries where phone='{phone}';";

            Execute(query);
        }

        public List<PreviewEntry> GetPreviews()
        {
            string query = $"select phone, name, surname from entries;";
            var zip = (SqlDataReader reader) => new PreviewEntry(reader[0].ToString(), reader[1].ToString() + ' ' + reader[2].ToString());
            return Fetch(query, zip);
        }

        public List<PreviewEntry> GetPreviews(string search_string)
        {
            string query = $"select phone, name, surname from entries where concat(name, ' ', surname) like '%{string.Join('%', search_string.Split(' '))}%';";
            var zip = (SqlDataReader reader) => new PreviewEntry(reader[0].ToString(), reader[1].ToString() + ' ' + reader[2].ToString());
            return Fetch(query, zip);
        }

        public Entry? getByPhoneNumber(string phone)
        {
            string query = $"select * from entries where phone='{phone}';";

            var zip = (SqlDataReader reader) => new Entry(
                new PhoneNumber(reader[0].ToString()),
                reader[1].ToString(),
                reader[2].ToString(),
                !string.IsNullOrEmpty(reader[3].ToString()) ? new EmailAddress(reader[3].ToString()) : null
            );
            List<Entry> results = Fetch(query, zip);
            if (results.Count == 0) return null;
            return results[0];
        }
    }
}
