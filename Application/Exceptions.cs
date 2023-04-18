using System;
using System.Collections.Generic;
using System.Text;

namespace Application
{
    public sealed class NotFoundException : ApplicationException
    {
        public string LocalizableObjectName { get; set; }

        public NotFoundException(string name, object locatableObjectId, string localizableObjectName, Dictionary<string, object> parameters)
            : base($"{name} for LocalizableObjectId - ({locatableObjectId}) is not found with given parameters.")
        {
            if (localizableObjectName != null) LocalizableObjectName = localizableObjectName;
            foreach (var parameter in parameters)
            {
                Data.Add(parameter.Key, parameter.Value);
            }
        }

        public NotFoundException(string name, object locatableObjectId, Dictionary<string, object> parameters)
            : base($"{name} for LocalizableObjectId - ({locatableObjectId}) is not found with given parameters.")
        {
            foreach (var parameter in parameters)
            {
                Data.Add(parameter.Key, parameter.Value);
            }
        }

        public NotFoundException(string name, object locatableObjectId)
            : base($"{name} for LocalizableObjectId - ({locatableObjectId}) is not found with given parameters.")
        {
        }
    }

    public sealed class InvalidSearchParameterException : ApplicationException
    {
        public string Parameter { get; set; }
        public InvalidSearchParameterException(string message)
            : base(message)
        {
        }
    }
}
