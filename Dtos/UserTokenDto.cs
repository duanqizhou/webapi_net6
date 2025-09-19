
namespace webapi.Dtos
{
    ///<summary>
    ///
    ///</summary>
    public class UserTokenDto
    {

           public int UserId {get;set;}
       
           public string RefreshToken {get;set;}

           public DateTime ExpireAt {get;set;}

    }
}
