using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PaymentSys.Models
{
    public class PaymentReq
    {
        [Key]
        public int Id { get; set; }
        [Column(TypeName="nvarchar(100)")]
        public string CardOwneName { get; set; }
        [Column(TypeName = "nvarchar(19)")]
        public string CardNumber { get; set; }
        [Column(TypeName = "nvarchar(5)")]
    
        public int UserId { get; set; }
    }
}
