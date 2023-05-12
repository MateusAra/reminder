using Entity.Model;
using System.ComponentModel.DataAnnotations;

namespace entity.model
{
    public class Reminder : BaseModel
    {

        [Required(ErrorMessage = "É necessário fornecer o Nome, verifique.")]
        public string? Name { get; set; }

        [Required(ErrorMessage = "É necessário fornecer a Data, verifique.")]
        public DateTime Date { get; set; }

    }
}