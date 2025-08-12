using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Dapper;
using DreamInCode.Manager.DTO;
using Microsoft.Data.SqlClient;
using Microsoft.IdentityModel.Tokens;

namespace SistemaInventario.Manager.Servicios
{
    public interface IAuthService
    {
        Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request);
    }
    public class AuthService : IAuthService
    {
        private readonly string _connectionString;
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
            _connectionString = config.GetConnectionString("SqlServerConnection");
        }

        public async Task<LoginResponseDTO> LoginAsync(LoginRequestDTO request)
        {
            using var connection = new SqlConnection(_connectionString);
            var query = "SELECT * FROM dbo.Users WHERE Correo = @Correo";
            var usuario = await connection.QueryFirstOrDefaultAsync<UsersDTO>(query, new { request.Correo });

            if (usuario == null) return null;

            if (!BCrypt.Net.BCrypt.Verify(request.Password, usuario.Password))
                return null;

            // Crear token
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(_config["Jwt:Key"]);

            var claims = new[]
             {
                new Claim(ClaimTypes.Name, usuario.Nombre),
                new Claim("Id", usuario.Id.ToString()),
                new Claim("Correo", usuario.Correo)
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(12),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _config["Jwt:Issuer"],
                Audience = _config["Jwt:Audience"]
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return new LoginResponseDTO
            {
                Nombre = usuario.Nombre,
                Token = tokenHandler.WriteToken(token)
            };
        }
    }
}
