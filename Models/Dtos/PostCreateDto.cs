using System;
using System.ComponentModel.DataAnnotations;

namespace AdMedAPI.Models.Dtos
{

    public class PostCreateDto

    {

        [Required] public string PostName { get; set; }
        [Required] public string PostDescription { get; set; }
        [Required] public string Content { get; set; }
        public DateTime TimeStamp { get; set; }

    }

}
