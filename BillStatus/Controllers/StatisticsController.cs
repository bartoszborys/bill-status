using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BillStatus.Models;
using BillStatus.Models.Contexts;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BillStatus.Controllers
{
    [Route("[controller]")]
    [Controller]
    public class StatisticsController : Controller
    {
        private readonly BillContext _context;

        public StatisticsController(BillContext context)
        {
            _context = context;
        }

        public ActionResult Index()
        {
            ViewData["TypeID"] = new SelectList(_context.BillTypes, "Id", "Name");
            return View();
        }

        [HttpGet("Measures/{billTypeId}")]
        public async Task<IEnumerable<BillMeasure>> Measures([FromRoute] int billTypeId, [FromQuery] DateTime periodFrom, [FromQuery] DateTime periodTo)
        {
            return this._context.BillMeasures
                .Where(x => x.Type.Id == billTypeId)
                .Where(x => x.CreatedAt > periodFrom)
                .Where(x => x.CreatedAt < periodTo);
        }

        [HttpGet("PriceParts/{billTypeId}")]
        public async Task<IEnumerable<BillPricePart>> PriceParts([FromRoute] int billTypeId)
        {
            return this._context.BillPriceParts
                .Where(x => x.Type.Id == billTypeId);
        }
    }
}