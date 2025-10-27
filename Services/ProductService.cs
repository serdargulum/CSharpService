namespace CSharpService.Services;

using CSharpService.Data;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;
using CSharpService.Models;

public class ProductService : IProductService
{
    public AppDbContext _context;

    private readonly MySqlConnection _conn;
    public ProductService(MySqlConnection conn, AppDbContext context)
    {
        _context = context;
        _conn = conn;
    }

    public async Task<List<Product>> GetProductsAsync()
    {
        return await _context.Products
            .AsNoTracking() // EF değişiklik takibi yapmaz, performans artar
            .ToListAsync();
    }

    public List<Product> GetProducts()
    {
        return _context.Products.ToList();
    }

    public async Task<List<Product>> GetProductsSql(IConfiguration configuration)
    {
        // We must use the same connection
        if (_conn.State != ConnectionState.Open)
            await _conn.OpenAsync();

        using var cmd = new MySqlCommand("SELECT * FROM Products", _conn);
        using var reader = await cmd.ExecuteReaderAsync();
        var products = new List<Product>();
        while (await reader.ReadAsync())
        {
            products.Add(new Product
            {
                Id = reader.GetInt32(0)
            });
        }

        return products;
    }

    public async Task<List<Product>> GetProductsDapperAsync(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");

        using var conn = new MySqlConnection(connectionString);
        await conn.OpenAsync();

        var products = await conn.QueryAsync<Product>(
            "SELECT * FROM Products"
        );

        return products.ToList();
    }
}
