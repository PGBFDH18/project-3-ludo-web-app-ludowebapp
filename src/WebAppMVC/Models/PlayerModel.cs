using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace WebAppMVC.Models
{
    public class PlayerModel
    {
        public int PlayerId { get; set; }
        [Required]
        [StringLength(60, MinimumLength = 3)]
        public string Name { get; set; }
        public PlayerColor PlayerColor;
        public List<PieceModel> Pieces { get; set; }
        public int Offset
        {
            get
            {
                return (int)PlayerColor * 10;
            }
        }
    }
}
