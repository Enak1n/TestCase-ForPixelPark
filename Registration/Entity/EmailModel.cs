namespace Registration.Entity
{
    public class EmailModel
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Code { get; set; }
        public DateTime DateTime { get; set; } = DateTime.Now;

        public EmailModel(string email, string code)
        {
            Email = email;
            Code = code;
        }
    }
}
