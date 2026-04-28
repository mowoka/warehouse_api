namespace DataWarehouse.Domain;

public enum BatchStatus
{
    Available,
    Empty,
    Expired,
}


public class InventoryBatch
{
    public int Id {get; set;}
    public Guid BatchId {get; set;} = Guid.NewGuid();
    public int ProductId {get; set;}
    public int Quantity {get; set;}
    public int RemainingQty {get; set;}
    public DateTime EntryDate {get; set;} = DateTime.Now;
    public int PicInId {get; set;}
    public BatchStatus Status {get; set;} = BatchStatus.Available;

    public Product? product {get; set;}
    public User? PicIn {get; set;}
}