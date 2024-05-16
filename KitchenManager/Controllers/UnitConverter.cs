namespace KitchenManager.Controllers
{
    public static class UnitConverter
    {
        public const decimal Kg = 1000;
        public const decimal Pound = 453.592m;

        public static decimal GetOunces(decimal grams)
        {
            return grams * 0.03527392m;
        }

        public static decimal GetPounds(decimal grams)
        {
            return grams * 0.00220462m;
        }

        public static Tuple<decimal, string> ConvertToMetric(decimal grams)
        {
            if (grams < Kg)
            {
                return Tuple.Create<decimal, string>(grams, "g");
            }
            else
            {
                return Tuple.Create<decimal, string>((grams / 1000), "kg");
            }
        }

        public static Tuple<decimal, string> ConvertToImperial(decimal grams)
        {
            // If less than a pound, return in oz
            if (grams < Pound)
            {
                return Tuple.Create<decimal, string>(GetOunces(grams), "oz");
            }
            // If greater than a pound, return in pounds
            else
            {
                return Tuple.Create<decimal, string>(GetPounds(grams), "pounds");
            }
        }
    }
}