namespace DataAccessLayer.Utilities.Result.Concrete
{
    class SuccessResult : Result
    {
        public SuccessResult(string message) : base(true, message)
        {

        }
        public SuccessResult() : base(true)
        {

        }

    }
}
