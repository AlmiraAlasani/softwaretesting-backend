namespace PRO.DTOs
{
    public class AccountDTO
    {
        public int Id { get; set; }
        public decimal Balance { get; set; }
        public string Email { get; set; }

        public AccountDTO(decimal balance, string email)
        {

            Balance = balance;
            Email = email;
        }
    }
}
