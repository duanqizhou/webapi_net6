using System;

namespace webapi.Models
{
    public class UserDto
    {

        public string Username { get; set; }
        public string Password { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
    }

    public class LoginUserDto
    {

        public string EMPID { get; set; }
        public string Password { get; set; }
        //验证码
        public string code { get; set; }
    }
    public class UserQueryDto
    {
        public int CurrentPage { get; set; } = 1;
        public int Size { get; set; } = 10;
        public string? Username { get; set; }
        public string? Phone { get; set; }
    }

    public class TableUserData
    {
        public string loginid { get; set; }
        public string category { get; set; }
        public string name { get; set; }
        public int attributes { get; set; }
        public string logincount { get; set; }
        public string description { get; set; }
        public string empid { get; set; }
        public string doctorcode { get; set; }
    }

    public class CreateOrUpdateUserDto
    {
        public string loginid { get; set; }
        public string password { get; set; }
        public string? category { get; set; }
        public string? description { get; set; }
        public string? name { get; set; }
        public string? doctorcode { get; set; }

    }

    public class UpdateStatusUserDto
    {
        public string Loginid { get; set; }
        public int Status { get; set; } // 是否启用
    }

}
