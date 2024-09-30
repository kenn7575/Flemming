using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;


namespace BL
{
    public class Vessel
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? CallSign { get; set; }
        public string? ImoNumber { get; set; }
        public int? GrossTonnage { get; set; }
        public double? Length { get; set; }
        public double? Width { get; set; }
        public double? Draught { get; set; }
        public Speed? Speed { get; set; }

        public bool ValidateImoNumber()
        {
            if (ImoNumber == null) return false;
            // Check if the IMO number is 7 digits long and contains only digits
            if (ImoNumber.Length != 7 || !int.TryParse(ImoNumber, out _))
            {
                return false;
            }

            // Calculate the checksum based on the first 6 digits
            int checksum = 0;
            int[] multipliers = { 7, 6, 5, 4, 3, 2 }; // The positional factors

            for (int i = 0; i < 6; i++)
            {
                int digit = int.Parse(ImoNumber[i].ToString());
                checksum += digit * multipliers[i];
            }

            // Extract the check digit from the IMO number (7th digit)
            int checkDigit = int.Parse(ImoNumber[6].ToString());

            // The rightmost digit of the checksum is the actual calculated check digit
            int calculatedCheckDigit = checksum % 10;

            // Return true if the check digit matches the calculated one
            return calculatedCheckDigit == checkDigit;
        }
    }
}
