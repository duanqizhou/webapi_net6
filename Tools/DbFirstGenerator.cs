using SqlSugar;

namespace webapi.Tools;

public class DbFirstGenerator
{
    public static void Generate(SqlSugarScope db)
    {
        // BaseData 库的实体
        var baseDataDb = db.GetConnectionScope("BaseData");
        baseDataDb.DbFirst
            .IsCreateAttribute(true)
            .CreateClassFile("Models/BaseData", "webapi.Models.BaseData");

        // LIS 库的实体
        var lisDb = db.GetConnectionScope("LIS");
        lisDb.DbFirst
            .IsCreateAttribute(true)
            .CreateClassFile("Models/LIS", "webapi.Models.LIS");

        Console.WriteLine("✅ 实体类已生成到 Models/BaseData 和 Models/LIS 文件夹！");
    }
}
