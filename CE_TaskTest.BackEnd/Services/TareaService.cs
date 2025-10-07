using CE_TaskTest.BackEnd.Context;
using CE_TaskTest.BackEnd.Models;
using Microsoft.EntityFrameworkCore;

namespace CE_TaskTest.BackEnd.Services
{
    public class TareaService(ApplicationDbContext context)
    {
        public List<Tarea> GetCompletadas()
        {
            return context.Tareas.Where(t => t.Completado).ToList();
        }
    }
}