using Business.Abstract;
using Core.Utils;
using DataAccess.EF.Abstract;
using DataAccess.EF.Concrete;
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
    public class UserService : IUserService
    {
        private readonly UserRepositoryBase _userRepository;
        public UserService(UserRepositoryBase userRepository)
        {
            _userRepository = userRepository;
        }
        public IDataResult<UserResponse> Add(UserRequest data)
        {
            var user = _userRepository.Get(x => x.UserEmail == data.UserEmail);
            if (user != null)
            {
                return new ErrorDataResult<UserResponse>(default, "E posta kayıtlı.");
            }

            var entity = new User()
            {
                CreateDate = DateTime.Now,
                EditDate = DateTime.Now,
                UserAddress = data.UserAddress,
                UserEmail = data.UserEmail,
                UserFirstName = data.UserFirstName,
                UserLastName = data.UserLastName,
                UserName = data.UserName,
                UserPasswordHash = data.UserPasswordHash,
                UserPasswordSalt = data.UserPasswordSalt,
                UserRole = data.UserRole,

            };
          


            _userRepository.Add(entity);

            UserResponse userResponse = new UserResponse()
            {
                UserRole = entity.UserRole,
                CreateDate = entity.CreateDate,
                UserAddress = entity.UserAddress,
                UserEmail = entity.UserEmail,
                UserFirstName = entity.UserFirstName,
                UserLastName = entity.UserLastName,
                UserName = entity.UserName,
                UserId = entity.UserId,
            };
            return new SuccessDataResult<UserResponse>(userResponse,"Kullanıcı Kaydedildi.");
        }

        public IResult Delete(int id)
        {
            var user = _userRepository.Get(x => x.UserId == id);
            _userRepository.Delete(user);
            return new SuccessResult("Kullanıcı Silindi.");
        }

        public IDataResult<UserResponse> Get(int id)
        {
            var user = _userRepository.Get(x => x.UserId == id);
            // Kategori nesnesi boş gelme durumu değerlendirilecek.
            var userResponse = new UserResponse()
            {
                UserAddress = user.UserAddress,
                UserEmail = user.UserEmail,
                UserFirstName = user.UserFirstName,
                UserLastName = user.UserLastName,
                UserName = user.UserName,
                UserRole = user.UserRole,

            };

            return new SuccessDataResult<UserResponse>(userResponse, "Kullanıcı bilgisi getirildi.");
        }

        public IDataResult<List<UserResponse>> GetAll()
        {
            var userList = _userRepository.GetAll().Select(x => new UserResponse()
            {
                UserAddress = x.UserAddress,
                UserEmail = x.UserEmail,
                UserFirstName = x.UserFirstName,
                UserLastName = x.UserLastName,
                UserName = x.UserName,
                UserRole = x.UserRole,

            }).ToList();

            return new SuccessDataResult<List<UserResponse>>(userList," Kullanıcılara ait tüm ürün bilgileri getirildi.");
        }

     

        public IResult Update(int id, UserRequest data)
        {
            var user = _userRepository.Get(x => x.UserId == id);
            user.CreateDate = DateTime.Now;
            user.EditDate = DateTime.Now;
            user.UserAddress = data.UserAddress;
            user.UserEmail = data.UserEmail;
            user.UserFirstName = data.UserFirstName;
            user.UserLastName = data.UserLastName;
            user.UserName = data.UserName;
            user.UserPasswordHash = data.UserPasswordHash;
            user.UserPasswordSalt = data.UserPasswordSalt;
            user.UserRole = data.UserRole;


            return new SuccessResult("Kullanıcı bilgileri güncellendi.");
        }

        public LoginResponse GetByMail(string email)
        {
            var user = _userRepository.Get(x => x.UserEmail == email);
            LoginResponse loginResponse = new LoginResponse()
            {
                UserEmail = user.UserEmail,
                UserFirstName = user.UserName,
                UserAddress = user.UserAddress,
                UserLastName = user.UserLastName,
                UserName = user.UserName,
                UserRole = user.UserRole,
                UserId = user.UserId,
                UserPasswordHash = user.UserPasswordHash,
                UserPasswordSalt = user.UserPasswordSalt
            };

            return loginResponse;

           
        }
    }
}
