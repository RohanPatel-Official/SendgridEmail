using System.ComponentModel.DataAnnotations;

namespace AzureSendgridSample.Common
{
    public class EmailRequest
    {
        public string ToEmail { get; set; }
        public string Subject { get; set; }
        public string Message { get; set; }
        public string CcEmail { get; set; } = string.Empty;
        public string BccEmail { get; set; } = string.Empty;
        public string HTMLMessage { get; set; } = string.Empty;
        public List<AttachmentRequest> Attachments { get; set; } = new List<AttachmentRequest> ();
    }

    public class AttachmentRequest
    {
        public string FileName { get; set; }
        public string Content { get; set; } // Base64 encoded content
        public string Type { get; set; } // MIME type
    }
}
