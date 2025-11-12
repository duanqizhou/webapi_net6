using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using webapi.Common;
using webapi.Dtos.Lis;

namespace webapi.Services
{
    public interface IDictionaryService
    {
        Task<SjxmDropdownsDto> GetSjxmDropdownsAsync();
    }
    public class DropdownItem
    {
        public string Value { get; set; } = string.Empty;
        public string Label { get; set; } = string.Empty;
        public bool Disabled { get; set; }
    }
    public class DictionaryService : IDictionaryService
    {
        private readonly ISamTypeServices _samTypeServices;
        private readonly ISamCheckTypeServices _samCheckTypeServices;
        private readonly ISamSampleTypeServices _samSampleTypeServices;

        public DictionaryService(
            ISamTypeServices samTypeServices,
            ISamCheckTypeServices samCheckTypeServices,
            ISamSampleTypeServices samSampleTypeServices)
        {
            _samTypeServices = samTypeServices;
            _samCheckTypeServices = samCheckTypeServices;
            _samSampleTypeServices = samSampleTypeServices;
        }

        public async Task<SjxmDropdownsDto> GetSjxmDropdownsAsync()
        {
            // 并行执行所有查询
            var jglxTask = _samTypeServices.GetListExpressionAsync(new List<string> { DictionaryConstants.ResultType });
            var cydwTask = _samTypeServices.GetListExpressionAsync(new List<string> { DictionaryConstants.CommonUnit });
            var dylxTask = _samTypeServices.GetListExpressionAsync(new List<string> { DictionaryConstants.PrintType });
            var jyffTask = _samTypeServices.GetListExpressionAsync(new List<string> { DictionaryConstants.TestMethod });
            var xswsTask = _samTypeServices.GetListExpressionAsync(new List<string> { DictionaryConstants.DecimalPlaces });
            var jylxTask = _samCheckTypeServices.GetListExpressionAsync(_ => true);
            var yblxTask = _samSampleTypeServices.GetListExpressionAsync(_ => true);

            await Task.WhenAll(jglxTask, cydwTask, dylxTask, jyffTask, jylxTask, yblxTask, xswsTask);

            // 构建返回结果
            return new SjxmDropdownsDto
            {
                Jglx = ToDropdownItems(jglxTask.Result, x => x.fcode),
                Cydw = ToDropdownItems(cydwTask.Result, x => x.fname),
                Dylx = ToDropdownItems(dylxTask.Result, x => x.fcode),
                Jyff = ToDropdownItems(jyffTask.Result, x => x.fcode),
                Xsws = ToDropdownItems(xswsTask.Result, x => x.fcode),
                Jylx = ToDropdownItems(jylxTask.Result, x => x.fcheck_type_id.ToString()),
                Yblx = ToDropdownItems(yblxTask.Result, x => x.fsample_type_id.ToString()) // 确保转为string
            };
        }

        private List<DropdownItem> ToDropdownItems<T>(IEnumerable<T> items, Func<T, string> valueSelector)
        {
            if (items == null)
                return new List<DropdownItem>();

            return items.Select(x => new DropdownItem
            {
                Value = valueSelector(x),
                Label = GetLabel(x),
                Disabled = false
            }).ToList();
        }

        private string GetLabel<T>(T item)
        {
            if (item == null)
                return string.Empty;

            // 尝试获取fname属性
            var fnameProperty = typeof(T).GetProperty("fname");
            if (fnameProperty != null)
            {
                return fnameProperty.GetValue(item)?.ToString() ?? string.Empty;
            }

            // 尝试获取Name属性
            var nameProperty = typeof(T).GetProperty("Name");
            if (nameProperty != null)
            {
                return nameProperty.GetValue(item)?.ToString() ?? string.Empty;
            }

            // 如果都没有，返回空字符串而不是调用ToString()
            return string.Empty;
        }
    }

}