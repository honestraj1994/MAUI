using MvvmHelpers;
using Newtonsoft.Json;
using Recipes.BusinessLayer;
using Recipes.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Recipes.ViewModels
{
    public class RecipeListViewModel : BaseViewModel
    {

        #region OnProperty

        private ObservableCollection<Meals> allRecipeList;
        public ObservableCollection<Meals> AllRecipeList
        {
            get { return allRecipeList; }
            set { allRecipeList = value; OnPropertyChanged(); }
        }

        #endregion

        #region Constructor

        public RecipeListViewModel()
        {

            AllRecipeList = new ObservableCollection<Meals>();

            LoadRecipeList();

        }

        #endregion


        #region Method

        public async Task LoadRecipeList()
        {
            try
            {
                var Response = await new RecipeServices().GetRecipeListData();

                if (Response != null)
                {
                    RecipeListModel recipeListModel = JsonConvert.DeserializeObject<RecipeListModel>(Response);

                    if (recipeListModel.meals.Count != 0)
                    {
                        allRecipeList = new ObservableCollection<Meals>(recipeListModel.meals);
                    }
                    else
                    {

                    }
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
            }


        }

        #endregion




    }
}
