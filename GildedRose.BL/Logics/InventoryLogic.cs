using AutoMapper;
using GildedRose.DL.UoW;
using GildedRose.DTO;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GildedRose.BL.Logics
{
    /// <summary>
    /// Inventory Logic Class
    /// </summary>
    public class InventoryLogic : IInventoryLogic
    {
        /// <summary>
        /// Local Property for mapper
        /// </summary>
        public IMapper _mapper { get; private set; }

        /// <summary>
        /// Local property for unit of work
        /// </summary>
        public IUnitOfWork _unitOfWork { get; private set; }

        /// <summary>
        /// Constructor for DI
        /// </summary>
        /// <param name="mapper">Mapper instance</param>
        /// <param name="unitOfWork">UnitofWork instance</param>
        public InventoryLogic(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Get all Inventory Asynchronous.
        /// </summary>
        /// <returns>IEnumerable of Inventories</returns>
        public async Task<IEnumerable<InventoryModel>> GetAllAsync()
        {
            var inventories = await _unitOfWork.InventoryRepository.GetAllAsync();

            return _mapper.Map<IEnumerable<InventoryModel>>(inventories);
        }
    }
}
