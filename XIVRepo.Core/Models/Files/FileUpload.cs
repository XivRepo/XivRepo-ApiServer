using System;
using System.ComponentModel.DataAnnotations;

namespace XIVRepo.Core.Models.Files
{
    public class FileUpload
    {
        [Key]
        public Guid Id { get; set; }
        public string Url { get; set; }
        public string Filename { get; set; }
    }
}