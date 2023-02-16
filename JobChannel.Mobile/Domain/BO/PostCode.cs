namespace JobChannel.Mobile.Domain.BO
{
    public class PostCode
    {
        public string Postcode { get; set; }

        public PostCode() => Postcode = string.Empty;

        public PostCode(string postcode) => Postcode = postcode;
    }
}