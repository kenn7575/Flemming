using BL.OpenAI;
using OpenAI_API.Moderation;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using static Microsoft.Graph.CoreConstants;

namespace BL
{
    public class CategorizedEmail : Email
    {
        [NotMapped]
        public string CategoryName { get; set; }
        public int CategoryId { get; set; }

        public CategorizedEmail(Email email, string categoryName, int categoryId) : base(email)
        {
            CategoryName = categoryName;
            CategoryId = categoryId;
        }
        public CategorizedEmail(Email email, CategorizeEmailResponse cer ) : base(email)
        {
            CategoryName = cer.CategoryName;
            CategoryId = cer.CategoryId;
        }
        public CategorizedEmail() 
        {
            
        }


        public enum EmailCategories
        {
            None,
            NewPilotageRequests,
            VesselArrivalDepartureUpdates,
            CargoOperations,
            InvoiceAndBillingInformation,
            OrderCancellations,
            OrderModifications,
            CommercialAndSalesInquiries,
            SpamAndAds,
            Miscellaneous,
            StatementOfTruth
        }
       
        public enum EmailCategoriesDisplay
        {
            Ingen,
            Nye_Lodsanmodning,
            Tidspdatering,
            Lastoperation,
            Faktureringsinformation,
            Ordreaflysning,
            Ordreredigering,
            Kommercielle_og_Salgsmæssige_Forespørgsl,
            Spam,
            Diverse,
            Sandhedserklæring
        }


    }
}
