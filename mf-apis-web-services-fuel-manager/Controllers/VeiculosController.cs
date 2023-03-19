using mf_apis_web_services_fuel_manager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mf_apis_web_services_fuel_manager.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VeiculosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public VeiculosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var model = await _context.Veiculos.ToListAsync();
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(Veiculo model)
        {
            if (model.AnoFabricacao <= 0 || model.AnoModelo <= 0)
            {
                return BadRequest(new { message = "Ano de Fabricação e Ano Modelo são obrigatórios e devem ser maiores que zero" });
            }

            _context.Veiculos.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = model.Id }, model);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Veiculos
                .Include(t => t.Usuarios)
                .ThenInclude(t => t.Usuario)
                .Include(t => t.Consumos)
                .FirstOrDefaultAsync(v => v.Id == id);

            if (model == null) return NotFound();

            GerarLinks(model); // Adicionando os links

            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, Veiculo model)
        {
            if (id != model.Id) return BadRequest();

            var modelDb = await _context.Veiculos.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);

            if (modelDb == null) return NotFound();

            _context.Veiculos.Update(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }


        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Veiculos.FindAsync(id);

            if (model == null) return NotFound();

            _context.Veiculos.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private void GerarLinks(Veiculo model)
        {
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "self", metodo: "GET"));
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "update", metodo: "PUT"));
            model.Links.Add(new LinkDto(model.Id, Url.ActionLink(), rel: "delete", metodo: "DELETE"));
        }

        [HttpPost("{id}/usuarios")]
        public async Task<ActionResult> AddUsuario(int id, VeiculoUsuarios model)
        {
            if (id != model.VeiculoId) return BadRequest();

            _context.VeiculoUsuarios.Add(model);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new {id = model.VeiculoId}, model);            
        }

        [HttpDelete("{id}/usuarios/{usuarioId}")]
        public async Task<ActionResult> DeleteUsuario(int id, int usuarioId)
        {
            var model = await _context.VeiculoUsuarios
                .Where(c => c.VeiculoId == id && c.UsuarioId == usuarioId)
                .FirstOrDefaultAsync();

            if (model == null) return NotFound();

            _context.VeiculoUsuarios.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }    
}





// Anotações------------------------------------------
/* ControllerBase: usado para configurar a API.
 No padrão MVC normalmente usamos apenas a Controller */

/* [ApiController] - O próprio framework faz as validações, então não precisamos definir via código por exemplo um erro 404, essa notação da API serva para já fazer isso de forma automatizada */

/*  public VeiculosController(AppDbContext context): Injeção de dependência  */

/* Toda classe que precisar de usar o banco de dados, é só usar essa configuração:

  private readonly AppDbContext _context;

        public VeiculosController(AppDbContext context)
        {
            _context = context;

        }
*/

/* ActionResult: Usado para retornar o tipo dados, ou seja, não precisamos pegar a resposta do banco de dados e converter para arquivo JSON ou HTML, ele configura isso através desse modelo de resposta.
*/
/* async Task por ser assíncrona : uma thread sendo executada*/

/* Quando o tipo for inteiro, temos o defatult, que é zero. Então acontece de aceitar a inserção do dado sem o valor, então temos que validar isso na API, método POST
  
  if (model.AnoFabricacao <= 0 || model.AnoModelo <= 0)
            {
                return BadRequest(new { message = "Ano de Fabricação e Ano modelo devem ser maior que zero" });
            }
 */

/*
 return CreatedAtAction("GetById", new {id = model.Id}, model);

Dessa forma ele retorna o Status ....
Nesse caso, é utilizado o próprio HTTP para informar que é um criação com sucesso de um recurso;
 */

/*
  if (id != model.Id) return BadRequest("Id incorreto");
Essa validação é por segurança, para não ter a possibilidade de trocar um id do objeto no banco
 */

/*
    var modelDb = _context.Veiculos.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);
            if (modelDb == null) return NotFound();
Verificando se existe na base de dados
 */

/*  
 --------------------ASNOTRACKING

   var modelDb = _context.Veiculos.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);

AsNoTracking(): Serve para não rastrear o dados, ou seja, a consulta não é para alteração.
*/


/*
 ----------------------- INCLUDE

 *  [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
                {
            var model = await _context.Veiculos
                .Include(t => t.Consumos) 
                .FirstOrDefaultAsync(v => v.Id == id);

            if (model == null) return NotFound();

            return Ok(model);
        }

------> O include vai servir para aquela propriedade de navegação pegar todos os consumos vinculados ao veículo.
A propriedade está definida na model:  
public ICollection<Consumo> Consumos { get; set; }
 */


/*
   GerarLinks(model); // É adicionado no GETBYID para add os links antes do retorno do método
 */
/*
 ------------------- ACTION LINK
Url.ActionLink(): O Asp net fornece esse método padrão.
 */

/*
 [HttpPost("{id}/usuarios")]
Quando vamos criar rotars associaando um recurso a outro recurso, definir dessa forma

        public async Task<ActionResult> AddUsuario
    Serve para associar usuarios com os veiculos

No getbyid vou adicionado:

 .Include(t => t.Usuarios)
 .ThenInclude(t => t.Usuario)
Coloca os 2 pq a tabela usuarios, é do tipo veiculousuarios, nessa table só tem o ID, o Usuario é incluído também para recuperar as informações do usuário, não somente o id
 */