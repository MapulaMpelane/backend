namespace Hospital.Hospital.Models
{
    public class Tracking
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public string AmbulanceLocation { get; set; }

        public Tracking() { }

        public Tracking(string patientId, string ambulanceLocation)
        {
            PatientId = patientId;
            AmbulanceLocation = ambulanceLocation;
        }
    }
}