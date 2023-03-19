using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace mf_apis_web_services_fuel_manager.Models
{
    [Table("Usuarios")]
    public class Usuario
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]
        [JsonIgnore] // Password não retorna na response
        public string Password { get; set; }
        [Required]
        public Perfil Perfil { get; set; }

        /* Prop Navegação */
        public ICollection<VeiculoUsuarios> Veiculos { get; set; }
    }

    public enum Perfil
    {
        [Display(Name = "Administrador")]
        Administrador,
        [Display(Name = "Usuario")]
        Usuario
    }
}

/*
 O Relacionamento será N:N
O usuário possui um ou mais veículos (1:N)
O veículo pode associar a mais de um usuário (N:N)
Por ex, duas pessoas podem usar o mesmo carro.
 */

/*
 Usuário pode estar associado a vários veículos
     public ICollection<VeiculoUsuarios> Veiculos { get; set; }
 */

/*
   [JsonIgnore] Toda a vez que formos recuperar os dados do banco para retornar para aplicação, ele vai ignorar esse campo.
 */