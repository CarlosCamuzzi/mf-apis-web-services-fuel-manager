using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mf_apis_web_services_fuel_manager.Models
{
    [Table("Veiculos")]
    public class Veiculo : LinksHATEOS
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Marca { get; set; }
        [Required]
        public string Modelo { get; set; }
        [Required]
        public string Placa { get; set; }
        [Required]
        public int AnoFabricacao { get; set; }
        [Required]
        public int AnoModelo { get; set; }
        
        /* Navegação Virtual */
        public ICollection<Consumo> Consumos { get; set; }

        public ICollection<VeiculoUsuarios> Usuarios{ get; set; }
    }
}

// Anotações --------------------------------------
/* Relação Veículos x Consumo é 1:n */
/* Navegação Virtual: Como é 1:n, nessa tabela de veículos não precisamos ter nenhuma propriedade de consumo, mas podemos ter uma navegação virtual.*/
/* 1 veículo possui vários consumos e 1 consumo está associado a apenas 1 veículo */
/* Quando o tipo for inteiro, temos o default, que é zero. Então acontece de aceitar a inserção do dado sem o valor, então temos que validar isso na API, método POST*/
/*
 * Veículo pode estar associado a vários usuários
 public ICollection<VeiculoUsuarios> Usuarios{ get; set; } 
 */
