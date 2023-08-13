namespace Tessa.Education.BLL.Models
{
    /// <summary>
    ///  Base Error Model
    /// </summary>
    public class ErrorModel
    {
        public int Code { get; set; }
        public string MnemonicCode { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string[] Params { get; set; }
    }
}
