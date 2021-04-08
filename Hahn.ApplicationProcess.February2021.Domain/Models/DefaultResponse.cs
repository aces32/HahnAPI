using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hahn.ApplicationProcess.February2021.Domain.Models
{
    public class DefaultResponse<T>
    {
        /// <summary>
        /// Api response description
        /// </summary>
        public string Message { get; set; }
        /// <summary>
        /// Api status code
        /// </summary>
        public T Data { get; set; }
    }
}
