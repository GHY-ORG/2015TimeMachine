using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace _2015TimeMachineCookie.Models
{
    public class UploadPicForm
    {
        [Required]
        [DataType(DataType.Text)]
        public string Title { set; get; }

        [Required]
        public int Type { set; get; }

        [Required]
        public int Order { set; get; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Time { set; get; }
        [Required]
        [DataType(DataType.Text)]
        public string Address { set; get; }
        [Required]
        [DataType(DataType.Text)]
        public string Description { set; get; }
    }
}