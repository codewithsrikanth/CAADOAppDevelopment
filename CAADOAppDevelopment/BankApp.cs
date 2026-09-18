using Microsoft.Data.SqlClient;
using System.Data;

namespace CAADOAppDevelopment
{
    class BankApp
    {
        static string connStr = "server=.;database=BankDB;integrated security=true;TrustServerCertificate=True;";
        static void Main(string[] args)
        {
            while (true)
            {
                Console.WriteLine("\n========== BANK MENU ==========");
                Console.WriteLine("1. Create Account");
                Console.WriteLine("2. Deposit");
                Console.WriteLine("3. Withdraw");
                Console.WriteLine("4. Transfer Amount");
                Console.WriteLine("5. Check Balance");
                Console.WriteLine("6. View Account Details");
                Console.WriteLine("7. View All Accounts");
                Console.WriteLine("8. View Transaction History");
                Console.WriteLine("9. Exit");
                int choice = ReadInt("Enter your choice (1-9): ");

                switch (choice)
                {
                    case 1: CreateAccount(); break;
                    case 2: Deposit(); break;
                    case 3: Withdraw(); break;
                    case 4: TransferAmount(); break;
                    case 5: CheckBalance(); break;
                    case 6: ViewAccount(); break;
                    case 7: ViewAllAccounts(); break;
                    case 8: ViewTransactionHistory(); break;
                    case 9: return;
                    default: Console.WriteLine("Invalid choice."); break;
                }
            }
        }
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
        static decimal ReadDecimal(string prompt)
        {
            Console.WriteLine(prompt);
            return Convert.ToDecimal(Console.ReadLine());
        }
        static bool ExecuteSP(string spName, SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(spName, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null) cmd.Parameters.AddRange(parameters);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                    return false;
                }
            }
        }
        static void ExecuteReaderSP(string spName, SqlParameter[] parameters)
        {
            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand(spName, con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                if (parameters != null) 
                    cmd.Parameters.AddRange(parameters);
                try
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (!reader.HasRows)
                        {
                            Console.WriteLine("No records found.");
                            return;
                        }
                        // Print column headers once
                        for (int i = 0; i < reader.FieldCount; i++)
                            Console.Write(reader.GetName(i) + "\t");
                        Console.WriteLine();
                        while (reader.Read())
                        {
                            for (int i = 0; i < reader.FieldCount; i++)
                                Console.Write(reader[i] + "\t");
                            Console.WriteLine();
                        }
                    }
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        static void CreateAccount()
        {
            string name = ReadString("Enter Customer Name: ");
            string type = ReadString("Enter Account Type (Savings/Current): ");
            decimal openingBalance = ReadDecimal("Enter Opening Balance: ");

            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("sp_CreateAccount", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@CustomerName", name);
                cmd.Parameters.AddWithValue("@AccType", type);
                cmd.Parameters.AddWithValue("@Balance", openingBalance);

                SqlParameter outParam = new SqlParameter("@NewAccNo", SqlDbType.Int);
                outParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParam);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    Console.WriteLine("Account created successfully! New AccNo = " + outParam.Value);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }                
            }
        }
        static void Deposit()
        {
            int accNo = ReadInt("Enter Account No: ");
            decimal amount = ReadDecimal("Enter Deposit Amount: ");

            SqlParameter[] parameters =
            {
            new SqlParameter("@AccNo", accNo),
            new SqlParameter("@Amount", amount)
            };

            if (ExecuteSP("sp_Deposit", parameters))
                Console.WriteLine("Deposit successful.");
        }
        static void Withdraw()
        {
            int accNo = ReadInt("Enter Account No: ");
            decimal amount = ReadDecimal("Enter Withdrawal Amount: ");

            SqlParameter[] parameters =
            {
            new SqlParameter("@AccNo", accNo),
            new SqlParameter("@Amount", amount)
            };

            if (ExecuteSP("sp_Withdraw", parameters))
                Console.WriteLine("Withdrawal successful.");
        }
        static void TransferAmount()
        {
            int fromAcc = ReadInt("Enter Source Account No: ");
            int toAcc = ReadInt("Enter Destination Account No: ");
            decimal amount = ReadDecimal("Enter Amount to Transfer: ");

            SqlParameter[] parameters =
            {
            new SqlParameter("@FromAccNo", fromAcc),
            new SqlParameter("@ToAccNo", toAcc),
            new SqlParameter("@Amount", amount)
        };

            if (ExecuteSP("sp_TransferAmount", parameters))
                Console.WriteLine("Transfer successful.");
        }
        static void CheckBalance()
        {
            int accNo = ReadInt("Enter Account No: ");

            using (SqlConnection con = new SqlConnection(connStr))
            using (SqlCommand cmd = new SqlCommand("sp_GetBalance", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@AccNo", accNo);

                SqlParameter outParam = new SqlParameter("@Balance", SqlDbType.Decimal);
                outParam.Precision = 12;
                outParam.Scale = 2;
                outParam.Direction = ParameterDirection.Output;
                cmd.Parameters.Add(outParam);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();

                    if (outParam.Value == DBNull.Value)
                        Console.WriteLine("Account not found.");
                    else
                        Console.WriteLine("Balance for AccNo " + accNo + " = " + outParam.Value);
                }
                catch (SqlException ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
        }
        static void ViewAccount()
        {
            int accNo = ReadInt("Enter Account No: ");
            SqlParameter[] parameters = { new SqlParameter("@AccNo", accNo) };
            ExecuteReaderSP("sp_GetAccountByNo", parameters);
        }
        static void ViewAllAccounts()
        {
            ExecuteReaderSP("sp_GetAllAccounts", null);
        }
        static void ViewTransactionHistory()
        {
            int accNo = ReadInt("Enter Account No: ");
            SqlParameter[] parameters = { new SqlParameter("@AccNo", accNo) };
            ExecuteReaderSP("sp_GetTransactionHistory", parameters);
        }
    }
}
