using Microsoft.AspNetCore.Mvc;
using PAW3.Core.BusinessLogic;
using PAW3.Models.Entities;

namespace PAW3.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SupplierApiController(ISupplierBusiness supplierBusiness) : ControllerBase
{
    // GET: api/SupplierApiController
    [HttpGet]
    public async Task<ActionResult<IEnumerable<Supplier>>> Get()
    {
        var suppliers = await supplierBusiness.GetSuppliers(id: null);
        return Ok(suppliers);
    }

    // GET api/SupplierApiController/5
    [HttpGet("{id}")]
    public async Task<ActionResult<Supplier>> Get(int id)
    {
        var suppliers = await supplierBusiness.GetSuppliers(id);
        var supplier = suppliers.FirstOrDefault();
        if (supplier == null)
            return NotFound();
        return Ok(supplier);
    }

    // POST api/SupplierApiController
    [HttpPost]
    public async Task<ActionResult<bool>> Post([FromBody] Supplier supplier)
    {
        var result = await supplierBusiness.SaveSupplierAsync(supplier);
        if (result)
            return CreatedAtAction(nameof(Get), new { id = supplier.SupplierId }, supplier);
        return BadRequest();
    }

    // PUT api/SupplierApiController/5
    [HttpPut("{id}")]
    public async Task<ActionResult<bool>> Put(int id, [FromBody] Supplier supplier)
    {
        if (id != supplier.SupplierId)
            return BadRequest();
        
        var result = await supplierBusiness.SaveSupplierAsync(supplier);
        if (result)
            return Ok(result);
        return NotFound();
    }

    // DELETE api/SupplierApiController/5
    [HttpDelete("{id}")]
    public async Task<ActionResult<bool>> Delete(int id)
    {
        var result = await supplierBusiness.DeleteSupplierAsync(id);
        if (result)
            return Ok(result);
        return NotFound();
    }
}

