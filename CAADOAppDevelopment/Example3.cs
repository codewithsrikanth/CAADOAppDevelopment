using Microsoft.Data.SqlClient;

namespace CAADOAppDevelopment
{
    class Example3
    {
        static void Main(string[] args)
        {
            GetEmpData();
            Console.WriteLine("Enter Eno: ");
            int eno = Convert.ToInt16(Console.ReadLine());
            ReadDataByEno(eno);
        }
        static void ReadDataByEno(int eno)
        {
            SqlConnection con = new SqlConnection("server=.;database=CompanyDB;integrated security=true;TrustServerCertificate=True;");
            string query = "select * from Employee where Eno="+eno;
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = null;
            con.Open();
            reader = cmd.ExecuteReader();
            if (reader.Read())
            {
                Console.WriteLine($"{reader["Eno"]}\t{reader["Ename"]}\t{reader["Job"]}\t{reader["Salary"]}\t{reader["Dname"]}\t");
            }
            else
            {
                Console.WriteLine("No Employee found with Eno: "+eno);
            }
            con.Close();
        }
        static void GetEmpData()
        {
            SqlConnection con = new SqlConnection("server=.;database=CompanyDB;integrated security=true;TrustServerCertificate=True;");
            string query = "select * from Employee";
            SqlCommand cmd = new SqlCommand(query, con);
            SqlDataReader reader = null;
            con.Open();
            reader = cmd.ExecuteReader();


            Console.WriteLine("{0,-6}{1,-20}{2,15}{3,10}{4,15}","Eno","Ename","Job","Salary","Dname");
            Console.WriteLine(new String('-',65));
            while (reader.Read())
            {
                int eno =Convert.ToInt32(reader["Eno"]);
                string ename = Convert.ToString(reader["Ename"]);
                string job = Convert.ToString(reader["Job"]);
                double sal =Convert.ToDouble(reader["Salary"]);
                string dname =Convert.ToString(reader["Dname"]);

                Console.WriteLine("{0,-6}{1,-20}{2,15}{3,10}{4,15}", eno,ename, job, sal,dname);
            }

            con.Close();
        }
    }
}
