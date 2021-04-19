using System;

namespace Hydra.NET
{
    /// <summary>
    /// Designates an operation supported by instances of a class.
    /// </summary>
    public class OperationAttribute : Attribute
    {
        /// <summary>
        /// Creates a new <see cref="OperationAttribute"/>.
        /// </summary>
        /// <param name="supportedClassType">
        /// The type of class whose instances support the operation.
        /// </param>
        public OperationAttribute(Type supportedClassType) => 
            SupportedClassType = supportedClassType;

        /// <summary>
        /// The HTTP method used for the operation. See <see cref="Method"/>.
        /// </summary>
        public string? Method { get; set; }

        /// <summary>
        /// The type of the class whose instances support the operation.
        /// </summary>
        public Type SupportedClassType { get; }

        /// <summary>
        /// The title of the operation.
        /// </summary>
        public string? Title { get; set; }
    }
}
