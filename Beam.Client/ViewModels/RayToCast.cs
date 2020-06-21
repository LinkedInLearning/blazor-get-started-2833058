using System.ComponentModel.DataAnnotations;
namespace Beam.Client.ViewModels
{
    public class RayToCast
    {
        [Required(ErrorMessage = "You can't send an empty Ray!")]
        public string Text { get; set; } = "";
    }
}