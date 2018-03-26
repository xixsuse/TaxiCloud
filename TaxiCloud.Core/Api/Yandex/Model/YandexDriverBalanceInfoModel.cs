namespace TaxiCloud.Core.Api.Yandex.Model
{
    public class YandexDriverBalanceInfoModel
    {
        public string DriverId { get; set; }
        public string PaymentType { get; set; }
        public float Pay { get; set; }
        public float Balance { get; set; }
        public bool IsToday { get; set; }
    }
}