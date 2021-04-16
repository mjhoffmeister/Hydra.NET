using System;
using System.Collections.Generic;
using System.Text;

namespace Hydra.NET
{
    public class OperationAttribute : Attribute
    {
        public OperationAttribute(Type supportedClassType) => 
            SupportedClassType = supportedClassType;

        public string? Method { get; set; }

        public Type SupportedClassType { get; }

        public string? Title { get; set; }
    }
}
