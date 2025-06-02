namespace Hospital.Hospital.Models
{
    public class Report
    {
        public int Id { get; set; }
        public string PatientId { get; set; }
        public string DoctorId { get; set; }
        public string ReportDetails { get; set; }
        public DateTime CreatedOn { get; set; }

        public Report() { }

        public Report( string patientId, string doctorId, string reportDetails, DateTime createdOn)
        {
            
            PatientId = patientId;
            DoctorId = doctorId;
            ReportDetails = reportDetails;
            CreatedOn = createdOn;
        }
    }
}