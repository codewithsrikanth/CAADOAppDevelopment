using ClibBankDAL;

namespace ClibBankBLL
{
    public class AccountBLL
    {
        private AccountDAL dal = new AccountDAL();
        public int CreateAccount(string custName, string acctype, decimal balance)
        {
            if (string.IsNullOrWhiteSpace(custName))
                throw new ArgumentException("Customer Name Can't be Empty");
            if (acctype != "Savings" &&  acctype != "Current")
                throw new ArgumentException("Account type must be Savings or Current.");
            if (balance < 0)
                throw new ArgumentException("Opening balance cannot be negitive");
            if(acctype == "Savings" && balance < 1000)
                throw new ArgumentException("Minimum opening balance for savings account is 1000");

            return dal.CreateAccount(custName, acctype, balance);
        }
    }
}
