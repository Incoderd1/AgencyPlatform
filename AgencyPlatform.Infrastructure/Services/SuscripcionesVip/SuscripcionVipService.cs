using AgencyPlatform.Application.DTOs.SuscripcionesVip;
using AgencyPlatform.Application.Interfaces.Services.SuscripcionesVip;
using AgencyPlatform.Infrastructure.Data.Entities;
using AgencyPlatform.Infrastructure.Repositories;
using Newtonsoft.Json;

namespace AgencyPlatform.Infrastructure.Services.SuscripcionesVip
{
    public class SuscripcionVipService : ISuscripcionVipService
    {
        private readonly ISuscripcionVipRepository _repository;

        public SuscripcionVipService(ISuscripcionVipRepository repository)
        {
            _repository = repository;
        }

        public async Task<List<SuscripcionVipDto>> ObtenerTodasAsync()
        {
            var lista = await _repository.GetAllAsync();
            return lista.Select(MapToDto).ToList();
        }

        public async Task<SuscripcionVipDto?> ObtenerPorIdAsync(int id)
        {
            var entidad = await _repository.GetByIdAsync(id);
            return entidad is null ? null : MapToDto(entidad);
        }

        public async Task<SuscripcionVipDto> CrearAsync(CrearSuscripcionVipDto dto)
        {
            var entidad = new Data.Entities.SuscripcionesVip
            {
                IdCliente = dto.IdCliente,
                IdPlan = dto.IdPlan,
                NumeroSuscripcion = dto.NumeroSuscripcion,
                FechaInicio = dto.FechaInicio,
                FechaFin = dto.FechaFin,
                FechaRenovacion = dto.FechaRenovacion,
                FechaProximoCargo = dto.FechaProximoCargo,
                TipoCiclo = dto.TipoCiclo,
                MontoBase = dto.MontoBase,
                MontoDescuento = dto.MontoDescuento,
                Impuestos = dto.Impuestos,
                MontoPagado = dto.MontoPagado,
                Moneda = dto.Moneda,
                AutoRenovacion = dto.AutoRenovacion,
                IntentosCobro = dto.IntentosCobro,
                FechaUltimoIntento = dto.FechaUltimoIntento,
                Estado = dto.Estado,
                OrigenSuscripcion = dto.OrigenSuscripcion,
                CuponAplicado = dto.CuponAplicado,
                MetodoPago = dto.MetodoPago,
                GatewayPago = dto.GatewayPago,
                IdClienteGateway = dto.IdClienteGateway,
                IdSuscripcionGateway = dto.IdSuscripcionGateway,
                DatosPago = dto.DatosPago != null ? JsonConvert.SerializeObject(dto.DatosPago) : null,
                ReferenciaPago = dto.ReferenciaPago,
                IdTransaccionPago = dto.IdTransaccionPago,
                FechaCancelacion = dto.FechaCancelacion,
                EfectivaHasta = dto.EfectivaHasta,
                MotivoCancelacion = dto.MotivoCancelacion,
                SolicitadaPor = dto.SolicitadaPor,
                NotasInternas = dto.NotasInternas,
                HaRecibidoRecordatorio = dto.HaRecibidoRecordatorio
            };

            await _repository.AddAsync(entidad);
            await _repository.SaveChangesAsync();

            return MapToDto(entidad);
        }

        public async Task<SuscripcionVipDto?> ActualizarAsync(int id, UpdateSuscripcionVipDto dto)
        {
            var entidad = await _repository.GetByIdAsync(id);
            if (entidad == null) return null;

            entidad.Estado = dto.Estado ?? entidad.Estado;
            entidad.FechaRenovacion = dto.FechaRenovacion ?? entidad.FechaRenovacion;
            entidad.FechaProximoCargo = dto.FechaProximoCargo ?? entidad.FechaProximoCargo;
            entidad.IntentosCobro = dto.IntentosCobro ?? entidad.IntentosCobro;
            entidad.FechaUltimoIntento = dto.FechaUltimoIntento ?? entidad.FechaUltimoIntento;
            entidad.AutoRenovacion = dto.AutoRenovacion ?? entidad.AutoRenovacion;
            entidad.NotasInternas = dto.NotasInternas ?? entidad.NotasInternas;

            _repository.Update(entidad);
            await _repository.SaveChangesAsync();

            return MapToDto(entidad);
        }

        public async Task<bool> EliminarAsync(int id)
        {
            var entidad = await _repository.GetByIdAsync(id);
            if (entidad == null) return false;

            _repository.Delete(entidad);
            await _repository.SaveChangesAsync();
            return true;
        }

        private static SuscripcionVipDto MapToDto(Data.Entities.SuscripcionesVip s) => new()
        {
            IdSuscripcion = s.IdSuscripcion,
            IdCliente = s.IdCliente,
            IdPlan = s.IdPlan,
            NumeroSuscripcion = s.NumeroSuscripcion,
            FechaInicio = s.FechaInicio,
            FechaFin = s.FechaFin,
            FechaRenovacion = s.FechaRenovacion,
            FechaProximoCargo = s.FechaProximoCargo,
            TipoCiclo = s.TipoCiclo,
            MontoBase = s.MontoBase,
            MontoDescuento = s.MontoDescuento,
            Impuestos = s.Impuestos,
            MontoPagado = s.MontoPagado,
            Moneda = s.Moneda,
            AutoRenovacion = s.AutoRenovacion,
            IntentosCobro = s.IntentosCobro,
            FechaUltimoIntento = s.FechaUltimoIntento,
            Estado = s.Estado,
            OrigenSuscripcion = s.OrigenSuscripcion,
            CuponAplicado = s.CuponAplicado,
            MetodoPago = s.MetodoPago,
            GatewayPago = s.GatewayPago,
            IdClienteGateway = s.IdClienteGateway,
            IdSuscripcionGateway = s.IdSuscripcionGateway,
            DatosPago = string.IsNullOrWhiteSpace(s.DatosPago)
    ? null
    : JsonConvert.DeserializeObject<DatosPagoDto>(s.DatosPago),
            ReferenciaPago = s.ReferenciaPago,
            IdTransaccionPago = s.IdTransaccionPago,
            FechaCancelacion = s.FechaCancelacion,
            EfectivaHasta = s.EfectivaHasta,
            MotivoCancelacion = s.MotivoCancelacion,
            SolicitadaPor = s.SolicitadaPor,
            NotasInternas = s.NotasInternas,
            HaRecibidoRecordatorio = s.HaRecibidoRecordatorio
        };
    }
}
