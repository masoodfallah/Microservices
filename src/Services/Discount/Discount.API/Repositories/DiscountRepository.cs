﻿using Dapper;
using Discount.API.Entities;
using Npgsql;

namespace Discount.API.Repositories;

public class DiscountRepository : IDiscountRepository
{
    private readonly IConfiguration _configuration;

    public DiscountRepository(IConfiguration configuration)
    {
        _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
    }

    public async Task<Coupon> Get(string productName)
    {
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("Databasesettings:ConnectionString"));

        var coupon = await connection.QueryFirstOrDefaultAsync<Coupon>
            ("SELECT * FROM Coupon WHERE ProductName = @ProductName", new { productName = productName });

        if (coupon == null)
            return new Coupon { ProductName = "No Discount", Amount = 0, Description = "No Discount Description" };

        return coupon;
    }

    public async Task<bool> Create(Coupon coupon)
    {
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("Databasesettings:ConnectionString"));

        var affected = await connection.ExecuteAsync
            ("INSERT INTO Coupon (ProductName, Description, Amount) VALUES (@ProductName, @Description, @Amount)",
            new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount });

        if (affected == 0)
            return false;

        return true;
    }

    public async Task<bool> Delete(string productName)
    {
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("Databasesettings:ConnectionString"));

        var affected = await connection.ExecuteAsync
            ("DELETE FROM Coupon WHERE ProductName = @ProductName",
            new { ProductName = productName });

        if (affected == 0)
            return false;

        return true;
    }

    public async Task<bool> Update(Coupon coupon)
    {
        using var connection = new NpgsqlConnection(_configuration.GetValue<string>("Databasesettings:ConnectionString"));

        var affected = await connection.ExecuteAsync
            ("UPDATE Coupon SET ProductName=@ProductName, Description=@Description, Amount=@Amount WHERE Id = @Id",
            new { ProductName = coupon.ProductName, Description = coupon.Description, Amount = coupon.Amount, Id = coupon.Id });

        if (affected == 0)
            return false;

        return true;
    }
}
