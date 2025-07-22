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

        public string Username { get; set; }
        public string Password { get; set; }
        //验证码
        public string code { get; set; }
    }
}
