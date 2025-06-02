namespace Hospital.Hospital.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string? MedicalHistory { get; set; }
        public string Password { get; set; }

        public Patient() { }

        public Patient(string name, string email, string phone, string? medicalHistory, string password)
        {
            Name = name;
            Email = email;
            Phone = phone;
            MedicalHistory = medicalHistory;
            Password = password;
        }
    }
}