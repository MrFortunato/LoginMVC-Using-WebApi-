using Microsoft.AspNetCore.Http;
using Projecto_Final.Validations;
using System;
using System.ComponentModel.DataAnnotations;

namespace Exame_Online.Models
{
    public class CandidaturaAddModel
    {
        [Key]
        public int Id_Candidatura { get; set; }

        [Display(Name = "Nome")]
        [Required(ErrorMessage = "Digite o seu Nome")]
        public string Nome { get; set; }


        [Display(Name = "Sobrenome")]
        [StringLength(20, ErrorMessage = "Máximo 20 caracteres")]
        [Required(ErrorMessage = "Digite o seu Sobrenome")]
        public string SobreNome { get; set; }

        [Display(Name = "Gênero")]
        [Required(ErrorMessage = "Selecione o gênero")]
        public string Genero { get; set; }
        
        [Display(Name = "Selecione a data de Nascimento")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyy}")] //Format as ShortDateTime
        [DataType(DataType.Date)]
        [Maior18Anos]
        public DateTime? Dt_Nasc { get; set; }

        [Display(Name = "Estado Civíl")]
        [Required(ErrorMessage = "Selecione o Estado Civíl")]
        [Range(1, 4)]
        public int? EstadoCivil { get; set; }
        [Display(Name ="Nr do BI")]
        //[StringLength(14,MinimumLength =10,ErrorMessage ="Nº do BI inválido")]
        [Required(ErrorMessage = "Digite o Nº do Bilhete de Identidade")]
        public string NrBI { get; set; }

        [Display(Name = "Telefone")]
        [Required(ErrorMessage = "Digite o seu terminal")]
        public string Telefone { get; set; }

        [DataType(DataType.EmailAddress)]
        [RegularExpression(@"^\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*$", ErrorMessage = "Email is not valid.")]
        [Required(ErrorMessage = "Digite o Email")]
        public string Email { get; set; }

        [Display(Name = "Nome de Usuário")]
        [Required(ErrorMessage = "Digite o Nome de Usuário")]
        public string NomeUser { get; set; }

        [Display(Name = "Senha")]
        [DataType(DataType.Password)]
        [Required(ErrorMessage ="Digite a Senha")]
        public string Senha { get; set; } 

        [Display(Name = "Confirmar Senha")]
        [DataType(DataType.Password)]
        [Compare("Senha",ErrorMessage ="A Senha não coincide")]
        [Required(ErrorMessage = "Digite a Senha")]
        public string ConfirmarSenha { get; set; }

        [Display(Name = "Opção 1")]
        [Required(ErrorMessage = "Selecione o curso (1ª Opção)")]
        public int? Id_Curso { get; set; }
        [Display(Name = "Opção 2")]
        [Required(ErrorMessage = "Selecione o curso (2ª Opção)")]
        public int? Id_Curso2 { get; set; }

        [Display(Name = "Período")]
        [Required(ErrorMessage = "Selecione o perído")]
        public int Periodo { get; set; }
        [Display(Name = "Ano Acadêmico")]
        [Required(ErrorMessage = "Selecione o Ano Acadêmico")]
        public int? AnoLectvo { get; set; }

        [Display(Name = "Foto")]
        public IFormFile Foto { get; set; }
    }
}
