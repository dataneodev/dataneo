using CSharpFunctionalExtensions;
using System;

namespace dataneo.SharedKernel
{
    public abstract class VerificationHandler<T>
    {
        private VerificationHandler<T> Next;

        public Result Verify(T request)
        {
            var verifyResult = Verification(request);
            if (verifyResult.IsFailure)
                return verifyResult;

            if (this.Next != null)
                return this.Next.Verification(request);

            return verifyResult;
        }

        protected abstract Result Verification(T request);

        public VerificationHandler<T> SetNext(VerificationHandler<T> nextVerificationHandler)
        {
            if (nextVerificationHandler == null)
                throw new ArgumentNullException(nameof(nextVerificationHandler));
            this.Next = nextVerificationHandler;
            return nextVerificationHandler;
        }
    }
}
