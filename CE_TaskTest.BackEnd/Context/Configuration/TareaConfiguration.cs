using CE_TaskTest.BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CE_TaskTest.BackEnd.Context.Configuration
{
    public class TareaConfiguration : IEntityTypeConfiguration<Tarea>
    {
        public void Configure(EntityTypeBuilder<Tarea> builder)
        {
            builder.Property(x => x.Descripcion).IsRequired();
            builder.Property(x => x.FechaTarea).IsRequired();
            builder.Property(x => x.Visibilidad).IsRequired();
            builder.Property(x => x.Estado).IsRequired();
            builder.Property(x => x.EstimacionId).IsRequired();
        }
    }
}
