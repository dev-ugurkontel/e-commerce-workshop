using Business.Abstract;
using Core.Utils;
using DataAccess.EF.Abstract;
using Entities.Entity;
using Entities.Surrogate.Request;
using Entities.Surrogate.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class CartService : ICartService
    {
        private readonly CartRepositoryBase _cartRepository;
        public CartService(CartRepositoryBase cartRepository)
        {
            _cartRepository = cartRepository;
        }
        public IDataResult<List<CartResponse>> GetAll()
        {
            var cartList = _cartRepository.GetAll().Select(c => new CartResponse()
            {
                TotalltemPrice = c.TotalltemPrice,
                TotalltemQuantity = c.TotalltemQuantity,
                CreateDate = c.CreateDate,
                EditDate = c.EditDate
            }).ToList();

            return new SuccessDataResult<List<CartResponse>>(cartList, "Sepet bilgileri getirildi.");
        }

        public IDataResult<CartResponse> Get(int id)
        {

            try
            {
                var getCart = _cartRepository.Get(c => c.CartId == id);

                if (getCart == null)
                {
                    return new ErrorDataResult<CartResponse>(default, "Kayıt bulunamadı.");
                }
                var CartResponse = new CartResponse()
                {
                    TotalltemPrice = getCart.TotalltemPrice,
                    TotalltemQuantity = getCart.TotalltemQuantity,
                    CreateDate = getCart.CreateDate,
                    EditDate = getCart.EditDate
                };
                return new SuccessDataResult<CartResponse>(CartResponse);
            }
            catch (Exception ex)
            {
                return new ExceptionDataResult<CartResponse>();
            }


        }

        public IResult Add(CartRequest data)
        {
            var entity = new Cart()
            {
                TotalltemPrice = data.TotalltemPrice,
                TotalltemQuantity = data.TotalltemQuantity,
                CreateDate = DateTime.Now,
                EditDate = data.EditDate
            };
            _cartRepository.Add(entity);
            return new SuccessResult("Sepet başarıyla kaydedildi.");
        }

        public IResult Update(int id, CartRequest data)
        {
            var cart = _cartRepository.Get(c => c.CartId == id);
            cart.TotalltemPrice = data.TotalltemPrice;
            cart.TotalltemQuantity = data.TotalltemQuantity;
            cart.EditDate = DateTime.Now;
            _cartRepository.Update(cart);
            return new SuccessResult("Sepet başarıyla güncellendi");
        }

        public IResult Delete(int id)
        {
            var cart = _cartRepository.Get(c => c.CartId == id);
            _cartRepository.Delete(cart);
            return new SuccessResult("Sepet başarıyla silindi.");

        }
    }
}
