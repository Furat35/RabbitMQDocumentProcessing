namespace EDevlet.Document.Common
{
    public class CreateDocumentModel
    {
        public Guid UserId { get; set; }

        public string Url { get; set; }
        public DocumentType DocumentType { get; set; }
    }

    public enum DocumentType
    {
        Pdf,
        Html,
        Png
    }
}
