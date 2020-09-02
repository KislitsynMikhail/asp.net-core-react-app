using QMPT.Data.Models;
using QMPT.Models.Responses.Helpers.RemovingModel;
using System;

namespace QMPT.Models.Responses
{
    public class UserResponse : BaseModelResponse, IRemovableResponse
    {
        public string Login { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        #region Removable
        public int? RemoverId { get; set; }
        public DateTime? RemovalTime { get; set; }
        public bool IsRemoved { get; set; }
        #endregion Removable

        public UserResponse(User user) : base(user)
        {
            Login = user.Login;
            FirstName = user.FirstName;
            LastName = user.LastName;

            RemovableModelResponseFiller.Fill(this, user);
        }
    }
}
