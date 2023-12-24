namespace InventoryManagementAPI.Models
{
    public class RequestDTO
    {
        public int RequestId { get; set; }
        public string ProductName { get; set; }
        public int ProductQuantity { get; set; }
        public string RequestStatus { get; set; }
    }
}
