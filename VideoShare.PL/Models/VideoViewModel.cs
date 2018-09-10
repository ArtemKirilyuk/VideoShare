using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using VideoShare.BLL.DTO;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace VideoShare.PL.Models
{
    public class VideoViewModel
    {
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        [Required(ErrorMessage ="Can't create video with empty name.")]
        public string Name { get; set; }

        public int Views { get; set; }

        public string Duration { get; set; }

        public string Path { get; set; }

        [HiddenInput(DisplayValue = false)]
        public string FileName { get; set; }

        public string ApplicationUserId { get; set; }

        [HiddenInput(DisplayValue = false)]
        public int PlaylistId { get; set; }

        public string Description { get; set; }

        [HiddenInput(DisplayValue = false)]
        public bool Liked { get; set; }

        public int LikesCount { get; set; }

        public IEnumerable<CategoryDTO> Categories { get; set; }

        public IEnumerable<CommentDTO> Comments { get; set; }
    }
}