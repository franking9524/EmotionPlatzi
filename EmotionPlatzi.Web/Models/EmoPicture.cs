using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Web;

namespace EmotionPlatzi.Web.Models
{
    public class EmoPicture
    {
        public int Id { get; set; }
        [Display(Name ="Nombre")]
        public string Nombre { get; set; }
        //[Required]
        //[MaxLength(1000, ErrorMessage = "La ruta supera el rango establecido")]
        [Display(Name = "Ruta")]
        public string Path { get; set; }

        public virtual ObservableCollection<EmoFace> Faces { get; set; }
    }
}