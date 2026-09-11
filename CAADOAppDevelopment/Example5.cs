using Microsoft.Data.SqlClient;
using System.Data;

namespace CAADOAppDevelopment
{
    class Example5
    {
        static string connStr = "server=.;database=CompanyDB;integrated security=true;TrustServerCertificate=True;";
        static void Main(string[] args)
        {
            DataTable dt = GetEmployees();
            foreach (DataRow row in dt.Rows) 
            {
                Console.WriteLine($"{row["Eno"]}\t{row["Ename"]}\t{row["Job"]}\t{row["Salary"]}\t{row["Dname"]}");
            }
        }

        static DataTable GetEmployees()
        {
            string query = "select * from Employee";
            SqlDataAdapter da = new SqlDataAdapter(query, new SqlConnection(connStr));
            DataTable dt = new DataTable();
            da.Fill(dt);
            return dt;
        }
    }
}
