using CSharpFunctionalExtensions;
using System;

namespace dataneo.SharedKernel
{
    public abstract class Handler<T>
    {
        private Handler<T> Next;

        public Result Handle(T request)
        {
            var verifyResult = HandleRequest(request);
            if (verifyResult.IsFailure)
                return verifyResult;

            if (this.Next != null)
                return this.Next.HandleRequest(request);

            return verifyResult;
        }

        protected abstract Result HandleRequest(T request);

        public Handler<T> SetNext(Handler<T> nextVerificationHandler)
        {
            if (nextVerificationHandler == null)
                throw new ArgumentNullException(nameof(nextVerificationHandler));
            this.Next = nextVerificationHandler;
            return nextVerificationHandler;
        }
    }
}
