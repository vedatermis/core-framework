namespace Core.Utilities.Results
{
    public class SuccessDataResult<T>: DataResult<T>
    {
        public SuccessDataResult(T data, string message) : base(data, true, message) { }

        public SuccessDataResult(T data) : base(data, true) { }

        public SuccessDataResult(string message): base(default, true, message) { }

        public SuccessDataResult(System.Collections.Generic.IList<global::Entities.Concrete.Category> list) : base(default, true) { }
    }
}