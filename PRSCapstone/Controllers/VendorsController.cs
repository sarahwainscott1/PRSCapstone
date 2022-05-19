using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PRSCapstone.Models;

namespace PRSCapstone.Controllers {
    [Route("api/[controller]")]
    [ApiController]
    public class VendorsController : ControllerBase {
        private readonly AppDbContext _context;

        public VendorsController(AppDbContext context) {
            _context = context;
        }
        private const string review = "REVIEW";
        private const string approve = "APPROVED";
        private const string reject = "REJECTED";
        // GET: api/Vendors
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Vendor>>> GetVendor() {
            if (_context.Vendor == null) {
                return NotFound();
            }
            return await _context.Vendor.ToListAsync();
        }

        // GET: api/Vendors/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Vendor>> GetVendor(int id) {
            if (_context.Vendor == null) {
                return NotFound();
            }
            var vendor = await _context.Vendor.FindAsync(id);

            if (vendor == null) {
                return NotFound();
            }

            return vendor;
        }
        //PO Lines
        [HttpGet("po/{vendorId}")]
        public async Task<ActionResult<Po>> CreatePo(int vendorId) {
            var po = new Po() {
                Vendor = await _context.Vendor.FindAsync(vendorId)
            };
                if (po.Vendor == null) {
                    return NotFound();
                }
                var rawpolines = from v in _context.Vendor
                                 join p in _context.Products on v.Id equals p.VendorId
                                 join rl in _context.RequestLines on p.Id equals rl.ProductId
                                 join r in _context.Requests on rl.RequestId equals r.Id
                                 where r.Status == approve && v.Id == vendorId
                                 select new {
                                     p.Id,
                                     Product = p.Name,
                                     rl.Quantity,
                                     p.Price,
                                     LineTotal = p.Price * rl.Quantity
                                 };
                var sortedLines = new SortedList<int, PoLine>();
                foreach (var line in rawpolines) {
                    if (!sortedLines.ContainsKey(line.Id)) {
                        var poline = new PoLine();
                        {
                            poline.Product = line.Product;
                            poline.Quantity = line.Quantity;
                            poline.Price = line.Price;
                            poline.LineTotal = line.LineTotal;
                        };
                        sortedLines.Add(line.Id, poline);
                    }
                else {
                    sortedLines[line.Id].Quantity += line.Quantity;
                    sortedLines[line.Id].LineTotal += line.LineTotal;
                    }
                }
            po.PoLines = sortedLines.Values;
            po.PoTotal = po.PoLines.Sum(x => x.LineTotal);
                return po;

            }
        
 
        // PUT: api/Vendors/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutVendor(int id, Vendor vendor) {
            if (id != vendor.Id) {
                return BadRequest();
            }

            _context.Entry(vendor).State = EntityState.Modified;

            try {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) {
                if (!VendorExists(id)) {
                    return NotFound();
                }
                else {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/Vendors
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Vendor>> PostVendor(Vendor vendor) {
            if (_context.Vendor == null) {
                return Problem("Entity set 'AppDbContext.Vendor'  is null.");
            }
            _context.Vendor.Add(vendor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetVendor", new { id = vendor.Id }, vendor);
        }

        // DELETE: api/Vendors/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVendor(int id) {
            if (_context.Vendor == null) {
                return NotFound();
            }
            var vendor = await _context.Vendor.FindAsync(id);
            if (vendor == null) {
                return NotFound();
            }

            _context.Vendor.Remove(vendor);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool VendorExists(int id) {
            return (_context.Vendor?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
