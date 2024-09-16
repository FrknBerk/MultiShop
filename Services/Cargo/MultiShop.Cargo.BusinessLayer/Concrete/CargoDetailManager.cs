using MultiShop.Cargo.BusinessLayer.Abstract;
using MultiShop.Cargo.DataAccessLayer.Abstract;
using MultiShop.Cargo.EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MultiShop.Cargo.BusinessLayer.Concrete
{
    public class CargoDetailManager : ICargoDetailService
    {
        private readonly ICargoDetailDal _cargoDetaildal;

        public CargoDetailManager(ICargoDetailDal cargoDetaildal)
        {
            _cargoDetaildal = cargoDetaildal;
        }

        public void TDelete(int id)
        {
            _cargoDetaildal.Delete(id);
        }

        public List<CargoDetail> TGetAll()
        {
            return _cargoDetaildal.GetAll();
        }

        public CargoDetail TGetById(int id)
        {
            return _cargoDetaildal.GetById(id);
        }

        public void TInsert(CargoDetail entity)
        {
            _cargoDetaildal.Insert(entity);
        }

        public void TUpdate(CargoDetail entity)
        {
            _cargoDetaildal.Update(entity);
        }
    }
}
