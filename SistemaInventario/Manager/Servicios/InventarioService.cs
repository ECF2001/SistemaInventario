using System.Data;
using AppDbContext;
using Dapper;
using DTO;
using Manager.Datos;
using Manager.Repositorio;

namespace SistemaInventario.Manager.Servicios
{
    public interface IInventarioService
    {
        Task<IEnumerable<Productos>> GetProductosAsync();
        Task<IEnumerable<Productos>> GetStockBajoMinimoAsync();
        Task<IEnumerable<Movimientos>> GetKardexAsync(int idProducto, DateTime? desde = null, DateTime? hasta = null);
        Task<Movimientos> AjusteStockAsync(int idProducto, int cantidad, string tipo, string? observaciones = null);
    }

    public class InventarioService : IInventarioService
    {
        private readonly AppDbContext.AppDbContext _db;
        private readonly IProductoRepository _productoRepo;
        private readonly IMovimientoRepository _movRepo;

        public InventarioService(AppDbContext.AppDbContext db, IProductoRepository productoRepo, IMovimientoRepository movRepo)
        {
            _db = db;
            _productoRepo = productoRepo;
            _movRepo = movRepo;
        }

        public async Task<IEnumerable<Productos>> GetProductosAsync()
        {
            return await _productoRepo.ListAsync<Productos>();
        }

        public async Task<IEnumerable<Productos>> GetStockBajoMinimoAsync()
        {
            var all = await _productoRepo.ListAsync<Productos>();
            return all.Where(p => p.Stock_Actual <= p.Stock_Minimo);
        }

        public async Task<IEnumerable<Movimientos>> GetKardexAsync(int idProducto, DateTime? desde = null, DateTime? hasta = null)
        {
            using var conn = _db.CreateConnection();
            var sql = "SELECT * FROM Movimientos WHERE id_producto = @id ORDER BY fecha ASC";
            if (desde.HasValue || hasta.HasValue)
            {
                sql = "SELECT * FROM Movimientos WHERE id_producto = @id AND (@desde IS NULL OR fecha >= @desde) AND (@hasta IS NULL OR fecha <= @hasta) ORDER BY fecha ASC";
            }
            var result = await conn.QueryAsync<Movimientos>(sql, new { id = idProducto, desde, hasta });
            return result;
        }

        public async Task<Movimientos> AjusteStockAsync(int idProducto, int cantidad, string tipo, string? observaciones = null)
        {
            // tipo: "ENTRADA" o "SALIDA"
            using var conn = _db.CreateConnection();
            if (conn.State != ConnectionState.Open) conn.Open();
            using var tx = conn.BeginTransaction();

            try
            {
                var prod = await _productoRepo.GetByIdAsync<Productos>(idProducto);
                if (prod is null) throw new InvalidOperationException("Producto no encontrado");

                var delta = tipo.ToUpperInvariant() == "SALIDA" ? -Math.Abs(cantidad) : Math.Abs(cantidad);
                var nuevoStock = prod.Stock_Actual + delta;
                if (nuevoStock < 0) throw new InvalidOperationException("El stock no puede quedar negativo");

                // 1) Insertar movimiento
                var mov = new Movimientos
                {
                    Id_Producto = idProducto,
                    Tipo_Movimiento = tipo,
                    Cantidad = Math.Abs(cantidad),
                    Fecha = DateTime.UtcNow,
                    Observaciones = observaciones
                };
                await _movRepo.AddAsync(mov, conn, tx);

                // 2) Actualizar stock de producto
                prod.Stock_Actual = nuevoStock;
                await _productoRepo.UpdateAsync(prod, conn, tx);

                tx.Commit();
                return mov;
            }
            catch
            {
                tx.Rollback();
                throw;
            }
        }
    }
}
