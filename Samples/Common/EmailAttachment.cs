namespace AzureSendgridSample.Common
{
    public class EmailAttachment
    {
        public string FileName { get; set; }
        public string Content { get; set; } // Base64 encoded content
        public string Type { get; set; } // MIME type
    }
}
