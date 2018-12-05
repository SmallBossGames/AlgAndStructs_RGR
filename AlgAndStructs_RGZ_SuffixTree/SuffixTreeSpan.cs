using System;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Синтаксическое дерево предначначено для обработки строк, состоящих из букв латинского алфавита
/// </summary>

namespace AlgAndStructs_RGZ_SuffixTree
{
    struct SuffixTreeSpan :IEnumerable<char>
    {
        private readonly IList<char> _charset;
        public readonly int? _to;

        public int Length => To - From;

        public int From { get; }

        public int To => _to == null ? _charset.Count : _to.Value;

        public static SuffixTreeSpan Zero => new SuffixTreeSpan(null, 0, 0);

        public SuffixTreeSpan(IList<char> charset, int fromIndex, int? toIndex)
        {
            _charset = charset;
            From = fromIndex;
            _to = toIndex;
        }

        public char this[int index]
        {
            get
            {
                if(index < 0 || index >= Length)
                {
                    throw new IndexOutOfRangeException();
                }
                return _charset[From + index];
            }
        }

        public (SuffixTreeSpan head, SuffixTreeSpan tail) Split(int position)
        {
            if (position < 1 || position >= Length)
            {
                throw new IndexOutOfRangeException();
            }

            var head = new SuffixTreeSpan(_charset, From, From + position);
            var tail = new SuffixTreeSpan(_charset, From + position, _to);

            return (head, tail);
        }

        public IEnumerator<char> GetEnumerator()
        {
            for (int i = From; i < To; i++)
            {
                yield return _charset[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
