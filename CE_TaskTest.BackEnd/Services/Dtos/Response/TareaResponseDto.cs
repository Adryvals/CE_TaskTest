using CE_TaskTest.BackEnd.Models;

namespace CE_TaskTest.BackEnd.Services.Dtos.Response
{
    public record TareaResponseDto
    {
        public int Id { get; set; }
        public required string Descripcion { get; set; }
        public DateOnly FechaTarea { get; set; }
        public int Visibilidad { get; set; }
        public int Estado { get; set; }
        public int EstimacionId { get; set; }
        public bool Completado { get; set; }
    }
}
