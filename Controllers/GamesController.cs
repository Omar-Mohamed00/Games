namespace Games_Website.Controllers
{
    public class GamesController : Controller
    {
        private readonly ICategoriesServices _categoriesServices;
        private readonly IDevicesServices _deviceServices;
        private readonly IGamesServices _gamesServices;

        public GamesController(ICategoriesServices categoriesServices
            , IDevicesServices deviceServices
            , IGamesServices gamesServices)
        {
            _categoriesServices = categoriesServices;
            _deviceServices = deviceServices;
            _gamesServices = gamesServices;
        }

        public IActionResult Index()
        {
            var games = _gamesServices.GetAll();
            return View(games);
        }
        public IActionResult Details(int id)
        {
            var game = _gamesServices.GetById(id);
            if(game is null)
                return NotFound();
            return View(game);
        }

        [HttpGet] // Default
        public IActionResult Create()
        {
            CreateGameFormViewModel viewModel = new()
            {
                Categories = _categoriesServices.GetSelectList().Cast<SelectListItem>().ToList(),
                Devices = _deviceServices.GetSelectList().Cast<SelectListItem>().ToList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesServices.GetSelectList().Cast<SelectListItem>().ToList();
                model.Devices = _deviceServices.GetSelectList().Cast<SelectListItem>().ToList();
                return View(model);
            }

            await _gamesServices.Create(model);

            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
			var game = _gamesServices.GetById(id);
			if (game is null)
				return NotFound();

            EditGameFormViewModel viewModel = new()
            {
                Id = id,
                Name = game.Name,
                Description = game.Description,
                CategoryId = game.CategoryId,
                Categories = _categoriesServices.GetSelectList(),
                SelectDevices = game.Devices.Select(d => d.DeviceId).ToList(),
                Devices = _deviceServices.GetSelectList(),
                CurrentCover = game.Cover
            };
            return View(viewModel);
		}

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditGameFormViewModel model)
        {
            if (!ModelState.IsValid)
            {
                model.Categories = _categoriesServices.GetSelectList().Cast<SelectListItem>().ToList();
                model.Devices = _deviceServices.GetSelectList().Cast<SelectListItem>().ToList();
                return View(model);
            }

            var game = await _gamesServices.Update(model);
            if (game is null)
                return BadRequest();

            return RedirectToAction(nameof(Index));
        }
        [HttpDelete]
        public IActionResult Delete(int id)
        {
            var isDeleted = _gamesServices.Delete(id);

            return isDeleted ? Ok() : BadRequest();
        }
    }
}
