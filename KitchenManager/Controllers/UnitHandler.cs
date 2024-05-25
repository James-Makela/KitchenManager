namespace KitchenManager.Controllers
{
    public static class UnitHandler
    {
        public enum CostingUnits
        {
            kg = 1000,
            grams100 = 100,
            grams10 = 10,

            litre = 1000,
            mls100 = 100,
            mls10 = 10,

            ea = 1,
            doz = 12,
        }


        public static Dictionary<string, int> GetStockUnits()
        {
            return new Dictionary<string, int>
            {
                ["kg"] = 1000,
                ["gm"] = 1,
                ["litres"] = 1000,
                ["mls"] = 1,
                ["pieces"] = 1,
                ["doz"] = 12,
            };
        }

        public static Dictionary<string, int> GetCostUnits()
        {
            return new Dictionary<string, int>
            {
                ["kg"] = 1000,
                ["100gm"] = 100,
                ["10gm"] = 10,
                ["litre"] = 1000,
                ["100mls"] = 100,
                ["10mls"] = 10,
                ["ea"] = 1,
                ["doz"] = 12,
            };
        }
    }
}
