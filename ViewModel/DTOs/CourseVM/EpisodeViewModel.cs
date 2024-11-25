namespace itfco.ViewModel.DTOs.ProductVM
{
	public class EpisodeListViewModel
	{
		public int EpisodeId { get; set; }
		public string EpisodeTitle { get; set; }
		public TimeSpan EpisodeTime { get; set; }
		public bool IsFree { get; set; }
	}
	public class FilterEpisodeListViewModel
	{
		public int ProductId { get; set; }
		public string Title { get; set; }
		public TimeSpan EpisodeTimeFrom { get; set; }
		public TimeSpan EpisodeTimeTo { get; set; }
		public Entity.Enum.Coust EpisodeStatus { get; set; }
	}
}
