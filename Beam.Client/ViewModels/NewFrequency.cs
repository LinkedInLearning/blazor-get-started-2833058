using System.ComponentModel.DataAnnotations;
namespace Beam.Client.ViewModels
{
    public class NewFrequency
    {
        [Required]
        [MaxLength(20)]
        [Display(Name="Frequency Name")]
        public string Name { get; set; } = "";
    }
}