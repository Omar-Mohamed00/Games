namespace Games_Website.ViewModels
{
	public class CreateGameFormViewModel: GameFormViewModel
	{
		[AllowedExtentions(FileSettings.AllowedExtention),
			MaxFileSize(FileSettings.MaxFileSizeInBytes)]
		public IFormFile Cover { get; set; } = default!;
    }
}