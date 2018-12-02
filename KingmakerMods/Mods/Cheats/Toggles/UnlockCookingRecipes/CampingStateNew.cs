using System.Linq;
using Kingmaker.Controllers.Rest.Cooking;
using Kingmaker.Controllers.Rest.State;
using KingmakerMods.Helpers;
using Patchwork;
using UnityEngine;

namespace KingmakerMods.Mods.Cheats.Toggles.UnlockCookingRecipes
{
	[ModifiesType]
	public class CampingStateNew : CampingState
	{
		#region DUPLICATES
		[NewMember]
		[DuplicatesBody("UnlockRecipe")]
		public void source_UnlockRecipe(BlueprintCookingRecipe recipe)
		{
			throw new DeadEndException("source_UnlockRecipe");
		}
		#endregion

		[NewMember]
		private static bool _unlockedAllRecipes;

		[NewMember]
		public void UnlockAllRecipes()
		{
			BlueprintCookingRecipe[] recipeBlueprints = Resources.FindObjectsOfTypeAll<BlueprintCookingRecipe>();

			foreach (BlueprintCookingRecipe cookingRecipe in recipeBlueprints.Where(r => !this.KnownRecipes.Contains(r)))
			{
				this.KnownRecipes.Add(cookingRecipe);
			}
		}

		[ModifiesMember("UnlockRecipe")]
		public void mod_UnlockRecipe(BlueprintCookingRecipe recipe)
		{
			if (recipe == null)
			{
				return;
			}

			if (!KingmakerPatchSettings.Cheats.UnlockCookingRecipes)
			{
				this.source_UnlockRecipe(recipe);
				return;
			}

			if (_unlockedAllRecipes)
			{
				return;
			}

			this.UnlockAllRecipes();
			_unlockedAllRecipes = true;
		}
	}
}
