
using System.ComponentModel.DataAnnotations;

namespace ModelProvider.ViewModels
{
    public class AutoView : IViewModell
    {
        [Key]
        public long Id { get; set; }

        [Required]
        public string Brand { get; set; }
        [Required]
        public string Model { get; set; }
        [Required]
        public string LicencePlate { get; set; }

        public virtual ClientView Client { get; set; }


        public override string ToString()
        {
            return Id + " " + Brand + " " + Model + " " + LicencePlate;
        }
    }
}
