using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BusMap.WebApi.DatabaseModels;
using BusMap.WebApi.Dto;
using BusMap.WebApi.Repositories.Abstract;

namespace BusMap.WebApi.Services
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

        public async Task<CarrierDto> GetCarrierAsync(int id)
        {
            var carrier = await _carrierRepository.GetCarrierAsync(id);
            return _mapper.Map<Carrier, CarrierDto>(carrier);
        }

        public async Task<IEnumerable<CarrierDto>> GetAllCarriersAsync()
        {
            var carriers = await _carrierRepository.GetAllCarriers();
            return _mapper.Map<IEnumerable<Carrier>, IEnumerable<CarrierDto>>(carriers);
        }

        public async Task AddCarrierAsync(Carrier carrier)
            => await _carrierRepository.AddCarrierAsync(carrier);

        public async Task AddCarrierRangeAsync(IEnumerable<Carrier> carriers)
            => await _carrierRepository.AddCarrierRangeAsync(carriers);

        public async Task RemoveCarrierAsync(CarrierDto carrier)
        {
            var carrierToRemove = await _carrierRepository.GetCarrierAsync(carrier.Id);
            await _carrierRepository.RemoveCarrier(carrierToRemove);
        } 
    }
}
