namespace CE_TaskTest.BackEnd.Services.Dtos.Response
{
    public record EstimacionResponseDto
    {
        public int Id { get; init; }
        public int Duracion { get; init; }
        public bool Activo { get; init; }
    }
}
