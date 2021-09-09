using System;
using System.ComponentModel.DataAnnotations;

namespace XIVRepo.Core.Models.Files
{
    public class PreviewImage
    {
        public Guid Id { get; set; }
        public string ImageUrl { get; set; }
        public string? ImageDescription { get; set; }
    }
}