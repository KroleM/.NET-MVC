using Przychodnia.Database.Data.Visits;

namespace Przychodnia.PortalWWW.ViewModels
{
    public class DoctorDisplayViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; } //?
        public DateTime BirthDate { get; set; } //?
		public double Price { get; set; }
		public string? Picture { get; set; }
        public string? PictureFormat { get; set; }
        public string LicenceNumber { get; set; }
        public int SpecializationId { get; set; }
        public Specialization Specialization { get; set; }

        public Dictionary<DateTime, List<VisitDateTime>>? Calendar { get; set; }    //DoctorDateTime?
        //dodatkowe listy z datami i czasami
    }
}
