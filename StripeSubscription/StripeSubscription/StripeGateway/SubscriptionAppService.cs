using Stripe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StripeSubscription.StripeGateway
{
    public class SubscriptionAppService
    {
        public void CreateTrialSubscription()
        {
            StripeConfiguration.ApiKey = StripeConsts.SecretKey;
            var trialEnd = DateTime.UtcNow.AddDays(AppConsts.TrialDays);
            var billingCycleAnchor = CalculateBillingCyleAnchor(trialEnd);

            var options = new SubscriptionCreateOptions
            {
                Customer = StripeConsts.Customer1,
                Items = new List<SubscriptionItemOptions>
                    {
                        new SubscriptionItemOptions
                        {
                            Quantity = 1,
                            Price = StripeConsts.PriceComplete,
                        },
                    },
                TrialEnd = trialEnd,
                BillingCycleAnchor = billingCycleAnchor

            };
            var service = new SubscriptionService();
            service.Create(options);
        }

        public void UpdateSubscription()
        {
            StripeConfiguration.ApiKey = StripeConsts.SecretKey;
            var removeProducts = new List<string> { "si_LzllYdnQBpJbzB" };
            var addProducts = new List<string> { StripeConsts.PriceCore, StripeConsts.PriceLeave, StripeConsts.PriceExpense,  };

            var service = new SubscriptionService();
            Subscription subscription = service.Get(StripeConsts.Subscription1);
            var subscriptionItems = new List<SubscriptionItemOptions>();

            // Existing prouducts/modules update
            foreach (var item in subscription.Items)
            {
                // Remove from existing product
                if (removeProducts.Contains(item.Id))
                {
                    subscriptionItems.Add(new SubscriptionItemOptions
                    {
                        Id = item.Id,
                        Deleted = true
                    });
                }
            }

            //New products/module
            foreach (var item in addProducts)
            {
                subscriptionItems.Add(new SubscriptionItemOptions
                {
                    Price = item,
                    Quantity = AppConsts.SubscriptionQty
                });
            }

            var options = new SubscriptionUpdateOptions
            {
                Items = subscriptionItems
            };

            service.Update(StripeConsts.Subscription1, options);
        }

        public void UpdateSubscriptionOnNewUser()
        {
            StripeConfiguration.ApiKey = StripeConsts.SecretKey;

            var service = new SubscriptionService();
            Subscription subscription = service.Get(StripeConsts.Subscription1);
            var subscriptionItems = new List<SubscriptionItemOptions>();

            foreach (var item in subscription.Items)
            {
                subscriptionItems.Add(new SubscriptionItemOptions
                {
                    Id = item.Id,
                    Quantity = item.Quantity + 1
                });
            }

            var options = new SubscriptionUpdateOptions
            {
                Items = subscriptionItems
            };

            service.Update(StripeConsts.Subscription1, options);
        }

        public void UpdateSubscriptionOnUserLeave()
        {
            StripeConfiguration.ApiKey = StripeConsts.SecretKey;

            var service = new SubscriptionService();
            Subscription subscription = service.Get(StripeConsts.Subscription1);
            var subscriptionItems = new List<SubscriptionItemOptions>();

            foreach (var item in subscription.Items)
            {
                subscriptionItems.Add(new SubscriptionItemOptions
                {
                    Id = item.Id,
                    Quantity = 10
                });
            }

            var options = new SubscriptionUpdateOptions
            {
                Items = subscriptionItems
            };

            service.Update(StripeConsts.Subscription1, options);
        }

        public void UpdateSubscriptionOnNewModules()
        {
            StripeConfiguration.ApiKey = StripeConsts.SecretKey;

            var removeProducts = new List<string> { "si_LyGfY576wWCBFU", "si_LyGnzbN6uLVKKb" };
            var addProducts = new List<string> { "price_1LGHYLF6Es8YSFIWfamFqXBH", "price_1LGHX4F6Es8YSFIWnRAhPfqH" };

            var service = new SubscriptionService();
            Subscription subscription = service.Get(StripeConsts.Subscription1);
            var subscriptionItems = new List<SubscriptionItemOptions>();

            // Existing prouducts/modules update
            foreach (var item in subscription.Items)
            {
                // Remove from existing product
                if (removeProducts.Contains(item.Id))
                {
                    subscriptionItems.Add(new SubscriptionItemOptions
                    {
                        Id = item.Id,
                        Deleted = true
                    });
                }
            }

            //New products/module
            foreach (var item in addProducts)
            {
                subscriptionItems.Add(new SubscriptionItemOptions
                {
                    Price = item,
                    Quantity = AppConsts.SubscriptionQty
                });
            }

            var options = new SubscriptionUpdateOptions
            {
                Items = subscriptionItems
            };

            service.Update(StripeConsts.Subscription1, options);
        }

        private DateTime CalculateBillingCyleAnchor(DateTime trialEnd)
        {
            DateTime dt = DateTime.UtcNow;
            var nextMonth = new DateTime(dt.AddMonths(1).Year, dt.AddMonths(1).Month, 1);
            if (nextMonth < trialEnd)
                nextMonth = nextMonth.AddMonths(1);
            return nextMonth;
        }

        //public void CreateSubscription()
        //{
        //    StripeConfiguration.ApiKey = StripeConsts.SecretKey;
        //    var trialEnd = DateTime.UtcNow.AddDays(AppConsts.TrialDays);
        //    var billingCycleAnchor = CalculateBillingCyleAnchor(trialEnd);

        //    var options = new SubscriptionCreateOptions
        //    {
        //        Customer = StripeConsts.Customer1,
        //        Items = new List<SubscriptionItemOptions>
        //        {
        //            new SubscriptionItemOptions
        //            {
        //                Quantity = AppConsts.SubscriptionQty,
        //                Price = StripeConsts.PriceComplete,
        //            },
        //        },
        //        TrialEnd = trialEnd,
        //        BillingCycleAnchor = billingCycleAnchor

        //    };
        //    var service = new SubscriptionService();
        //    service.Create(options);
        //}

        //public void UpdateSubscription()
        //{
        //    StripeConfiguration.ApiKey = StripeConsts.SecretKey;
        //    //var trialEnd = DateTime.UtcNow.AddDays(AppConsts.TrialDays);
        //    //var billingCycleAnchor = CalculateBillingCyleAnchor(trialEnd);

        //    var service = new SubscriptionService();
        //    Subscription subscription = service.Get(StripeConsts.Subscription1);

        //    var options = new SubscriptionUpdateOptions
        //    {
        //        Items = new List<SubscriptionItemOptions>
        //        {
        //            new SubscriptionItemOptions
        //            {
        //                Id = subscription.Items.Data[0].Id,
        //                Quantity = AppConsts.SubscriptionQty,
        //                Price = StripeConsts.PriceComplete,
        //                Deleted = true
        //            },
        //            new SubscriptionItemOptions
        //            {
        //                Quantity = AppConsts.SubscriptionQty,
        //                Price = StripeConsts.PriceExpense
        //            },
        //            //new SubscriptionItemOptions
        //            //{
        //            //    Quantity = AppConsts.SubscriptionQty,
        //            //    Price = StripeConsts.PriceLeave,
        //            //}
        //        },

        //    };

        //    service.Update(StripeConsts.Subscription1, options);
        //}
    }
}
