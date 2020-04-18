using System;
using System.Data.SqlClient;

namespace MSSql
{
   class Program
    {
        static void Main(string[] args)
        {
            Person person = new Person();
        look:
            Console.WriteLine();
            int n = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Information about Employees");
            switch (n)
            {
                case 1:
                    person.GetPerson();
                    break;
                case 2:
                    Console.Write("Input ID of Employee: ");
                    int Id = Convert.ToInt32(Console.ReadLine());
                    person.GetPersonById(Id);
                    break;
                case 3:
                    Console.Write("LastName of Employee:");
                    string lname = Console.ReadLine();
                    Console.Write("FirstName of Employee:");
                    string fname = Console.ReadLine();
                    Console.Write("MiddleName of Employee:");
                    string Mname = Console.ReadLine();
                    Console.WriteLine("BirthDate of Employee:");
                    DateTime birthd = DateTime.Now;
                    person.PutInfoEmployees(lname,fname,Mname,birthd);
                    break;
                case 4:
                    Console.Write("ID of an Employee: ");
                    int id = Convert.ToInt32(Console.ReadLine());
                    person.DeletePersonInfo(id);
                    break;
                case 5:
                    Console.WriteLine("Please enter an ID to update: ");
                    int ID = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("Please enter LastName:");
                    string LastName = Console.ReadLine();
                    Console.WriteLine("Please enter FirsName:");
                    string FirstName = Console.ReadLine();
                    Console.WriteLine("Please enter MiddleName:");
                    string MiddleName = Console.ReadLine();
                    Console.WriteLine("Please enter a BirthDate:");
                    DateTime BirthDate = Convert.ToDateTime(Console.ReadLine());
                    person.UpdatePerson(ID, LastName, FirstName, MiddleName, BirthDate);
                    break;
                default:
                    Console.Write("Please out of range! Choose in the interval 1-5");
                    break;
            }
            goto look;
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
