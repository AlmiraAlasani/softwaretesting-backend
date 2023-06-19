using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PRO.Models
{
    public class Income
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int Id { get; set; }
        public string Description { get; set; }
        public decimal Amount { get; set; }
        //  public DateOnly Date { get; set; }
    }
}
