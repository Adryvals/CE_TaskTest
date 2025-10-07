namespace CE_TaskTest.BackEnd.Services.Dtos.Request
{
    public record TareaRequestDto
    {
        public required string Descripcion { get; set; }
        public DateOnly FechaTarea { get; set; }
        public int Visibilidad { get; set; }
        public int Estado { get; set; }
        public int EstimacionId { get; set; }
        public bool Completado { get; set; }
    }
}
