using System;
using System.Data.SqlClient;

namespace MSSql
{
    class Program
    {
        static void Main(string[] args)
        {
            
        }
    }

    class SqlConnect
    {
        SqlConnection connection;
        public SqlConnect(string connectionString)
        {
            connection = new SqlConnection(connectionString);
        }
        public SqlConnect()
        {
            connection = new SqlConnection(@"Data Source = HAFIZI\SQLEXPRESS; Initial Catalog = MSSql; Integrated Security=true;");
        }
        public void Open()
        {
            connection.Open();
        }
        public void Close()
        {
            connection.Close();
        }
        public void Get(string str)
        {
            SqlCommand command = new SqlCommand(str, connection);
            SqlDataReader sdr = command.ExecuteReader();
            while (sdr.Read())
            {
                for (int i = 0; i < sdr.FieldCount; i++)
                    Console.Write(sdr[i].ToString() + "\t");
                Console.WriteLine();
            }
        }
        public void Insert(string str)
        {
            SqlCommand command = new SqlCommand(str, connection);
            int result = command.ExecuteNonQuery();
            if (result > 0)
                Console.WriteLine("Data is saved successfully\n");
            else
                Console.WriteLine("Error");
        }
        public void Delete(string str)
        {
            SqlCommand command = new SqlCommand(str, connection);
            int result = command.ExecuteNonQuery();
            if (result > 0)
                Console.WriteLine("Data is deleted successfully");
            else
                Console.WriteLine("Error");
        }
        public void Update(string str)
        {
            SqlCommand command = new SqlCommand(str, connection);
            int result = command.ExecuteNonQuery();
            if (result > 0)
                Console.WriteLine("Data is updated successfully");
            else
                Console.WriteLine("Error in updating");
        }
    }
    class Person
    {
        SqlConnect sqlConnect;
        public Person()
        {
            sqlConnect = new SqlConnect();
        }
        public void GetPerson()
        {
            string str = "SELECT * FROM Person";
            sqlConnect.Open();
            sqlConnect.Get(str);
            sqlConnect.Close();
        }
        public void GetPersonById(int Id)
        {
            string str = $"SELECT * FROM Person WHERE ID={Id}";
            sqlConnect.Open();
            sqlConnect.Get(str);
            sqlConnect.Close();
        }
        public void PutInfoEmployees(string LastName, string FirstName, string MiddleName, DateTime BirthDate)
        {
            string str = $"INSERT INTO Person ([LastName], [FirstName], [MiddleName], [BirthDate]) VALUES ('{LastName}','{FirstName}','{MiddleName}','{BirthDate}')";
            sqlConnect.Open();
            sqlConnect.Insert(str);
            sqlConnect.Close();
        }
        public void DeletePersonInfo(int Id)
        {
            string str = $"DELETE Person WHERE ID={Id}";
            sqlConnect.Open();
            sqlConnect.Delete(str);
            sqlConnect.Close();
        }
        public void UpdatePerson(int ID,string LastName, string FirstName, string MiddleName, DateTime BirthDate)
        {
            string insertSqlCommand = $"Update Person set FirstName='{LastName}',LastName='{FirstName}',MiddleName='{MiddleName}',BirthDate='{BirthDate}' where id={ID}";
            sqlConnect.Open();
            sqlConnect.Update(insertSqlCommand);
            sqlConnect.Close();
        }
    }
}
