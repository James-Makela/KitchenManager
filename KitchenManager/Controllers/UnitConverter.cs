namespace KitchenManager.Controllers
{
    public static class UnitConverter
    {
        public const decimal KgOrLitre = 1000;
        public const decimal Pound = 453.592m;
        public const decimal Gallon = 3785.41m;

        public static readonly string[] Liquids = ["ml", "millilitres", "pint", "quart", "fl oz", "fluid ounce", "gallon"];

        public static decimal GetOunces(decimal grams)
        {
            return grams * 0.03527392m;
        }

        public static decimal GetPounds(decimal grams)
        {
            return grams * 0.00220462m;
        }

        public static decimal GetGallons(decimal grams)
        {
            return grams / Gallon;
        }

        public static Tuple<decimal, string> ConvertSolidToMetric(decimal grams)
        {
            if (grams < 10)
            {
                return Tuple.Create<decimal, string>(grams, "g");
            }
            if (grams < KgOrLitre)
            {
                return Tuple.Create<decimal, string>(Math.Round(grams, 0), "g");
            }
            else
            {
                return Tuple.Create<decimal, string>((grams / 1000), "kg");
            }
        }

        public static Tuple<decimal, string> ConvertSolidToImperial(decimal grams)
        {
            // If less than a pound, return in oz
            if (grams < Pound)
            {
                return Tuple.Create<decimal, string>(GetOunces(grams), "oz");
            }
            // Otherwise return in pounds
            return Tuple.Create<decimal, string>(GetPounds(grams), "pounds");
        }

        public static Tuple<decimal, string> ConvertLiquidToMetric(decimal grams)
        {
            // If less than a litre, return in ml
            if (grams < KgOrLitre)
            {
                return Tuple.Create<decimal, string>(grams, "ml");
            }
            // Otherwise return in litres
            return Tuple.Create<decimal, string>((grams / 1000), "L");
        }

        public static Tuple<decimal, string> ConvertLiquidToImperial(decimal grams)
        {
            if (grams < Gallon)
            {
                decimal flOz = GetOunces(grams);
                if (flOz < 10)
                {
                    return Tuple.Create<decimal, string>(flOz, "fl oz");
                }
                return Tuple.Create<decimal, string>(Math.Round(flOz, 0), "fl oz");
            }
            else return Tuple.Create<decimal, string>(Math.Round(GetGallons(grams), 1), "gal");
        }

        public static Tuple<decimal, string> Convert(decimal grams, string oldMeasurement)
        {
            string unitSystem = PreferencesManager.GetUnit() ? "metric" : "imperial";
            if (unitSystem == "metric")
            {
                // Do metric stuff
                if (Liquids.Contains(oldMeasurement))
                {
                    return ConvertLiquidToMetric(grams);
                }
                return ConvertSolidToMetric(grams);
            }
            else
            {
                // Do imperial stuff
                if (Liquids.Contains(oldMeasurement))
                {
                    return ConvertLiquidToImperial(grams);
                }
                return ConvertSolidToImperial(grams);
            }
        }
    }
}