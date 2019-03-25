using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using WebAppMVC.Models;

namespace LudoApp.ValidationAttributes
{





    public sealed class CheckGameAttribute : ValidationAttribute
    {
        private List<string> ValidGameStatus = new List<string>() { "Lobby", "Running", "Paused", "Won" };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            //return success if value equals one of the valid gamestautes
            return ValidGameStatus.Where(x => x == value.ToString()).Any() ? ValidationResult.Success : new ValidationResult("Please choose valid GameStatus");
        }
    }


    public sealed class ListRange : RangeAttribute
    {

        public int min { get; set; }
        public int max { get; set; }

        public ListRange(int min, int max) : base(min, max)
        {
            this.min = min;
            this.max = max;
        }


        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value.GetType() == typeof(List<PlayerModel>))
            {
                List<PlayerModel> d = (List<PlayerModel>)value;

                if (d.Count <= max && d.Count >= min)
                    return ValidationResult.Success;
                else
                    return new ValidationResult($"Please stick the amount of players between the range of{min} and {max}");
            }
            else
                return new ValidationResult("Error serverside");

        }
    }


}