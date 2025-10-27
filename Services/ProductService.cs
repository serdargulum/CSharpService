namespace CSharpService.Services;

using CSharpService.Data;
using Microsoft.EntityFrameworkCore;
using MySql.Data.MySqlClient;
using System.Data;
using Dapper;
using CSharpService.Models;
using System.Data.Common;

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

    private static T? GetNullable<T>(DbDataReader reader, string column)
    {
        int ord = reader.GetOrdinal(column);
        if (reader.IsDBNull(ord)) return default;
        return (T)reader.GetValue(ord);
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
                Id = reader.GetInt64(reader.GetOrdinal("Id")),
                BrandId = reader.IsDBNull(reader.GetOrdinal("BrandId")) ? null : reader.GetInt64(reader.GetOrdinal("BrandId")),
                ProductCategoryId = reader.IsDBNull(reader.GetOrdinal("ProductCategoryId")) ? null : reader.GetInt64(reader.GetOrdinal("ProductCategoryId")),
                TaxId = reader.IsDBNull(reader.GetOrdinal("TaxId")) ? null : reader.GetInt64(reader.GetOrdinal("TaxId")),
                StockType = reader.GetInt32(reader.GetOrdinal("StockType")),
                Stock = GetNullable<ushort>(reader, "Stock"),
                CurrencyCode = reader.GetString(reader.GetOrdinal("CurrencyCode")),
                Price = reader.GetDecimal(reader.GetOrdinal("Price")),
                IsPrepackedItem = reader.GetBoolean(reader.GetOrdinal("IsPrepackedItem")),
                IsActive = reader.GetBoolean(reader.GetOrdinal("IsActive")),
                PackagingPrice = reader.GetDecimal(reader.GetOrdinal("PackagingPrice")),
                Calories = reader.IsDBNull(reader.GetOrdinal("Calories")) ? null : GetNullable<ushort>(reader, "Calories"),
                SKU = reader.IsDBNull(reader.GetOrdinal("SKU")) ? null : reader.GetString(reader.GetOrdinal("SKU")),
                DeletedAt = reader.IsDBNull(reader.GetOrdinal("DeletedAt")) ? null : reader.GetDateTime(reader.GetOrdinal("DeletedAt")),
                CreatedAt = reader.IsDBNull(reader.GetOrdinal("CreatedAt")) ? null : reader.GetDateTime(reader.GetOrdinal("CreatedAt")),
                UpdatedAt = reader.IsDBNull(reader.GetOrdinal("UpdatedAt")) ? null : reader.GetDateTime(reader.GetOrdinal("UpdatedAt")),
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
