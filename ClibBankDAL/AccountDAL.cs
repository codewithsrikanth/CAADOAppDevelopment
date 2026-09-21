using Microsoft.Data.SqlClient;
using System.Data;
using System.Xml.Linq;


namespace ClibBankDAL
{
    public class AccountDAL
    {
        static string connStr = "server=.;database=BankDB;integrated security=true;TrustServerCertificate=True;";

        public int CreateAccount(string custName,string accType, decimal balance)
        {
            using(SqlConnection con = new SqlConnection(connStr))
                using(SqlCommand cmd = new SqlCommand("sp_CreateAccount", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerName", custName);
                cmd.Parameters.AddWithValue("@AccType", accType);
                cmd.Parameters.AddWithValue("@Balance", balance);

                SqlParameter outParam = new SqlParameter("@NewAccNo", SqlDbType.Int);
                outParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParam);

                con.Open();
                cmd.ExecuteNonQuery();
                return (int)outParam.Value;
            }            
        }
    }
}
