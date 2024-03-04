namespace Clarity.Web.Service.Helpers
{
    public class IndianSalaryConverter
    {
        private static string[] ones = {
        "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
        "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
    };

        private static string[] tens = {
        "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
    };

        public static string ConvertToWords(double salary)
        {
            if (salary == 0)
                return "Zero";

            long intPart = (long)Math.Floor(salary);
            int decPart = (int)Math.Round((salary - intPart) * 100);

            string intWords = ConvertToWords(intPart);
            string decWords = ConvertToWords(decPart);

            if (string.IsNullOrEmpty(intWords))
                return $"{decWords} paise only";

            if (string.IsNullOrEmpty(decWords))
                return $"{intWords} rupees only";

            return $"{intWords} rupees and {decWords} paise only";
        }

        private static string ConvertToWords(long number)
        {
            if (number == 0)
                return "";

            if (number < 20)
                return ones[number];

            if (number < 100)
                return $"{tens[number / 10]} {ones[number % 10]}".Trim();

            return $"{ones[number / 100]} Hundred {ConvertToWords(number % 100)}".Trim();
        }
        public static string ConvertDecimalToWords(decimal number)
        {
            long intPart = (long)Math.Floor(number);
            int decPart = (int)Math.Round((number - intPart) * 100);

            string intWords = ConvertToWords(intPart);
            string decWords = ConvertToWords(decPart);

            if (string.IsNullOrEmpty(intWords))
                return $"{decWords} paise";

            if (string.IsNullOrEmpty(decWords))
                return $"{intWords} rupees";

            return $"{intWords} rupees and {decWords} paise";
        }
    }
}
