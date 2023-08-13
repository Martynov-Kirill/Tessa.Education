namespace Tessa.Education.Entites.Models
{
    public class ErrorFilterModel
    {
        public string Name { get; set; }
        public string MnemonicCode { get; set; }
        public IntInterval ErrorCodeRange { get; set; }
    }
    public class IntInterval
    {
        public int MinValue { get; set; }
        public int MaxValue { get; set; }
    }
}
