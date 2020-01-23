using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BillStatus.Models;
using BillStatus.Models.Contexts;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BillStatus.Controllers
{
    [Route("[controller]")]
    [Controller]
    public class BillTypesController : Controller
    {
        private readonly BillContext _context;

        public BillTypesController(BillContext context)
        {
            _context = context;
        }

        // GET: api/BillTypes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BillType>>> GetBillTypes()
        {
            return await _context.BillTypes.ToListAsync();
        }


        [HttpGet("create")]
        public IActionResult Create([FromQuery] int Count)
        {
            var model = new NewBillTypeDetails();
            model.Type = new BillType();
            model.PriceParts = Enumerable.Range(0, Count).Select(x => new BillPricePart() ).ToList();
            ViewBag.Count = Count + 1;
            return View(model);
        }

        [HttpPost("create")]
        public async Task<ActionResult<NewBillTypeDetails>> CreatePost([FromForm] NewBillTypeDetails billData)
        {
            _context.BillTypes.Add(billData.Type);
            foreach(var billPart in billData.PriceParts)
            {
                billPart.Type = billData.Type;
                _context.BillPriceParts.Add(billPart);
            }


            _context.SaveChanges();

            return CreatedAtAction("Create", billData);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BillType>> GetBillType(int id)
        {
            var billType = await _context.BillTypes.FindAsync(id);

            if (billType == null)
            {
                return NotFound();
            }

            return billType;
        }

        // PUT: api/BillTypes/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBillType(int id, BillType billType)
        {
            if (id != billType.Id)
            {
                return BadRequest();
            }

            _context.Entry(billType).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BillTypeExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/BillTypes
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<BillType>> PostBillType([FromBody] BillType billType)
        {
            _context.BillTypes.Add(billType);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetBillType", new { id = billType.Id }, billType);
        }

        // DELETE: api/BillTypes/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<BillType>> DeleteBillType(int id)
        {
            var billType = await _context.BillTypes.FindAsync(id);
            if (billType == null)
            {
                return NotFound();
            }

            _context.BillTypes.Remove(billType);
            await _context.SaveChangesAsync();

            return billType;
        }

        private bool BillTypeExists(int id)
        {
            return _context.BillTypes.Any(e => e.Id == id);
        }
    }
}
