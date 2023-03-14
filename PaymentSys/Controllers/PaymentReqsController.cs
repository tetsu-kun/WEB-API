using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PaymentSys.Models;

namespace PaymentSys.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentReqsController : ControllerBase
    {
        private readonly PayContext _context;

        public PaymentReqsController(PayContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentReq>>> GetpaymentReqs()
        {
            return await _context.PaymentReqs.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<PaymentReq>> GetPaymentReq(int id)
        {
            var paymentReq = await _context.PaymentReqs.FindAsync(id);

            if (paymentReq == null)
            {
                return NotFound();
            }

            return paymentReq;
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<List<PaymentReq>>> Get(int userId)
        {
            var req = await _context.PaymentReqs
                .Where(c => c.UserId == userId)
                .ToListAsync();


            return req;
        }


        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPaymentReq(int id, PaymentReq paymentReq)
        {
            if (id != paymentReq.UserId)
            {
                return BadRequest();
            }

            _context.Entry(paymentReq).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaymentReqExists(id))
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

        /*
        [HttpPost]
        public async Task<ActionResult<PaymentReq>> PostPaymentReq(PaymentReq req)
        {
            var user = await _context.Users.FirstOrDefaultAsync(req.UserId);
            if (user == await _context.Users.FindAsync(req.UserId))
                return NotFound();
                
            _context.PaymentReqs.Add(req);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentReq", new { id = req.UserId }, req);
        }
        */
       
        [HttpDelete("{id}")]
        public async Task<ActionResult<PaymentReq>> DeletePaymentReq(int id)
        {
            var paymentReq = await _context.PaymentReqs.FindAsync(id);
            if (paymentReq == null)
            {
                return NotFound();
            }

            _context.PaymentReqs.Remove(paymentReq);
            await _context.SaveChangesAsync();

            return paymentReq;
        }

        private bool PaymentReqExists(int id)
        {
            return _context.PaymentReqs.Any(e => e.UserId == id);
        }
    }
}
