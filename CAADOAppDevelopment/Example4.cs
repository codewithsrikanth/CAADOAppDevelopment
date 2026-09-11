using Microsoft.Data.SqlClient;

namespace CAADOAppDevelopment
{
    class Example4
    {
        static string connStr = "server=.;database=CompanyDB;integrated security=true;TrustServerCertificate=True;";
        static void Main(string[] args)
        {
            Console.WriteLine("CRUD Operations");
            Console.WriteLine("1. Insert\n2. Update\n3. Delete\n 4.GetEmpByNo");
            int choice = ReadInt("Enter your choice (1-4): ");

            string spName = "";
            SqlParameter[] parameters = null;

            switch (choice)
            {
                case 1:
                    spName = "sp_InsertEmployee";
                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@Eno",ReadInt("Enter Eno: ")),
                        new SqlParameter("@Ename",ReadString("Enter Ename: ")),
                        new SqlParameter("@Job",ReadString("Enter Job: ")),
                        new SqlParameter("@Salary",ReadDouble("Enter Salary: ")),
                        new SqlParameter("@Dname",ReadString("Enter Dname: "))
                    };
                    ExecuteDML(spName, parameters);
                    break;
                case 2:
                    spName = "sp_UpdateRec";
                    parameters = new SqlParameter[]
                    {
                        new SqlParameter("@Eno",ReadInt("Enter Eno: ")),
                        new SqlParameter("@Ename",ReadString("Enter Ename: ")),
                        new SqlParameter("@Job",ReadString("Enter Job: ")),
                        new SqlParameter("@Salary",ReadDouble("Enter Salary: ")),
                        new SqlParameter("@Dname",ReadString("Enter Dname: "))
                    };
                    ExecuteDML(spName, parameters);
                    break;
                default:
                    Console.WriteLine("Invalid Choice");
                    break;
            }

        }

        //Read the Input
        static int ReadInt(string prompt)
        {
            Console.WriteLine(prompt);
            return Convert.ToInt32(Console.ReadLine());
        }
        static string ReadString(string prompt)
        {
            Console.WriteLine(prompt);
            return Console.ReadLine();
        }
        static double ReadDouble(string prompt)
        {
            Console.WriteLine(prompt);
            return Convert.ToDouble(Console.ReadLine());
        }

        static void ExecuteDML(string spName, SqlParameter[] parameters)
        {
            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand(spName, con);
            cmd.CommandType = System.Data.CommandType.StoredProcedure; //Only when we are sending SP
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }
            try
            {
                con.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                Console.WriteLine(rowsAffected > 0 ? rowsAffected+" row(s) affected.":"No records affected. Check the Eno is exists");
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: "+ex.Message);
            }
            finally
            {
                con.Close();
            }
        }
    }
}
