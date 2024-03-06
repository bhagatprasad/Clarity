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
            {
                if (number % 10 == 0)
                    return tens[number / 10 - 2]; // Adjusting index for zero-based array
                else
                    return $"{tens[number / 10 - 2]} {ones[number % 10]}".Trim();
            }

            // Handling hundreds
            if (number < 1000)
            {
                string remainderWords = ConvertToWords(number % 100);
                if (string.IsNullOrWhiteSpace(remainderWords))
                    return $"{ones[number / 100]} Hundred"; // Return without appending remainder if it's empty
                else
                    return $"{ones[number / 100]} Hundred {remainderWords}".Trim();
            }

            // Handling thousands
            if (number < 1000000)
            {
                string remainderWords = ConvertToWords(number % 1000);
                if (string.IsNullOrWhiteSpace(remainderWords))
                    return $"{ConvertToWords(number / 1000)} Thousand"; // Return without appending remainder if it's empty
                else
                    return $"{ConvertToWords(number / 1000)} Thousand {remainderWords}".Trim();
            }

            // Handle other cases here if needed

            return "Number too large to convert"; // Handle unsupported cases
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
