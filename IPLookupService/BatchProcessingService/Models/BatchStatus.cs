namespace BatchProcessingService.Models
{
    public class BatchStatus
    {
        public Guid BatchId { get; set; }
        public int Total { get; set; }
        public int Processed { get; set; }
        public bool IsCompleted { get; set; }
    }
}
