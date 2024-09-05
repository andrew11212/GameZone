namespace GameZone.Models
{
	public class Device
	{
		public int Id { get; set; }

		public string Name { get; set; }=string.Empty;

		public string Icon { get; set; }=string.Empty;

		public ICollection<GameDevise> Devises { get; set; } = new List<GameDevise>();
	}
}
