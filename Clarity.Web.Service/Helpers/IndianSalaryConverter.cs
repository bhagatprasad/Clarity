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
            if (number == 0)
                return "zero";

            if (number < 0)
                return "minus " + ConvertDecimalToWords(Math.Abs(number));

            string words = "";

            if ((number / 1000000) > 0)
            {
                words += ConvertDecimalToWords(number / 1000000) + " million ";
                number %= 1000000;
            }

            if ((number / 1000) > 0)
            {
                words += ConvertDecimalToWords(number / 1000) + " thousand ";
                number %= 1000;
            }

            if ((number / 100) > 0)
            {
                words += ConvertDecimalToWords(number / 100) + " hundred ";
                number %= 100;
            }

            if (number > 0)
            {
                if (words != "")
                    words += "and ";

                var unitsMap = new[] { "zero", "one", "two", "three", "four", "five", "six", "seven", "eight", "nine", "ten", "eleven", "twelve", "thirteen", "fourteen", "fifteen", "sixteen", "seventeen", "eighteen", "nineteen" };
                var tensMap = new[] { "zero", "ten", "twenty", "thirty", "forty", "fifty", "sixty", "seventy", "eighty", "ninety" };

                if (number < 20)
                    words += unitsMap[(int)number];
                else
                {
                    words += tensMap[(int)(number / 10)];
                    if ((number % 10) > 0)
                        words += "-" + unitsMap[(int)(number % 10)];
                }
            }

            return words;


        }

       
    }
}
