using System.Collections.Generic;

namespace CVhub.Models
{
    public class JobOfferCreateView : JobOffer 
    {
        public IEnumerable<Company> Companies { get; set; }
    }
}
