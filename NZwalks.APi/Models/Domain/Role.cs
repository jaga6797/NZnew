﻿namespace NZwalks.APi.Models.Domain
{
    public class Role
    {
        public Guid  Id { get; set; }
        public string Name { get; set; }
        //nav prop

        public List<User_Role> UserRoles { get; set; }
    }
}
