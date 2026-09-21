namespace ClibBankDAL
{
    //Model/Entity class
    public class Account
    {
        public int AccNo { get; set; }
        public string? CustomerName { get; set; }
        public string? AccType { get; set; }
        public decimal Balance { get; set; }
    }
}
