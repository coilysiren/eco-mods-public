namespace Eco.Mods.TechTree
{
    using Eco.Gameplay.Components;
    using Eco.Gameplay.Items.Recipes;

    public partial class StreetlampRecipe : RecipeFamily
    {
        public void ModsPreInitialize()
        {
#pragma warning disable CS8602 // Dereference of a possibly null reference.
            List<IngredientElement> ingredients = this.Recipes.FirstOrDefault().Ingredients;
            foreach (IngredientElement ingredient in this.Recipes.FirstOrDefault().Ingredients)
#pragma warning restore CS8602 // Dereference of a possibly null reference.
            {
                if (ingredient.Item.GetType() == typeof(SteelBarItem))
                {
                    _ = ingredients.Remove(ingredient);
                    ingredients.Add(
                        new IngredientElement(
                            typeof(IronBarItem),
                            ingredient.Quantity.GetBaseValue,
                            true
                        )
                    );
                }
            }
        }

        public void ModsPostInitialize()
        {
            CraftingComponent.AddRecipe(tableType: typeof(AssemblyLineItem), recipe: this);
        }
    }
}
