using EPiServer.Commerce.Order;
using EPiServer.Framework.Localization;
using EPiServer.ServiceLocation;
using Foundation.Cms.ViewModels;
using Foundation.Commerce.Customer.ViewModels;
using Foundation.Commerce.Models.Pages;
using Foundation.Commerce.Order.Payments;
using Mediachase.Commerce;
using Mediachase.Commerce.Orders;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Foundation.Commerce.Order.ViewModels
{
    [Bind(Exclude = "Payment")]
    public class CheckoutViewModel : ContentViewModel<CheckoutPage>
    {
        public const string MultiShipmentCheckoutViewName = "MultiShipmentCheckout";

        public const string SingleShipmentCheckoutViewName = "SingleShipmentCheckout";

        private Injected<LocalizationService> _localizationService;

        public CheckoutViewModel() => Payments = new List<PaymentOptionBase>();

        public CheckoutViewModel(CheckoutPage checkoutPage) : base(checkoutPage) => Payments = new List<PaymentOptionBase>();

        public CommerceHomePage CommerceHomePage => StartPage as CommerceHomePage;

        /// <summary>
        /// Gets or sets a collection of all coupon codes that have been applied.
        /// </summary>
        public IEnumerable<string> AppliedCouponCodes { get; set; }

        /// <summary>
        /// Gets or sets all available payment methods that the customer can choose from.
        /// </summary>
        public IEnumerable<PaymentMethodViewModel> PaymentMethodViewModels { get; set; }

        public string ReferrerUrl { get; set; }

        /// <summary>
        /// Gets or sets all existing shipments related to the current order.
        /// </summary>
        public IList<ShipmentViewModel> Shipments { get; set; }

        /// <summary>
        /// Gets or sets a list of all existing addresses for the current customer and that can be used for billing and shipment.
        /// </summary>
        public IList<AddressModel> AvailableAddresses { get; set; }

        /// <summary>
        /// Gets or sets the billing address.
        /// </summary>
        public AddressModel BillingAddress { get; set; }

        /// <summary>
        /// Gets or sets the payment method associated to the current purchase.
        /// </summary>
        public IList<PaymentOptionBase> Payments { get; set; }

        public IPaymentMethod Payment { get; set; }

        /// <summary>
        /// Gets or sets whether the shipping address should be the same as the billing address.
        /// </summary>
        public bool UseBillingAddressForShipment { get; set; }

        /// <summary>
        /// Gets or sets the view message.
        /// </summary>
        public string Message { get; set; }

        public int AddressType { get; set; }

        public Currency Currency { get; set; }

        public string SelectedPayment { get; set; }

        public OrderSummaryViewModel OrderSummary { get; set; }

        /// <summary>
        /// Gets the name of the checkout view required depending on the number of distinct shipping addresses.
        /// </summary>
        public string ViewName => Shipments.Count > 1 ? MultiShipmentCheckoutViewName : SingleShipmentCheckoutViewName;

        public ContactViewModel CurrentCustomer { get; set; }
        public string QuoteStatus { get; set; } = "";
        public bool IsOnHoldBudget { get; set; }

        public bool IsUsePaymentPlan { get; set; }

        public PaymentPlanSetting PaymentPlanSetting { get; set; }

        public List<SelectListItem> SubscriptionOption => new List<SelectListItem>
                {
                    new SelectListItem {Text = "3 months", Value = "3"},
                    new SelectListItem {Text = "6 months", Value = "6"},
                    new SelectListItem {Text = "12 months", Value = "12"}
                };
        public List<SelectListItem> Modes => new List<SelectListItem>
        {
            new SelectListItem { Text = string.IsNullOrEmpty(_localizationService.Service.GetString("/Shared/None")) ? "None" : _localizationService.Service.GetString("/Shared/None"), Value="0"},
            new SelectListItem { Text = string.IsNullOrEmpty(_localizationService.Service.GetString("/Shared/Days")) ? "Days" : _localizationService.Service.GetString("/Shared/Days"), Value="1"},
            new SelectListItem { Text = string.IsNullOrEmpty(_localizationService.Service.GetString("/Shared/Weeks")) ? "Weeks" : _localizationService.Service.GetString("/Shared/Weeks"), Value="2"},
            new SelectListItem { Text = string.IsNullOrEmpty(_localizationService.Service.GetString("/Shared/Months")) ? "Months" : _localizationService.Service.GetString("/Shared/Months"), Value="3"},
            new SelectListItem { Text = string.IsNullOrEmpty(_localizationService.Service.GetString("/Shared/Years")) ? "Years" : _localizationService.Service.GetString("/Shared/Years"), Value="4"}
        };
    }


    public class PaymentPlanSetting
    {
        public PaymentPlanCycle CycleMode
        {
            get; set;
        }
        public int CycleLength { get; set; }
        public int MaxCyclesCount { get; set; }
        public int CompletedCyclesCount { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime? LastTransactionDate { get; set; }
        public bool IsActive { get; set; }
    }

}