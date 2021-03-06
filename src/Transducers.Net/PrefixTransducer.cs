﻿using System;

namespace Transducers.Net
{
    internal class PrefixTransducer<TSource> : ITransducer<TSource, TSource>
    {
        private readonly Func<Func<TSource, bool>> _prefixCompleteFactory;

        public PrefixTransducer(Func<Func<TSource, bool>> prefixCompleteFactory)
        {
            _prefixCompleteFactory = prefixCompleteFactory;
        }

        public Func<TAcc, TSource, TAcc> Transform<TAcc>(ReductionStatus status, Func<TAcc, TSource, TAcc> reducer)
        {
            var prefixComplete = _prefixCompleteFactory();
            return (acc, source) => {
                if (prefixComplete(source)) {
                    status.Complete = true;
                    return acc;
                }
                return reducer(acc, source);
            };
        }
    }
}
