namespace Hospital.Hospital.Models
{
    public class EmergencyResponder
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public EmergencyResponder() { }

        public EmergencyResponder(string name, string email, string phone)
        {
            Name = name;
            Email = email;
            Phone = phone;
        }
    }
}