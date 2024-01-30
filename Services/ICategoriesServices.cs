namespace Games_Website.Services
{
    public interface ICategoriesServices
    {
        IEnumerable<SelectListItem> GetSelectList();
    }
}