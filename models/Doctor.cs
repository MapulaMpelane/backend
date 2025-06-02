namespace Hospital.Hospital.Models
{
    public class Doctor
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Specialization { get; set; }

        public Doctor() { }

        public Doctor(string name, string email, string phone, string specialization)
        {
            Name = name;
            Email = email;
            Phone = phone;
            Specialization = specialization;
        }
    }
}