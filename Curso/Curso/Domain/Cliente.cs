using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Curso.Domain
{
    [Table("Clientes")] // caso o nome da tabela seja diferente na base de dados
    public class Cliente
    {
        [Key]
        public int Id { get; set; }
        [Required] //propriedade obrigatória
        public string Nome { get; set; }
        [Column("Phone")] //caso o campo da tabela seja diferente na base de dados
        public string Telefone { get; set; }
        public string CEP {get;set;}
        public string Estado { get; set; }
        public string Cidade { get; set; }
    }
}
