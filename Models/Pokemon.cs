using System.ComponentModel.DataAnnotations;

namespace Login01.Models
{
    public class Pokemon
    {
        [Key, Display(Name = "Nº")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Insira o nome do Pokemon!")]
        [Display(Name = "Nome")]
        public string Name { get; set; }

        [Display(Name = "Descrição")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Selecione o tipo do Pokemon!")]
        [Display(Name = "Tipo")]
        public string Type { get; set; }

        [Display(Name = "Altura")]
        public string? Height { get; set; }

        [Display(Name = "Peso")]
        public float? Width { get; set; }

        [Display(Name ="Gênero")]
        public string? Gender { get; set; }

        [Display(Name ="Habilidades")]
        public string? Abilities { get; set; }

        public byte[]? Image { get; set; }   
    }
}
