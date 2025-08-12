using System.Data;
using AppDbContext;
using Dapper;
using DTO;
using Manager.Repositorio;

namespace SistemaInventario.Manager.Servicios
{
    public interface ICompraService
    {
        Task<int> RegistrarCompraAsync(CompraRequest request);
        Task<IEnumerable<Compras>> ListarComprasAsync(DateTime? desde = null, DateTime? hasta = null, int? idProveedor = null);
        Task<(Compras? compra, IEnumerable<Detalle_Compras> detalle)> ObtenerDetalleCompraAsync(int idCompra);
    }

    public class CompraService : ICompraService
    {
        private readonly AppDbContext.AppDbContext _db;
        private readonly ICompraRepository _compraRepo;
        private readonly IDetalleCompraRepository _detalleRepo;
        private readonly IProductoRepository _productoRepo;
        private readonly IMovimientoRepository _movRepo;

        public CompraService(
            AppDbContext.AppDbContext db,
            ICompraRepository compraRepo,
            IDetalleCompraRepository detalleRepo,
            IProductoRepository productoRepo,
            IMovimientoRepository movRepo)
        {
            _db = db;
            _compraRepo = compraRepo;
            _detalleRepo = detalleRepo;
            _productoRepo = productoRepo;
            _movRepo = movRepo;
        }

        public async Task<int> RegistrarCompraAsync(CompraRequest request)
        {
            if (request.Items is null || request.Items.Count == 0)
                throw new InvalidOperationException("La compra debe tener al menos un item.");

            using var conn = _db.CreateConnection();
            if (conn.State != ConnectionState.Open) conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                decimal total = request.Items.Sum(i => i.PrecioUnitario * i.Cantidad);

                // 1) Insert cabecera
                var compra = new Compras
                {
                    Fecha_Compra = request.FechaCompra,
                    Id_Proveedor = request.IdProveedor,
                    Total = total
                };
                await _compraRepo.AddAsync(compra, conn, tx);
                int idCompra = compra.Id;

                // 2) Insert detalle + actualizar stock + registrar movimiento (ENTRADA)
                foreach (var it in request.Items)
                {
                    var det = new Detalle_Compras
                    {
                        Id_Compra = idCompra,
                        Id_Producto = it.IdProducto,
                        Cantidad = it.Cantidad,
                        Precio_Unitario = it.PrecioUnitario
                    };
                    await _detalleRepo.AddAsync(det, conn, tx);

                    var prod = await _productoRepo.GetByIdAsync<Productos>(it.IdProducto);
                    if (prod is null) throw new InvalidOperationException($"Producto {it.IdProducto} no encontrado.");

                    prod.Stock_Actual += it.Cantidad;
                    prod.Precio_Compra = it.PrecioUnitario; // opcional: actualizar último costo
                    await _productoRepo.UpdateAsync(prod, conn, tx);

                    var mov = new Movimientos
                    {
                        Id_Producto = it.IdProducto,
                        Tipo_Movimiento = "ENTRADA",
                        Cantidad = it.Cantidad,
                        Fecha = request.FechaCompra,
                        Observaciones = $"Compra #{idCompra}"
                    };
                    await _movRepo.AddAsync(mov, conn, tx);
                }

                tx.Commit();
                return idCompra;
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }

        public async Task<IEnumerable<Compras>> ListarComprasAsync(DateTime? desde = null, DateTime? hasta = null, int? idProveedor = null)
        {
            using var conn = _db.CreateConnection();
            var sql = "SELECT * FROM Compras WHERE 1=1" +
                      " AND (@desde IS NULL OR fecha_compra >= @desde)" +
                      " AND (@hasta IS NULL OR fecha_compra <= @hasta)" +
                      " AND (@idProveedor IS NULL OR id_proveedor = @idProveedor)" +
                      " ORDER BY fecha_compra DESC, Id DESC";
            var data = await conn.QueryAsync<Compras>(sql, new { desde, hasta, idProveedor });
            return data;
        }

        public async Task<(Compras? compra, IEnumerable<Detalle_Compras> detalle)> ObtenerDetalleCompraAsync(int idCompra)
        {
            using var conn = _db.CreateConnection();
            var compra = await _compraRepo.GetByIdAsync<Compras>(idCompra);
            var detalle = await conn.QueryAsync<Detalle_Compras>(
                "SELECT * FROM Detalle_Compras WHERE id_compra = @id",
                new { id = idCompra });
            return (compra, detalle);
        }
    }
}
