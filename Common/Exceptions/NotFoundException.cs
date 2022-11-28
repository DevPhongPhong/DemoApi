using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    public class NotFoundException<TypeOfInput> : Exception
    {
        public TypeOfInput _input;
        public Type _typeOfEntity;
        public NotFoundException(TypeOfInput input, Type typeOfEntity)
        {
            _input = input;
            _typeOfEntity = typeOfEntity;
        }
        public override string Message => "Không tìm thấy " + _typeOfEntity.FullName + " có input: " + _input;
    }
}
