namespace Przychodnia.PortalWWW.Helpers
{
	public class DoctorParameter
	{
        public int DoctorId { get; set; }
        // Liczba dni licząc od dziś, które wyznaczą datę, od której będą wczytane dane.
        public int StartDay { get; set; }
    }
}
