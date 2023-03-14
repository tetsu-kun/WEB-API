using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace PaymentSys.Models
{
    public class CreateReq
    {
        public string CardOwneName { get; set; } = "BARRY BLOCK";

        public string CardNumber { get; set; } = "4254684578542145";

        public int UserId { get; set; } = 1;
    }
}
