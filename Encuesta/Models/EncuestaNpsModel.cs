namespace Encuesta.Models
{
    public class EncuestaNpsModel
    {
        public int Id { get; set; }
        public int AttetionId { get; set; }
        public int PatientId { get; set; }
        public int DoctorId { get; set; }
        public DateTime Date { get; set; }
        public int NpsScore { get; set; }
        public string? Comment { get; set; }
        public int IaScore { get; set; }
        public int Magnitud { get; set; }
    }
}
