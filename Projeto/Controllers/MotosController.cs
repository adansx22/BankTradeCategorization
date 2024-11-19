using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Projeto.Data;
using Projeto.Models;

namespace Projeto.Controllers
{
    [Authorize]
    public class MotosController : Controller
    {
        private readonly ApplicationDbContext _context;

        public MotosController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Motos
        public async Task<IActionResult> Index()
        {
            return View(await _context.Motos.ToListAsync());
        }

        // GET: Motos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moto = await _context.Motos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (moto == null)
            {
                return NotFound();
            }

            return View(moto);
        }

        [HttpPost("filtrar")]
        public IActionResult FiltrarMotosPorPlaca([FromBody] Moto filtro)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Os dados fornecidos no corpo da solicitação são inválidos.");
            }

            var motosQuery = _context.Motos.AsQueryable();

            if (filtro != null && !string.IsNullOrWhiteSpace(filtro.Placa))
            {
                motosQuery = motosQuery.Where(m => m.Placa.Contains(filtro.Placa));
            }

            var motosFiltradas = motosQuery.ToList();

            return Ok(motosFiltradas);
        }

        // GET: Motos/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Motos/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Ano,Modelo,Placa,Marca,Disponivel")] Moto moto)
        {
            try
            {
                // Verificar se a placa já existe
                var placaExistente = _context.Motos.Any(m => m.Placa == moto.Placa);
                if (placaExistente)
                    return BadRequest("Já existe uma moto cadastrada com essa placa.");

                if (ModelState.IsValid)
                {
                    _context.Add(moto);
                    await _context.SaveChangesAsync();
                    //return RedirectToAction(nameof(Index));
                    return Ok("Moto criada com sucesso.");
                }
            }
            catch (Exception ex)
            {
                // Log de erro
                return StatusCode(500, "Erro interno ao criar moto.");
            }
            return View(moto);
        }

        // GET: Motos/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var moto = await _context.Motos.FindAsync(id);
            if (moto == null)
            {
                return NotFound();
            }
            return View(moto);
        }

        // POST: Motos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Ano,Modelo,Placa,Marca,Disponivel")] Moto moto)
        {
            if (id != moto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(moto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MotoExists(moto.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(moto);
        }

        // GET: Motos/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            try
            {
                // Lógica para excluir moto
                var moto = _context.Motos.Find(id);

                if (moto == null)
                    return NotFound("Moto não encontrada.");

                _context.Motos.Remove(moto);
                _context.SaveChanges();

                return Ok("Moto excluída com sucesso.");
            }
            catch (Exception ex)
            {
                // Log de erro
                return StatusCode(500, "Erro interno ao excluir moto.");
            }
        }

        // POST: Motos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var moto = await _context.Motos.FindAsync(id);
            if (moto != null)
            {
                _context.Motos.Remove(moto);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MotoExists(int id)
        {
            return _context.Motos.Any(e => e.Id == id);
        }
    }
}
