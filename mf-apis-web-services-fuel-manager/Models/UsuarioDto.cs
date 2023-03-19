using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace mf_apis_web_services_fuel_manager.Models
{
    [NotMapped]
    public class UsuarioDto
    {        
        public int? Id { get; set; }
        [Required]
        public string Nome { get; set; }
        [Required]        
        public string Password { get; set; }
        [Required]
        public Perfil Perfil { get; set; }
    }
}


/*
 Essa classe serve para ser utilizada como interface para as requests de create de usuário. Quando foi utilizado o JsonIgnote na class Usuario, ele dá problema na hora do create, então é preciso criar essa class DTO, que irá para o DB
 */