using Kingmaker.Controllers.Rest.Cooking;
using KingmakerMods.Helpers;
using Patchwork;

namespace KingmakerMods.Mods.Cheats.Toggles.CookingRequiresNoIngredients
{
	[ModifiesType]
	public class BlueprintCookingRecipeNew : BlueprintCookingRecipe
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("CheckIngredients")]
		public bool source_CheckIngredients()
		{
			throw new DeadEndException("source_CheckIngredients");
		}

		[NewMember]
		[DuplicatesBody("SpendIngredients")]
		public void source_SpendIngredients()
		{
			throw new DeadEndException("source_SpendIngredients");
		}
		#endregion

		[ModifiesMember("CheckIngredients")]
		public bool mod_CheckIngredients()
		{
			// ReSharper disable once ConvertIfStatementToReturnStatement
			if (KingmakerPatchSettings.Cheats.CookingRequiresNoIngredients)
			{
				return true;
			}

			return this.source_CheckIngredients();
		}

		[ModifiesMember("SpendIngredients")]
		public void mod_SpendIngredients()
		{
			if (KingmakerPatchSettings.Cheats.CookingRequiresNoIngredients)
			{
				return;
			}

			this.source_SpendIngredients();
		}
	}
}
