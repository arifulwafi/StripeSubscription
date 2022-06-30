using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeSubscription.StripeGateway
{
    public class InvoiceAppService
    {
        public void CreateInvoice()
        {
            StripeConfiguration.ApiKey = StripeConsts.SecretKey;
            DateTime dt = DateTime.UtcNow;
            var nextMonth = new DateTime(dt.AddMonths(1).Year, dt.AddMonths(1).Month, 1);
            nextMonth = nextMonth.AddMonths(1);

            var options = new InvoiceCreateOptions
            {
                Customer = StripeConsts.Customer1,
            };
            var service = new InvoiceService();
            service.Create(options);
        }

        public void RetrieveInvoice()
        {
            StripeConfiguration.ApiKey = StripeConsts.SecretKey;
            DateTime dt = DateTime.UtcNow;
            var nextMonth = new DateTime(dt.AddMonths(1).Year, dt.AddMonths(1).Month, 1);
            nextMonth = nextMonth.AddMonths(1);

            var options = new UpcomingInvoiceOptions
            {
                Customer = StripeConsts.Customer1,
                SubscriptionStartDate = nextMonth
            };
            var service = new InvoiceService();
            Invoice upcoming = service.Upcoming(options);
        }
    }
}
