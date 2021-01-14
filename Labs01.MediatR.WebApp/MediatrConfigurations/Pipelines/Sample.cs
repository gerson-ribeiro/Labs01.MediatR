using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Labs01.MediatR.WebApp.MediatrConfigurations.Pipelines
{
    public class Sample<T>
    {
        public T Results { get; set; }
        public object Errors { get; set; }
    }
}
