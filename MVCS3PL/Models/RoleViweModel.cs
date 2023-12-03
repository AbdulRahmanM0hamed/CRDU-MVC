using System;

namespace MVCS3PL.Models
{
    public class RoleViweModel
    {
        public string Id { get; set; }
        public string RoleName { get; set; }


        public RoleViweModel()
        {
            Id = Guid.NewGuid().ToString();
        }

    }
}
