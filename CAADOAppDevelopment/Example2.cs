using Microsoft.Data.SqlClient;

namespace CAADOAppDevelopment
{
    class Example2
    {
        static string connStr = "server=.;database=CompanyDB;integrated security=true;TrustServerCertificate=True;";
        static void Main(string[] args)
        {
            Console.WriteLine("Employee Table Operations: ");
            Console.WriteLine("1. Insert");
            Console.WriteLine("2. Update");
            Console.WriteLine("3. Delete");
            Console.WriteLine("Enter your choice");
            int choice = Convert.ToInt16(Console.ReadLine());

            SqlConnection con = new SqlConnection(connStr);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;

            try
            {
                switch (choice)
                {
                    case 1: //Insert
                        Console.Write("Enter Eno: ");
                        int eno = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Ename: ");
                        string? ename = Console.ReadLine();
                        Console.Write("Enter Job: ");
                        string? job = Console.ReadLine();
                        Console.Write("Enter Salary: ");
                        double sal = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Enter Dname: ");
                        string? dname = Console.ReadLine();

                        cmd.CommandText = "insert into Employee Values(@Eno,@Ename,@Job,@Salary,@DName)";
                        cmd.Parameters.AddWithValue("@Eno", eno);
                        cmd.Parameters.AddWithValue("@Ename", ename);
                        cmd.Parameters.AddWithValue("@Job", job);
                        cmd.Parameters.AddWithValue("@Salary", sal);
                        cmd.Parameters.AddWithValue("@DName", dname);
                        break;
                    case 2:
                        Console.Write("Enter Eno: ");
                        int eno1 = Convert.ToInt32(Console.ReadLine());
                        Console.Write("Enter Ename: ");
                        string? ename1 = Console.ReadLine();
                        Console.Write("Enter Job: ");
                        string? job1 = Console.ReadLine();
                        Console.Write("Enter Salary: ");
                        double sal1 = Convert.ToDouble(Console.ReadLine());
                        Console.Write("Enter Dname: ");
                        string? dname1 = Console.ReadLine();
                        cmd.CommandText = "update Employee set Ename=@Ename,Job=@Job,Salary=@Salary,Dname=@DName where Eno=@Eno";
                        cmd.Parameters.AddWithValue("@Eno", eno1);
                        cmd.Parameters.AddWithValue("@Ename", ename1);
                        cmd.Parameters.AddWithValue("@Job", job1);
                        cmd.Parameters.AddWithValue("@Salary", sal1);
                        cmd.Parameters.AddWithValue("@DName", dname1);
                        break;
                    case 3:
                        Console.Write("Enter Eno of employee to delete: ");
                        int delEno = Convert.ToInt32(Console.ReadLine());
                        cmd.CommandText = "Delete from Employee where Eno=@Eno";
                        cmd.Parameters.AddWithValue("@Eno", delEno);
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        return;
                }
                con.Open();
                int rows = cmd.ExecuteNonQuery();
                if(rows >0)
                    Console.WriteLine(rows+" Affected");
                else
                    Console.WriteLine("No Rows Affected");
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
