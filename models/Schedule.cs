namespace Hospital.Hospital.Models
{
    public class Schedule
    {
        public int Id { get; set; }
        public string DoctorId { get; set; }
        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }

        public Schedule() { }

        public Schedule(string doctorId, DateTime availableFrom, DateTime availableTo)
        {
            DoctorId = doctorId;
            AvailableFrom = availableFrom;
            AvailableTo = availableTo;
        }
    }
}