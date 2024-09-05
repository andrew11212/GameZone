namespace GameZone.Models
{
	public class Game
	{
        public int id { get; set; }
		
		[MaxLength (length:250)]
		public string name { get; set; } =string.Empty;

		[MaxLength(length: 2500)]
		public string description { get; set; } =string.Empty;
		
		[MaxLength(length: 500)]
		public string cover { get; set; } =string.Empty;

		public int CategoryID { get; set; }
		public Category Category { get; set; } = default!;

		public ICollection<GameDevise> Devises { get; set; } =new List<GameDevise>();
    }
}
