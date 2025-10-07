using AutoMapper;
using CE_TaskTest.BackEnd.Context;
using CE_TaskTest.BackEnd.Models;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.BackEnd.Services.Dtos.Response;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading;

namespace CE_TaskTest.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstimacionController(ApplicationDbContext _context, IMapper mapper, IValidator<EstimacionRequestDto>? validator = null) : ControllerBase
    {
        // GET: api/<EstimacionController>
        [HttpGet]
        public async Task<ActionResult<List<EstimacionResponseDto>>> Get()
        {
            try
            {
                var estimaciones = await _context.Estimaciones.ToListAsync();
                if (estimaciones.Count > 0)
                {
                    var estimacionesResponse = mapper.Map<List<EstimacionResponseDto>>(estimaciones);
                    return Ok(estimacionesResponse);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }

            return NotFound("No hay estimaciones registradas.");
        }

        // GET api/<EstimacionController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstimacionResponseDto>> GetById(int id)
        {
            try
            {
                var estimacion = await _context.Estimaciones.FindAsync(id);
                if (estimacion != null)
                {
                    var estimacionResponse = mapper.Map<EstimacionResponseDto>(estimacion);
                    return Ok(estimacionResponse);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }

            return NotFound($"No se encontró la estimación con ID {id}.");
        }

        // POST api/<EstimacionController>
        [HttpPost]
        public async Task<IActionResult> Post(EstimacionRequestDto estimacionRequestDto)
        {
            try
            {
                var validationResult = await validator!.ValidateAsync(estimacionRequestDto);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                var estimaciones = _context.Estimaciones.Any(x => x.Duracion == estimacionRequestDto.Duracion);
                if (estimaciones)
                {
                    return BadRequest("La estimacion ya existe");
                }

                var estimacion = mapper.Map<Estimacion>(estimacionRequestDto);
                await _context.AddAsync(estimacion);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = estimacion.Id }, estimacion);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT api/<EstimacionController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] EstimacionRequestDto estimacion)
        {
            try
            {
                var estimacionItem = await _context.Estimaciones.FindAsync(id);
                if (estimacionItem != null)
                {
                    var validationResult = await validator!.ValidateAsync(estimacion);
                    if (!validationResult.IsValid)
                    {
                        return BadRequest(validationResult.Errors);
                    }

                    if (_context.Estimaciones.Any(x => x.Id != id && x.Duracion == estimacion.Duracion))
                    {
                        return BadRequest("La estimacion ya existe");
                    }

                    estimacionItem.Duracion = estimacion.Duracion;

                    _context.Update(estimacionItem);
                    await _context.SaveChangesAsync();

                    return NoContent();
                }

                return BadRequest();

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // DELETE api/<EstimacionController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var estimacion = await _context.Estimaciones.FindAsync(id);
                if (estimacion != null)
                {
                    estimacion.Activo = !estimacion.Activo;

                    _context.Update(estimacion);
                    await _context.SaveChangesAsync();

                    return NoContent();
                }

                return BadRequest();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
