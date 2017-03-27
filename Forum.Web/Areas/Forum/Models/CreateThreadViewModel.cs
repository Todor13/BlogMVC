using Forum.Models;
using Forum.Services.Contracts;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace Forum.Web.Areas.Forum.Models
{
    public class CreateThreadViewModel : IMapFrom<Thread>
    {
        private string content;
        private string title;

        [AllowHtml]
        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 13)]
        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                var input = value.Trim();
                this.title = input;
            }
        }

        [AllowHtml]
        [Required]
        [StringLength(200, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 13)]
        public string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                var input = value.Trim();
                this.content = input;
            }
        }

        [Required(ErrorMessage = "Section Field Is Required")]
        public int SectionId { get; set; }
    }
}