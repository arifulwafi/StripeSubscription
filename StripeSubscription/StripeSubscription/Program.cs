using StripeSubscription.StripeGateway;
using System;

namespace StripeSubscription
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SaaS Subscription");

            CustomerAppService customerService = new CustomerAppService();
            //1. Create Customer 
            //customerService.CreateCustomer();

            SubscriptionAppService subscriptionService = new SubscriptionAppService();

            //2. Create Trial Subscription (complete module) without card
            //subscriptionService.CreateTrialSubscription();

            //3. Add Payment Method (LunaHR subscription page UI to Stripe)
            //4.Update Subscription(only if complete to other modules)
            subscriptionService.UpdateSubscription();

            //5. Add/Remove Users
            //subscriptionService.UpdateSubscriptionOnNewUser();

            //6. Add/Remove Modules
            //subscriptionService.UpdateSubscriptionOnNewModules();

            InvoiceAppService invoiceService = new InvoiceAppService();
            //invoiceService.CreateInvoice();

            //To Do:
            // Payment method management
            // Invoice method management
            // Webhook

            //Concerns
            //Advance payment/Invoice
        }
    }
}
