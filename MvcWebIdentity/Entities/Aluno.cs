using System.ComponentModel.DataAnnotations;

namespace MvcWebIdentity.Entities
{
    public class Aluno
    {
        public int AlunoId { get; set; }
        [Required, MaxLength(80, ErrorMessage ="Nome Não pode ter mais que 80 caracteres")]
        public string? Nome { get; set; }
        [EmailAddress]
        [Required,MaxLength(120)]
        public string? Email { get; set; }
        public int Idade { get; set; }
        [MaxLength(80)]
        public string? Curso { get; set; }
    }
}
