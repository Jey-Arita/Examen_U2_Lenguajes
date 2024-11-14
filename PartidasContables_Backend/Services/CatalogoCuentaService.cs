using AutoMapper;
using Microsoft.EntityFrameworkCore;
using PartidasContables.DataBase;
using PartidasContables.DataBase.Entities;
using PartidasContables.Dtos.CatalogoCuenta;
using PartidasContables.Dtos.Common;
using PartidasContables.Services.Interface;

namespace PartidasContables.Services
{
    public class CatalogoCuentaService : ICatalogoCuentaService
    {
        private readonly PartidaDbContext _context;
        private readonly IMapper _mapper;

        public CatalogoCuentaService(PartidaDbContext context, IMapper mapper)
        {
            this._context = context;
            this._mapper = mapper;
        }
        public async Task<ResponseDto<List<CatalogoCuentaDto>>> ListCatalogoCuentaAsync()
        {
            var entities = await _context.CatalogoCuentas.Include(c => c.CuentasHijas).ToListAsync();
            var dtos = _mapper.Map<List<CatalogoCuentaDto>>(entities);

            return new ResponseDto<List<CatalogoCuentaDto>>
            {
                StatusCode = 200,
                Status = true,
                Message = "Listado de cuentas obtenido exitosamente",
                Data = dtos
            };
        }
        public async Task<ResponseDto<CatalogoCuentaDto>> CrearCatalogoCuentaAsync(CatalogoCuentaCreateDto catalogoCuentaDto)
        {
            var entity = _mapper.Map<CatalogoCuentaEntity>(catalogoCuentaDto);

            _context.CatalogoCuentas.Add(entity);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<CatalogoCuentaDto>(entity);
            return new ResponseDto<CatalogoCuentaDto>
            {
                StatusCode = 201,
                Status = true,
                Message = "Cuenta creada exitosamente",
                Data = dto 
            };
        }
        public async Task<ResponseDto<CatalogoCuentaDto>> EditAsync(CatalogoCuentaEditDto catalogoCuentaEditDto, Guid id)
        {
            var entity = await _context.CatalogoCuentas.FirstOrDefaultAsync(e => e.Id == id);
            if (entity == null)
                return new ResponseDto<CatalogoCuentaDto>
                {
                    StatusCode = 404,
                    Status = false,
                    Message = "Cuenta no encontrada"
                };

            _mapper.Map(catalogoCuentaEditDto, entity);
            _context.CatalogoCuentas.Update(entity);
            await _context.SaveChangesAsync();

            var dto = _mapper.Map<CatalogoCuentaDto>(entity);
            return new ResponseDto<CatalogoCuentaDto>
            {
                StatusCode = 200,
                Status = true,
                Message = "Cuenta actualizada exitosamente",
                Data = dto
            };
        }
        public async Task<ResponseDto<CatalogoCuentaDto>> DeleteAsync(Guid id)
        {
        var entity = await _context.CatalogoCuentas.FirstOrDefaultAsync(x => x.Id == id);
        if (entity == null)
            return new ResponseDto<CatalogoCuentaDto> 
            { 
                StatusCode = 401,
                Status = false, 
                Message = "Cuenta no encontrada" };

        _context.CatalogoCuentas.Remove(entity);
        await _context.SaveChangesAsync();

        return new ResponseDto<CatalogoCuentaDto> 
        { 
            StatusCode = 200,
            Status = true,
            Message = "Cuenta eliminada exitosamente" 
        };
    }
    }
}

