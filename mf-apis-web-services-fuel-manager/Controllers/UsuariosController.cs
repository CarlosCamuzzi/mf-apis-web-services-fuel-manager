﻿using mf_apis_web_services_fuel_manager.Models;
using mf_apis_web_services_fuel_manager.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace mf_apis_web_services_fuel_manager.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public UsuariosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult> GetAll()
        {
            var model = await _context.Usuarios.ToListAsync();
            return Ok(model);
        }

        [HttpPost]
        public async Task<ActionResult> Create(UsuarioDto model)
        {
            Usuario novo = new Usuario()
            {
                Nome = model.Nome,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password), // Criptografando senha
                Perfil = model.Perfil
            };

            _context.Usuarios.Add(novo);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetById", new { id = novo.Id }, novo);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult> GetById(int id)
        {
            var model = await _context.Usuarios
                .FirstOrDefaultAsync(v => v.Id == id);

            if (model == null) return NotFound();

            return Ok(model);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, UsuarioDto model)
        {
            if (id != model.Id) return BadRequest();

            var modelDb = await _context.Usuarios.AsNoTracking()
                .FirstOrDefaultAsync(v => v.Id == id);

            if (modelDb == null) return NotFound();

            // Aqui é a mesma situação do UsuarioDto do post
            modelDb.Nome = model.Nome;
            modelDb.Password = BCrypt.Net.BCrypt.HashPassword(model.Password);
            modelDb.Perfil = model.Perfil;

            _context.Usuarios.Update(modelDb);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var model = await _context.Usuarios.FindAsync(id);

            if (model == null) return NotFound();

            _context.Usuarios.Remove(model);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        [AllowAnonymous] // Permite usuário anônimo
        [HttpPost("authenticate")]
        public async Task<ActionResult> Authenticate(AuthenticateDto model)
        {
            var usuarioDb = await _context.Usuarios.FindAsync(model.Id);
         
            // Verifica se existe usuário e se a senha está correta
            if (usuarioDb == null || !BCrypt.Net.BCrypt.Verify(model.Password, usuarioDb.Password)) return Unauthorized();

            var tokenService = new TokenService();
            var jwt = tokenService.GenerateJwtToken(usuarioDb);
                        
            return Ok(new {jwtToken = jwt});
        }       
    }
}


//------------------------ ANOTAÇÕES
/*
  [HttpPost]
        public async Task<ActionResult> Create(UsuarioDto model)

Utiliza o UsuarioDto, pois nesse objeto é possível enviar o password, conforme configurado na class usuariodto

Dessa forma, vamos configurar um novo usuário para salva no banco.

  Usuario novo = new Usuario()
            {
                Nome = model.Nome,
                Password = BCrypt.Net.BCrypt.HashPassword(model.Password), // Criptografando senha
                Perfil = model.Perfil
            };
 */

/*---------------------- JWT GenerateJwtToken(Usuario model)
1 - Cria um token
2 - Associa a key 
3 - Inclui os claims (declarações/atributos do usuário), ou seja, se adm, se é user...
4 - Cria o token com os claims, data de expiração, critografa (signingcredentials)
5 - Salva na variável token
6 - Retorna o token

 */