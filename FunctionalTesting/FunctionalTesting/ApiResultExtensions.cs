using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalTesting
{
    public static class ApiResultExtensions
    {
        public static ApiResult OnSuccessAndWhen<T>(this ApiResult<T> result, Func<T, bool> predicate,
            Func<ApiResult> trueResult, Func<ApiResult> falseResult)
        {
            if (result.IsFailure) return ApiResult.Fail(result.ApiCall, result.ErrorCode);

            return predicate(result.Value) ? trueResult() : falseResult();
        }

        public static ApiResult<T> OnSuccessAndWhen<T>(this ApiResult<T> result, Func<T, bool> predicate,
            Func<ApiResult<T>> trueResult, Func<ApiResult<T>> falseResult)
        {
            if (result.IsFailure) return ApiResult.Fail<T>(result.ApiCall, result.ErrorCode);

            return predicate(result.Value) ? trueResult() : falseResult();
        }

        public static ApiResult OnSuccess<T>(this ApiResult<T> result, Func<T, ApiResult> func)
        {
            if (result.IsFailure) return ApiResult.Fail(result.ApiCall, result.ErrorCode);

            return func(result.Value);
        }

        //public static ApiResult<T> OnSuccess<T>(this ApiResult<T> result, Func<T, ApiResult<T>> func)
        //{
        //    if (result.IsFailure) return ApiResult.Fail<T>(result.ApiCall, result.ErrorCode);

        //    return func(result.Value);
        //}

        public static ApiResult OnSuccess(this ApiResult result, Func<ApiResult> func)
        {
            if (result.IsFailure) return ApiResult.Fail(result.ApiCall, result.ErrorCode);

            return func();
        }

        //public static ApiResult<T> OnSuccess<T>(this ApiResult<T> result, Func<T, bool> predicate, Func<ApiResult<T>> func)
        //{
        //    if (result.IsFailure) return ApiResult.Fail<T>(result.ApiCall, result.ErrorCode);

        //    return predicate(result.Value) ? func() : result;
        //}

        public static void When(this ApiResult result, Func<ApiResult, bool> predicate, Action<ApiResult> action)
        {
            if (predicate(result)) action(result);
        }

        public static ApiResult Cast<T>(this ApiResult<T> result)
        {
            return result.IsSuccessful ? ApiResult.Ok(result.ApiCall) : ApiResult.Fail(result.ApiCall, result.ErrorCode);
        }
    }
}