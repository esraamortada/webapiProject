using System.ComponentModel;

namespace Anadolu.Models
{
    public class ContactUs
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Message { get; set; }

        [DefaultValue(false)]
        public bool IsDeleted { get; set; }
    }
}
