namespace Hospital.Hospital.Models
{
    public class Appointment
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public DateTime AppointmentDateTime { get; set; }
        public bool IsEmergency { get; set; }
        public string? Notes { get; set; }

        public Appointment() { }

        public Appointment(string patientId, string doctorId, DateTime appointmentDateTime, bool isEmergency, string? notes)
        {
            PatientId = patientId;
            DoctorId = doctorId;
            AppointmentDateTime = appointmentDateTime;
            IsEmergency = isEmergency;
            Notes = notes;
        }
    }
}