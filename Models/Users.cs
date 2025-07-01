using System;
using System.Linq;
using System.Text;
using SqlSugar;

namespace webapi.Models
{
    ///<summary>
    ///
    ///</summary>
    [SugarTable("Users")]
    public partial class Users
    {
           public Users(){


           }
           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:False
           /// </summary>           
           [SugarColumn(IsPrimaryKey=true)]
           public int Id {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string Username {get;set;}

           /// <summary>
           /// Desc:
           /// Default:
           /// Nullable:True
           /// </summary>           
           public string PasswordHash {get;set;}

    }
}
