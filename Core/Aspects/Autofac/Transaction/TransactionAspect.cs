using System;
using Castle.DynamicProxy;
using Core.Utilities.Interceptors.Autofac;
using System.Transactions;

namespace Core.Aspects.Autofac.Transaction
{
    public class TransactionAspect : MethodInterception
    {
        public override void Intercept(IInvocation invocation)
        {
            using (var transaction = new TransactionScope())
            {
                try
                {
                    invocation.Proceed();
                    transaction.Complete();
                }
                catch (Exception exception)
                {
                    transaction.Dispose();
                    throw;
                }
            }
        }
    }
}