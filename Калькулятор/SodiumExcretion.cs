using System.Collections.Generic;

namespace Калькулятор
{
    public static class SodiumExcretion
    {
        private static Dictionary<string, string> information = new()
        {
            {
                "Преренальна олігурія",

                "Наприклад: Гіповолемія, захворювання серця, стеноз ниркової артерії " +
                "сепсис. Варто пам'ятати, що контраст-індуковане враження нирок часто виглядає як " +
                "преренальна олігурія."

            },
            {
                "Ренальна олігурія",
                "Наприклад: гострий інтерстиційний нефрить, гострий тубулярний некроз, гломерулонефрити."
            },
            {
                "Постренальна олігурія",
                "Наприклад, ДГПЗ, камінь сечового міхура, двобічна обструція сечоводу."
            },

        };
        public static string GetInfo(string key)
        {
            return information [key];
        }
        public static double CalculateFractExcretion(int SCr, int UNa, int SNa, int UCr)
        => 100 * ((double) SCr / 1000 * UNa) / (SNa * (double) UCr / 1000);
        public static (string, string) InterpretFractExcretion(double fractExcretion)
        {
            string oliguriaType, additionalInfo;

            //if (fractExcretion < 1)
            //    (oliguriaType, additionalInfo) = ("Преренальна олігурія", "");
            //else if ((fractExcretion >= 1) && (fractExcretion <= 4))
            //    (oliguriaType, additionalInfo) = ("Ренальна олігурія", "");
            //else
            //    (oliguriaType, additionalInfo) = ("Постренальна олігурія", "");

            //return (oliguriaType, additionalInfo);


            if (fractExcretion < 1)
                oliguriaType = "Преренальна олігурія";
            else if ((fractExcretion >= 1) && (fractExcretion <= 4))
                oliguriaType = "Ренальна олігурія";
            else
                oliguriaType = "Постренальна олігурія";

            return (oliguriaType, GetInfo(oliguriaType));
        }
    }
}
