using SqlSugar;

namespace webapi.Tools;

public class DbFirstGenerator
{
    public static void Generate(ISqlSugarClient db)
    {
        db.DbFirst
          .IsCreateAttribute(true) // 添加特性标记
          //.Where("User", "Order") // 可选：只生成指定表
          .CreateClassFile("Models", "webapi.Models");

        Console.WriteLine("✅ 实体类已生成！");
    }
}
