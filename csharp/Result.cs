using System;

namespace Tools
{
    public sealed class Result<TValue, TError>
    {
        private readonly TValue? _value;
        private readonly TError? _error;

        public bool IsError { get; }

        public bool IsSuccess => !IsError;

        private Result(TValue value)
        {
            _value = value;
        }

        private Result(TError error)
        {
            IsError = true;
            _error = error;
        }

        public ResultMapper<T> Ok<T>(Func<TValue, T> okMapper)
        {
            ArgumentNullException.ThrowIfNull(okMapper);
            return new ResultMapper<T>(this, okMapper);
        }

        public ResultMapper<TValue> Ok()
        {
            return Ok(value => value);
        }

        public static implicit operator Result<TValue, TError>(TValue value)
        {
            return new(value);
        }

        public static implicit operator Result<TValue, TError>(TError error)
        {
            return new(error);
        }

        public sealed class ResultMapper<T>
        {
            private readonly Result<TValue, TError> _result;
            private readonly Func<TValue, T> _ok;

            public ResultMapper(Result<TValue, TError> result, Func<TValue, T> ok)
            {
                _result = result;
                _ok = ok;
            }

            public T Error(Func<TError, T> errorMapper)
            {
                ArgumentNullException.ThrowIfNull(errorMapper);
                return _result.IsSuccess
                    ? _ok(_result._value!)
                    : errorMapper(_result._error!);
            }

            public T Error(T value)
            {
                return _result.IsSuccess
                    ? _ok(_result._value!)
                    : value;
            }
        }
    }
}
