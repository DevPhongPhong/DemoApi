using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Exceptions
{
    // T: int, long, short,...
    public class IdNotFoundException<TypeOfId> : Exception
    {
        public TypeOfId _id;
        public Type _typeOfEntity;
        public IdNotFoundException(TypeOfId id, Type typeOfEntity)
        {
            _id = id;
            _typeOfEntity = typeOfEntity;
        }
        public override string Message => "Không tìm thấy " + _typeOfEntity.FullName + " có id: " + _id;
    }
}
