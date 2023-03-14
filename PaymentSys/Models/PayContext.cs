using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

//конструктор бд
namespace PaymentSys.Models
{
    public class PayContext : DbContext
    {
        public PayContext(DbContextOptions<PayContext> options):base(options)
        {

        }
        public DbSet<PaymentReq> PaymentReqs { get; set; }
        
    }

}
