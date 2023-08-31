using Business.Abstract;
using Business.ValidationRules.FluentValidation;
using Core.Utils;
using Core.Validation;
using Core.Utils.Results;
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
    public class UserService : IUserService
    {
        private readonly UserRepositoryBase _userRepository;
        public UserService(UserRepositoryBase userRepository)
        {
            _userRepository = userRepository;
        }

        public IDataResult<UserResponse> Add(UserRequest data)
        {
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
            throw new NotImplementedException();
        }

        public IDataResult<UserResponse> Get(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<UserResponse>> GetAll(int page = 0, int pageSize = 0)
        {
            throw new NotImplementedException();
        }


        public IResult Update(int id, UserRequest data)
        {
            throw new NotImplementedException();
        }

        public LoginResponse GetByMail(string email)
        {
            var user = _userRepository.Get(x => x.UserEmail == email);

            if(user != null)
            {
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
            return null;

           
        }
    }
}
