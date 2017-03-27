using Forum.Models;
using Forum.Services.Contracts;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Forum.Web.Areas.Forum.Models
{
    public class CreateThreadViewModel : IMapFrom<Thread>
    {
        [AllowHtml]
        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 13)]
        public string Title { get; set; }

        [AllowHtml]
        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 13)]
        public string Content { get; set; }

        [Required(ErrorMessage = "Section Field Is Required")]
        [StringLength(20, ErrorMessage = "Choose Section please", MinimumLength = 3)]
        public int SectionId { get; set; }
    }
}