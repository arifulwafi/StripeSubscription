using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeSubscription.StripeGateway
{
    public class CustomerAppService
    {
        public void CreateCustomer()
        {
            StripeConfiguration.ApiKey = StripeConsts.SecretKey;

            var options = new CustomerCreateOptions
            {
                Name = "Bill Gates",
                Email = "gates@email.com",
            };
            var service = new CustomerService();
            service.Create(options);
        }
    }
}
