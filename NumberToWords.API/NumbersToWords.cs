namespace NumberToWords.API
{
    public class NumberToWords
    {
        private static readonly string[] Ones =
        {
            "", "ONE", "TWO", "THREE", "FOUR", "FIVE", "SIX", "SEVEN", "EIGHT",
            "NINE", "TEN", "ELEVEN", "TWELVE", "THIRTEEN", "FOURTEEN", "FIFTEEN",
            "SIXTEEN", "SEVENTEEN", "EIGHTEEN", "NINETEEN"
        };

        private static readonly String [] Tens =
        {
            "", "", "TWENTY", "THIRTY", "FORTY", "FIFTY", "SIXTY", "SEVENTY", "EIGHTY",
            "NINETY"
        };

        private static readonly String [] Scale =
        {
            "", "THOUSAND", "MILLION", "BILLION", "TRILLION"
        };

        public string Convert(string input)
        {   
            // Validate Input 

            if (string.IsNullOrWhiteSpace(input))
                throw new ArgumentException("Inpute cannot be empty");

            // Remove Whitespaces 
            input = input.Trim();

            // Check for digits and atmost one decimal point
            foreach (char c in input)
            { 
                if(!char.IsDigit(c) && c!= '.' )
                {
                    throw new ArgumentException("Input must only contain digits and a decimal point");
                }
            }

            // Check for multiple decimal points 
            bool flag = false;

            foreach (char c in input)
            {
                if (c == '.' && flag)
                {
                    throw new ArgumentException("There should be atmost one decimal point.");
                }

                if (c == '.')
                {
                    flag = true; 
                    continue;
                }

            }

            string dollars;
            string cents;

            if (flag)
            {
                string [] dollars_cents = input.Split('.');

                dollars = dollars_cents[0] == "" ? "0" : dollars_cents[0];
                cents = dollars_cents[1] == "" ? "00" : dollars_cents[1];
            }
            else
            {
                dollars = input;
                cents = "0";
            }

            if (dollars.Length > 15)
            {
                throw new ArgumentException("Number is too large. Inputs are supported uptill 999 trillion");
            }

            if (cents.Length > 2)
            {
                throw new ArgumentException("Inputs upto two decimal places are accepted");
            }

            // Adding padding to dollars if needed
            
            while (dollars.Length % 3 != 0)
            {
                dollars = "0" + dollars;
            }

            // Splitting dollars into chunks of three

            List<string> dollarChunks = new List<string>();

            for (int i = 0; i < dollars.Length; i+= 3)
            {
                dollarChunks.Add(dollars.Substring(i,3));
            }

            // Putting it all together for dollar chunks with scales
            string finalDollarString = "";
            int scaleIndex = dollarChunks.Count - 1;

            for(int i = 0; i < dollarChunks.Count; i++)
            {
                int chunkValue = int.Parse(dollarChunks[i]);
                string chunkWord = ConvertChunk(chunkValue);

                if (chunkValue != 0)

                {
                    if (finalDollarString != "")
                        finalDollarString += " ";

                finalDollarString += chunkWord;

                finalDollarString += " " + Scale[scaleIndex];

                }
                
                scaleIndex--; 
            }

            finalDollarString = finalDollarString.Trim();

            // Convert Cents 
            if (cents.Length  == 1)
            {
                cents += "0";
            }

            int centsValue = int.Parse(cents);
            string centString = ConvertChunk(centsValue);

            if (centsValue == 0 || centsValue == 00) 
                centString = "";

            // Combining dollars and cents 
            string result = "";

            if (finalDollarString == "")
                finalDollarString = "ZERO";

            result = finalDollarString + " DOLLARS";

            string centLabel = centsValue == 1 ? " CENT" : " CENTS";
            
            if (centString != "")
                result +=  " AND " + centString + centLabel;

            return result.Trim(); 
        }

        // Helper function to convert dollar chunks into words 
        public string ConvertChunk(int dollars)
        {   

            string dollarString = "";

            if (dollars == 0)
            {
                dollarString = "ZERO";
            }
            else if (dollars < 20)
            {
                dollarString = Ones[dollars];
            }
            else if (dollars >= 20 && dollars < 100)
            {
                dollarString = Tens[dollars / 10] + " " + Ones[dollars % 10];
            }
            else if (dollars >= 100 && dollars <= 999)
            {   
                int dollarSubstring = dollars % 100;

                if (dollarSubstring == 0)
                {
                    dollarString = Ones[dollars / 100] + " HUNDRED";
                }
                else if (dollarSubstring < 20)
                {
                    dollarString =  Ones[dollars / 100] + " HUNDRED AND " + Ones[dollarSubstring];
                }
                else
                {
                    dollarString = Ones[dollars / 100] + " HUNDRED AND " + Tens[dollarSubstring / 10] + " " + Ones[dollarSubstring % 10];
                }
                
            }

            return dollarString.Trim(); 

        }

    }
}

