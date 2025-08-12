namespace DTO
{
    public class CompraRequest
    {
        public int IdProveedor { get; set; }
        public DateTime FechaCompra { get; set; } = DateTime.UtcNow;
        public List<CompraItem> Items { get; set; } = new();
        public string? Observaciones { get; set; }
    }

    public class CompraItem
    {
        public int IdProducto { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }
    }
}
