using System;
using System.Collections.Generic;
using System.Linq;

namespace FunctionalTesting
{
    public class Evaluate<T>
    {
        private readonly List<T> _values;

        public Evaluate(params T[] values)
        {
            _values = new List<T>(values);
        }

        public WhenEvaluate<T> When(params Func<T, bool>[] predicates)
        {
            return _values.Where((t, i) => predicates.ElementAtOrDefault(i) != null && !predicates[i](t)).Any()
                ? new WhenEvaluate<T>(this, false)
                : new WhenEvaluate<T>(this, true);
        }
    }

    public class WhenEvaluate<T>
    {
        private readonly Evaluate<T> _evaluate;
        private readonly bool _conditionsMet;

        public WhenEvaluate(Evaluate<T> evaluate, bool conditionsMet)
        {
            _evaluate = evaluate;
            _conditionsMet = conditionsMet;
        } 

        public ThenEvaluate<T> Then(Action action)
        {
            if (_conditionsMet) action();

            return new ThenEvaluate<T>(_evaluate, _conditionsMet);
        }
    }

    public class ThenEvaluate<T>
    {
        private readonly Evaluate<T> _evaluate;
        private readonly bool _conditionsMet;

        public ThenEvaluate(Evaluate<T> evaluate, bool conditionsMet)
        {
            _evaluate = evaluate;
            _conditionsMet = conditionsMet;
        }

        public Evaluate<T> Else(Action action)
        {
            if (!_conditionsMet) action();

            return _evaluate;
        }

        public WhenEvaluate<T> When(params Func<T, bool>[] predicates)
        {
            return _evaluate.When(predicates);
        }
    }

    public static class EvaluateMethods
    {
        public static Evaluate<T> Evaluate<T>(params T[] values)
        {
            return new Evaluate<T>(values);
        }
    }
}