using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngolaPrev.VivaEstetica.MVC.Services.User.Result
{
    public class AddUserResult
    {
        public AddUserResult(bool isSuccess, IEnumerable<string> errors)
        {
            IsSuccess = isSuccess;
            Errors = errors;
        }

        public bool IsSuccess { get; }
        public IEnumerable<string> Errors { get; }
    }
}