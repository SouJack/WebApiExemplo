using System;
using System.ComponentModel.DataAnnotations.Schema;
using Dapper.Contrib.Extensions;

namespace Vibe.Exemplo.WebApi.Modelos
{
    [Dapper.Contrib.Extensions.Table("CONTATO")]
    public class Contato
    {
        [Key]
        public int Id { get; set; }
        public string Cpf { get; set; }
        public string Nome { get; set; }
        public DateTime? Nascimento { get; set; }
        public String Observacao { get; set; }
        [Column("SITUACAOENTIDADE")]
        public Situacao SituacaoEntidade { get; set; }
    }
}