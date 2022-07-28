using System.Data.SqlClient;

namespace PetPhoneBook
{
    internal class DbConnection
    {
        private SqlConnection connection;

        public DbConnection(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }

        public void Open() => connection.Open();

        public void Close() => connection.Close();

        public void Execute(string query)
        {
            SqlCommand command = new SqlCommand(query, connection);
            command.ExecuteNonQuery();
        }

        public void ExecuteMany(string[] queries)
        {
            var transaction = connection.BeginTransaction();
            var command = connection.CreateCommand();
            command.Transaction = transaction;

            try
            {
                foreach (string query in queries)
                {
                    command.CommandText = query;
                    command.ExecuteNonQuery();
                }
                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }

        public SqlDataReader Fetch(string query)
        {
            SqlCommand command = new SqlCommand(query, connection);
            return command.ExecuteReader();
        }
    }
}