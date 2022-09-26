namespace WebGUI.Models
{
    public class BankingUser
    {
        public int accNo { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public int pin { get; set; }
        public int balance { get; set; }
        public byte[] pictureData { get; set; }

    }
}
