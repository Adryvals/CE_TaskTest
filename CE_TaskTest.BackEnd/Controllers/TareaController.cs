using AutoMapper;
using CE_TaskTest.BackEnd.Context;
using CE_TaskTest.BackEnd.Models;
using CE_TaskTest.BackEnd.Services;
using CE_TaskTest.BackEnd.Services.Dtos.Request;
using CE_TaskTest.BackEnd.Services.Dtos.Response;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CE_TaskTest.BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TareaController(ApplicationDbContext _context, IMapper mapper, IValidator<TareaRequestDto>? validator = null, TareaService? service = null) : ControllerBase
    {
        // GET: api/<TaskController>
        [HttpGet]
        public async Task<ActionResult<List<TareaResponseDto>>> Get()
        {
            try
            {
                var tareas = await _context.Tareas.ToListAsync();
                if (tareas.Count != 0)
                {
                    var tareasResponse = mapper.Map<List<TareaResponseDto>>(tareas);
                    return Ok(tareasResponse);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }

            return NotFound("No hay tareas registradas.");
        }

        // GET api/<TaskController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TareaResponseDto>> GetById(int id)
        {
            try
            {
                var tarea = await _context.Tareas.FindAsync(id);
                if (tarea != null)
                {
                    var tareaResponse = mapper.Map<TareaResponseDto>(tarea);
                    return Ok(tareaResponse);
                }
            }
            catch (Exception)
            {
                return StatusCode(500, "Error del servidor");
            }

            return NotFound($"No se encontró la tarea con ID {id}.");
        }

        // GET api/<TaskController>/completed
        [HttpGet("Completed")]
        public ActionResult<List<TareaResponseDto>> CompletedTasks()
        {
            var tareas = service!.GetCompletadas();

            if (tareas.Any())
            {
                var tareasResponse = mapper.Map<List<TareaResponseDto>>(tareas);
                return Ok(tareasResponse);
            }

            return NotFound("no existen tareas completadas");
        }

        // POST api/<TaskController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TareaRequestDto tareaRequestDto)
        {
            try
            {
                var validationResult = await validator!.ValidateAsync(tareaRequestDto);
                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors);
                }

                var tareas = _context.Tareas.Any(x => x.Descripcion == tareaRequestDto.Descripcion && x.FechaTarea == tareaRequestDto.FechaTarea);
                if (tareas)
                {
                    return BadRequest("La tarea ya existe");
                }

                var tarea = mapper.Map<Tarea>(tareaRequestDto);

                await _context.AddAsync(tarea);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetById), new { id = tarea.Id }, tarea);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT api/<TaskController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TareaRequestDto tarea)
        {
            try
            {
                var tareaItem = await _context.Tareas.FindAsync(id);
                if (tareaItem != null)
                {
                    if (_context.Tareas.Any(x => x.Id != id && x.Descripcion == tarea.Descripcion && x.FechaTarea == tarea.FechaTarea))
                    {
                        return BadRequest("La tarea ya existe");
                    }

                    tareaItem.Estado = tarea.Estado;
                    tareaItem.EstimacionId = tarea.EstimacionId;
                    tareaItem.Completado = tarea.Completado;
                    tareaItem.Descripcion = tarea.Descripcion;
                    tareaItem.FechaTarea = tarea.FechaTarea;
                    tareaItem.Visibilidad = tarea.Visibilidad;

                    _context.Update(tareaItem);
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

        // PATCH api/<TaskController>/5
        [HttpPatch("UpdateAchievedState")]
        public async Task<IActionResult> AchievedState(int id)
        {
            try
            {
                var tarea = await _context.Tareas.FindAsync(id);
                if (tarea != null)
                {
                    tarea.Completado = !tarea.Completado;

                    _context.Update(tarea);
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
