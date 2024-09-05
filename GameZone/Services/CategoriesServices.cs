
using GameZone.ViewModel;

namespace GameZone.Services
{
	public class CategoriesServices : ICategoriesServices
	{
		public readonly ApplicationDbContext Context;

		public CategoriesServices(ApplicationDbContext context )
		{
			Context = context;
		}

        public IEnumerable<SelectListItem> GetSelectList()
		{
		 var result=Context.Categories
				.Select(c=>new SelectListItem { Value =c.Id.ToString(),Text = c.Name})
				.OrderBy(c=>c.Text)
				.AsNoTracking()
				.ToList();

			return result;
		}
	}
}
