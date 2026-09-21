using ClibBankBLL;

namespace CABankPL
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create an account: ");

            Console.WriteLine("Enter Customer Name: ");
            string? custName = Console.ReadLine();

            Console.WriteLine("Enter Account Type(Savings/Current)");
            string? accType = Console.ReadLine();

            Console.WriteLine("Enter Opening Balance");
            decimal bal = Convert.ToDecimal(Console.ReadLine());

            AccountBLL obj = new AccountBLL();
            int accNo = obj.CreateAccount(custName, accType, bal);
            Console.WriteLine("Account created successfully! New Account No is: "+accNo);
        }
    }
}
