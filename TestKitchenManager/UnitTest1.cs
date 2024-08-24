using KitchenManager.Models;
using KitchenManager.Controllers;

namespace TestKitchenManager
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public async Task TestMethod1()
        {
            RecipeManager manager = new RecipeManager();
            Recipe newRecipe = await manager.GetRandomRecipe();

            Assert.AreEqual(newRecipe, manager.CurrentRecipe);
        }
    }
}