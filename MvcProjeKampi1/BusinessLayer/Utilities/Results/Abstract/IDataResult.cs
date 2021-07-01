using DataAccessLayer.Utilities.Result.Abstract;

namespace DataAccessLayer.Utilities.Result.Concrete
{
    public interface IDataResult<T>:IResult
    {
        T Data { get; }
    }
}
