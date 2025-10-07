using CE_TaskTest.BackEnd.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CE_TaskTest.BackEnd.Context.Configuration
{
    public class EstimacionConfiguration : IEntityTypeConfiguration<Estimacion>
    {
        public void Configure(EntityTypeBuilder<Estimacion> builder)
        {
            builder.Property(x => x.Activo).ValueGeneratedOnAdd().HasDefaultValue(true).IsRequired();
            builder.Property(x => x.Duracion).IsRequired();
        }
    }
}
