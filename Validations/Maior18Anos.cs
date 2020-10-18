using Exame_Online.Models;
using System;
using System.ComponentModel.DataAnnotations;


namespace Projecto_Final.Validations
{
    public class Maior18Anos:ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var Candidato = (CandidaturaAddModel)validationContext.ObjectInstance;

            if (Candidato.Dt_Nasc == null)
                return new ValidationResult("Digite a sua Data de Nascimento.");

            var age = DateTime.Today.Year - Convert.ToDateTime(Candidato.Dt_Nasc).Year;

            return (age >= 18)
                ? ValidationResult.Success
                : new ValidationResult("O Candidato deve ser maior de Idade.");
        }
    }
}
