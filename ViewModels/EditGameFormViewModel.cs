using Games_Website.Attributes;

namespace Games_Website.ViewModels
{
	public class EditGameFormViewModel: GameFormViewModel
	{
        public int Id { get; set; }
        public string? CurrentCover{ get; set; }

        [AllowedExtentions(FileSettings.AllowedExtention),
		MaxFileSize(FileSettings.MaxFileSizeInBytes)]
		public IFormFile? Cover { get; set; } = default!;
	}
}
