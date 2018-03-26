namespace TaxiCloud.Core.Api.Yandex.Model
{
    public class YandexBalanceModel
    {
        public string Id { get; set; }
        public float Balance { get; set; }

        public override string ToString()
        {
            return $"ID: {Id}, Balance: {Balance}";
        }
    }
}