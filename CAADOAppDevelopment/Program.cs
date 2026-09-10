using Microsoft.Data.SqlClient;

namespace CAADOAppDevelopment
{
    class Program
    {
        static void Main(string[] args)
        {
            //Console.WriteLine("Enter Employee No: ");
            //int eno = Convert.ToInt16(Console.ReadLine());
            //string connstr = "server=.;database=CompanyDB;integrated security=true;TrustServerCertificate=True;";
            //SqlConnection conn = new SqlConnection(connstr);
            //string query = "delete from Employee where Eno=" + eno;
            //SqlCommand cmd = new SqlCommand(query, conn);
            //conn.Open();
            //int i = cmd.ExecuteNonQuery();
            //conn.Close();
            //if (i == 0)
            //{
            //    Console.WriteLine("No Record found");
            //}
            //else
            //{
            //    Console.WriteLine(i + " Record(s) Affected.");
            //}

            using(SqlConnection con = new SqlConnection("server=.;database=CompanyDB;integrated security=true;TrustServerCertificate=True;"))
            {
                Console.WriteLine("Enter Employee No: ");
                int eno = Convert.ToInt16(Console.ReadLine());
                using (SqlCommand cmd = new SqlCommand("delete from Employee where Eno="+eno, con))
                {
                    con.Open();
                    int i = cmd.ExecuteNonQuery();
                    con.Close();
                    if (i == 0)
                    {
                        Console.WriteLine("No Record found");
                    }
                    else
                    {
                        Console.WriteLine(i + " Record(s) Affected.");
                    }
                }
            }
        }
    }
}
