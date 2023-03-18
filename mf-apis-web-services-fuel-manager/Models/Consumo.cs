using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mf_apis_web_services_fuel_manager.Models
{
    [Table("Consumos")]
    public class Consumo : LinksHATEOS
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Descricao { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Valor { get; set; }
        [Required]
        public TipoCombustivel Tipo { get; set; }

        [Required]
        public int VeiculoId { get; set; }
        /* Navegação Virtual */
        public Veiculo Veiculo { get; set; }
    }
    public enum TipoCombustivel
    {
        Diesel,
        Etanol,
        Gasolina
    }
}

// Anotações ------------------------------------------
/* Relação Veículos x Consumo é 1:n */
/* Navegação Virtual: Quando carregar o consumo, irá carregar o veículo associado a esse consumo
 */
/* A migration deu um warning, para isso:
 Deu alerta no tipo decimal, então para isso fazer a configuração abaixo, que define um tipo decimal de 18 dígitos e 2 casas pós-vírgula
 [Column(TypeName = "decimal(18,2)")]

*/
