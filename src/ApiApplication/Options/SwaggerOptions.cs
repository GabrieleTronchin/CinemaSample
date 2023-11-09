using System.ComponentModel.DataAnnotations;

namespace ApiApplication.Options
{
    public class SwaggerOptions
    {
        [Required]
        public string Endpoint { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Version { get; set; }

    }
}
