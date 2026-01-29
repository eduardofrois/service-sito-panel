using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ServiceSitoPanel.src.constants;

namespace ServiceSitoPanel.src.functions
{
    public static class HandleFunctions
    {
        public static string? SelectStatus(int value)
        {
            switch (value)
            {
                case 1:
                    return StatusOrder.NewStatus[Status.PendingPurchase];

                case 2:
                    return StatusOrder.NewStatus[Status.SaleToRecive];

                case 3:
                    return StatusOrder.NewStatus[Status.ReadyForDelivery];

                case 4:
                    return StatusOrder.NewStatus[Status.ConfirmSale];

                case 5:
                    return StatusOrder.NewStatus[Status.PaidPurchase];

                case 9:
                    return StatusOrder.NewStatus[Status.PartialPayment];

                case 10:
                    return StatusOrder.NewStatus[Status.FullyPaid];

                case 11:
                    return StatusOrder.NewStatus[Status.DeliveredToClient];

                default:
                    return null;
            }
        }

        public static List<string>? SelectOneOrMoreStatus(int value)
        {
            switch (value)
            {
                case 1:
                    return new List<string> { StatusOrder.NewStatus[Status.PendingPurchase] };

                case 2:
                    return new List<string> { StatusOrder.NewStatus[Status.SaleToRecive] };

                case 3:
                    return new List<string> { StatusOrder.NewStatus[Status.ReadyForDelivery] };

                case 4:
                    return new List<string> { StatusOrder.NewStatus[Status.ConfirmSale] };

                case 5:
                    return new List<string> { StatusOrder.NewStatus[Status.PaidPurchase] };

                case 6:
                    // MoreThenOne: Inclui Compra Realizada, Pagamento Parcial e Pagamento Quitado
                    // Mantém compatibilidade retroativa incluindo Compra Quitada também
                    return new List<string> {
                        StatusOrder.NewStatus[Status.ConfirmSale],
                        StatusOrder.NewStatus[Status.PaidPurchase],
                        StatusOrder.NewStatus[Status.PartialPayment],
                        StatusOrder.NewStatus[Status.FullyPaid]
                    };

                case 7:
                    return new List<string> { StatusOrder.NewStatus[Status.ToCheck] };

                case 8:
                    return new List<string> { StatusOrder.NewStatus[Status.Checked] };

                case 9:
                    return new List<string> { StatusOrder.NewStatus[Status.PartialPayment] };

                case 10:
                    return new List<string> { StatusOrder.NewStatus[Status.FullyPaid] };

                case 11:
                    return new List<string> { StatusOrder.NewStatus[Status.DeliveredToClient] };

                default:
                    return new List<string>(); // Return empty list instead of null to prevent NullReferenceException
            }
        }

        public static TimeZoneInfo GetTimeZone()
        {
            var tz = TimeZoneInfo.FindSystemTimeZoneById("E. South America Standard Time");
            return tz;
        }

    }
}