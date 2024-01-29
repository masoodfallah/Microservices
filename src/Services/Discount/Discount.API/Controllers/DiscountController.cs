using Discount.API.Entities;
using Discount.API.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Discount.API.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DiscountController : ControllerBase
{
    private readonly IDiscountRepository _repository;

    public DiscountController(IDiscountRepository repository)
    {
        _repository = repository ?? throw new ArgumentNullException(nameof(repository));
    }


    [HttpGet("{productName}", Name = "GetDiscount")]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> Get(string productName)
    {
        var coupon = await _repository.Get(productName);
        return Ok(coupon);
    }


    [HttpPost]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> Create([FromBody] Coupon coupon)
    {
        await _repository.Create(coupon);

        return CreatedAtRoute("GetDiscount", new { productName = coupon.ProductName }, coupon);
    }


    [HttpPut]
    [ProducesResponseType(typeof(Coupon), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> Update([FromBody] Coupon coupon)
    {
        return Ok(await _repository.Update(coupon));
    }


    [HttpDelete("{productName}", Name = "DeleteDiscount")]
    [ProducesResponseType(typeof(void), (int)HttpStatusCode.OK)]
    public async Task<ActionResult<Coupon>> Delete(string productName)
    {
        return Ok(await _repository.Delete(productName));
    }

}
