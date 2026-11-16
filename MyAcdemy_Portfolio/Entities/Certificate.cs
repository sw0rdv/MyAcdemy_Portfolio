namespace MyAcdemy_Portfolio.Entities
{
    public class Certificate
    {
        public int CertificateID { get; set; }
        public string Title { get; set; }
        public string Issuer { get; set; }
        public string Year { get; set; }
        public string? CertificateUrl { get; set; }
    }
}
