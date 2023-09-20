using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace EFCorePeliculas.Entidades.Configuraciones
{
    public class PagoConfig : IEntityTypeConfiguration<Pago>
    {
        public void Configure(EntityTypeBuilder<Pago> builder)
        {
            builder.HasDiscriminator(x => x.TipoPago)
                .HasValue<PagoPaypal>(TipoPago.Paypal)
                .HasValue<PagoTarjeta>(TipoPago.Tarjeta);

            builder.Property(x => x.Monto).HasPrecision(18, 2);
        }
    }
}
