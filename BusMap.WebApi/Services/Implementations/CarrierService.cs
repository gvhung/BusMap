using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto.Carriers;
using BusMap.WebApi.Repositories.Abstract;
using BusMap.WebApi.Services.Abstract;

namespace BusMap.WebApi.Services.Implementations
{
    public class CarrierService : ICarrierService
    {
        private readonly ICarrierRepository _carrierRepository;
        private readonly IMapper _mapper;

        public CarrierService(ICarrierRepository carrierRepository, IMapper mapper)
        {
            _carrierRepository = carrierRepository;
            _mapper = mapper;
        }

        public async Task<CarriersCarrierDto> GetCarrierAsync(int id)
        {
            var carrier = await _carrierRepository.GetCarrierAsync(id);
            return _mapper.Map<Carrier, CarriersCarrierDto>(carrier);
        }

        public async Task<CarriersCarrierDto> GetCarrierIncludeRoutesAsync(int id)
        {
            var carrier = await _carrierRepository.GetCarrierIncludeRoutesAsync(id);
            return _mapper.Map<Carrier, CarriersCarrierDto>(carrier);
        }

        public async Task<CarriersCarrierDto> GetCarrierIncludeRoutesBusStopsAsync(int id)
        {
            var carrier = await _carrierRepository.GetCarrierIncludeRoutesBusStopsAsync(id);
            return _mapper.Map<Carrier, CarriersCarrierDto>(carrier);
        }

        public async Task<IEnumerable<CarriersCarrierDto>> GetAllCarriersAsync()
        {
            var carriers = await _carrierRepository.GetAllCarriersAsync();
            return _mapper.Map<IEnumerable<Carrier>, IEnumerable<CarriersCarrierDto>>(carriers);
        }

        public async Task<IEnumerable<CarriersCarrierDto>> GetAllCarriersIncludeRoutesAsync()
        {
            var carriers = await _carrierRepository.GetAllCarriersIncludeRoutesAsync();
            return _mapper.Map<IEnumerable<Carrier>, IEnumerable<CarriersCarrierDto>>(carriers);
        }

        public async Task<IEnumerable<CarriersCarrierDto>> GetAllCarriersIncludeRoutesBusStopsAsync()
        {
            var carriers = await _carrierRepository.GetAllCarriersIncludeRoutesBusStopsAsync();
            return _mapper.Map<IEnumerable<Carrier>, IEnumerable<CarriersCarrierDto>>(carriers);
        }

        public async Task AddCarrierAsync(Carrier carrier)
            => await _carrierRepository.AddCarrierAsync(carrier);

        public async Task AddCarrierRangeAsync(IEnumerable<Carrier> carriers)
            => await _carrierRepository.AddCarrierRangeAsync(carriers);

        public async Task RemoveCarrierAsync(CarriersCarrierDto carrier)
        {
            var carrierToRemove = await _carrierRepository.GetCarrierAsync(carrier.Id);
            await _carrierRepository.RemoveCarrierAsync(carrierToRemove);
        } 
    }
}
