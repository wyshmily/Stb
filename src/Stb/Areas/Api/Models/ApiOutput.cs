using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Stb.Api.Models.AuthViewModels;

namespace Stb.Api.Models
{
    public class ApiOutput<T>
    {
        /// <summary>
        /// 返回代码<br />
        /// Api本身调用成功时（HttpStatusCode = 200）<br />
        /// <ul>
        ///     <li>A0000 - 执行成功，没有错误</li>
        ///     <li>B0000 - 已知错误，Msg包含错误信息</li>
        ///     <li>E0000 - 未知错误，Msg包含错误信息，但可读性较低，并且是英文</li>
        /// </ul>
        /// 在一些情况下Api可能返回其它状态码，包括<br />
        /// <ul>
        ///     <li>501 - UnAuthorized，权限认证失败时返回UnAuthorized</li>
        ///     <li>404 - NotFound，没有此接口或者遗漏必须的调用参数时返回NotFound</li>
        ///     <li>其它 - 如果有的话再补充</li>
        /// </ul>
        /// </summary>
        public string Code { get; set; }

        /// <summary>
        /// 错误信息
        /// </summary>
        public string Msg { get; set; }

        /// <summary>
        /// 返回数据
        /// </summary>
        public T Data { get; set; }

        public ApiOutput(T loginData, string code = "A00000", string msg = null)
        {
            this.Data = loginData;
            this.Code = code;
            this.Msg = msg;
        }
    }
}
