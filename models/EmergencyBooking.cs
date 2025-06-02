namespace Hospital.Hospital.Models
{
    public class EmergencyBooking
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public string? ResponderId { get; set; }
        public string Location { get; set; }
        public string? AmbulanceLocation { get; set; }
        public bool Confirmed { get; set; }
        public DateTime Date { get; set; }

        public EmergencyBooking()
        {
            Date = DateTime.Now;
        }

        public EmergencyBooking(string patientId, string? responderId, string location)
        {
            PatientId = patientId;
            ResponderId = responderId;
            Location = location;
            Confirmed = false;
            Date = DateTime.Now;
        }
    }
}