using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace BL
{
    public class PilotageInfo
    {
        public PilotageInfo(string jsonString)
        {
            try
            {
                // Deserialize the JSON string into a temporary PilotageInfo object
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true // Enable case-insensitive property names
                };
                var tempPilotageInfo = JsonSerializer.Deserialize<PilotageInfo>(jsonString, options);

                // Copy the properties from the deserialized object to the current instance
                if (tempPilotageInfo != null)
                {
                    this.Vessel = tempPilotageInfo.Vessel;
                    this.Cargo = tempPilotageInfo.Cargo;
                    this.Pilotage = tempPilotageInfo.Pilotage;
                    this.FaultsOrDeficiencies = tempPilotageInfo.FaultsOrDeficiencies;
                    this.ContactDetails = tempPilotageInfo.ContactDetails;
                    this.PaymentInfo = tempPilotageInfo.PaymentInfo;
                    this.AdditionalInfo = tempPilotageInfo.AdditionalInfo;
                    this.HelcomNotificationRequired = tempPilotageInfo.HelcomNotificationRequired;
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine("Error parsing JSON: " + ex.Message);
            }
        }
        public PilotageInfo() { }
        [Key]
        public int Id { get; set; }
        public Vessel? Vessel { get; set; }
        public string? Cargo { get; set; }
        public Pilotage? Pilotage { get; set; }
        public string? FaultsOrDeficiencies { get; set; }
        public ContactDetails? ContactDetails { get; set; }
        public string? PaymentInfo { get; set; }
        public string? AdditionalInfo { get; set; }
        public bool? HelcomNotificationRequired { get; set; }
    }
}