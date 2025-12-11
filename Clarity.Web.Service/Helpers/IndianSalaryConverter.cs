namespace Clarity.Web.Service.Helpers
{
    public static class IndianSalaryConverter
    {
        private static string[] ones = {
        "", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine", "Ten",
        "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen"
        };

        private static string[] tens = {
        "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety"
    };

        public static string ConvertToWords(decimal salary)
        {
            if (salary == 0)
                return "Zero";

            long intPart = (long)Math.Floor(salary); // Integer part of the salary
            int decPart = (int)((salary - intPart) * 100); // Decimal part (paise)

            string intWords = ConvertToWordsIndian(intPart); // Convert integer part to words
            string decWords = ConvertToWordsIndian(decPart); // Convert decimal part to words

            if (string.IsNullOrEmpty(intWords))
                return $"{decWords} paise only";

            if (string.IsNullOrEmpty(decWords))
                return $"{intWords} rupees only";

            return $"{intWords} rupees and {decWords} paise only";
        }

        private static string ConvertToWordsIndian(decimal number)
        {
            if (number == 0)
                return "";

            long intPart = (long)Math.Floor(number); // Extract the integer part
            string words = "";

            // Handle Crores
            if ((intPart / 10000000) > 0)
            {
                words += ConvertToWordsIndian(intPart / 10000000) + " Crore ";
                intPart %= 10000000;
            }

            // Handle Lakhs
            if ((intPart / 100000) > 0)
            {
                words += ConvertToWordsIndian(intPart / 100000) + " Lakh ";
                intPart %= 100000;
            }

            // Handle Thousands
            if ((intPart / 1000) > 0)
            {
                words += ConvertToWordsIndian(intPart / 1000) + " Thousand ";
                intPart %= 1000;
            }

            // Handle Hundreds
            if ((intPart / 100) > 0)
            {
                words += ConvertToWordsIndian(intPart / 100) + " Hundred ";
                intPart %= 100;
            }

            // Handle Tens and Ones
            if (intPart > 0)
            {
                if (words != "")
                    words += "and ";

                if (intPart < 20)
                    words += ones[intPart];
                else
                {
                    words += tens[intPart / 10];
                    if ((intPart % 10) > 0)
                        words += "-" + ones[intPart % 10];
                }
            }

            return words.Trim();
        }
    }
}
