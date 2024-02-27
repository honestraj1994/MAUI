using Recipes.ViewModels;

namespace Recipes.Views;

public partial class RecipeListPage : ContentPage
{
	public RecipeListPage()
	{
		InitializeComponent();

        BindingContext = new RecipeListViewModel();

		Title = "Recipes";
    }
}