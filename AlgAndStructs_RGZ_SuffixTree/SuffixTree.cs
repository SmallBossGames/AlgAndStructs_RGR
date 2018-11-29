using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// Синтаксическое дерево предначначено для обработки строк, состоящих из букв латинского алфавита
/// </summary>

namespace AlgAndStructs_RGZ_SuffixTree
{
    class SuffixTree
    {
        private readonly SuffixTreeEdge _root;
        private readonly List<char> _string;

        ///Add opertion tempData
        private int _tempAddIndex;
        private int _tempAddDeep;
        private SuffixTreeEdge _tempAddEdge;
        /// Edd temp data

        public SuffixTree()
        {
            _root = new SuffixTreeEdge()
            {
                Span = new SuffixTreeSpan(null, 0, 0)
            };
            _string = new List<char>(16);

            ResetAddTemp();
        }

        public void Add(char character)
        {
            var charIndex = _string.Count;
            _string.Add(character);

            if(_tempAddIndex < _tempAddEdge.Span.Length)
            {
                _tempAddIndex++;
                if (_tempAddEdge.Span[_tempAddIndex] == character)
                {
                    return;
                }
                else
                {
                    var tail = _tempAddEdge.Subdivide(_tempAddIndex + 1);

                    ResetAddTemp();
                    //TODO: Add split logic
                }
            }
            else
            {
                for (int i = 0; i < _tempAddEdge.Children.Count; i++)
                {
                    var checkedEdge = _tempAddEdge.Children[i];
                    if (checkedEdge.Span[0] == character)
                    {
                        _tempAddIndex = 0;
                        _tempAddEdge = checkedEdge;
                        _tempAddDeep++;
                        return;
                    }
                }

                _tempAddEdge.Children.Add(new SuffixTreeEdge()
                {
                    Span = new SuffixTreeSpan(_string, charIndex, null)
                });

                ResetAddTemp();
            }
        }

        public void AddRange(IEnumerable<char> characters)
        {
            foreach (var item in characters)
            {
                Add(item);
            }
        }

        private void ResetAddTemp()
        {
            _tempAddIndex = 0;
            _tempAddDeep = 0;
            _tempAddEdge = _root;
        }

        private void AddEdgeToRootEdges(SuffixTreeEdge edge)
        {
            for (int i = 0; i < _root.Children.Count; i++)
            {
                SuffixTreeEdge child = _root.Children[i];

                if (child.Span.From > edge.Span.From)
                {
                    child.Children.Add(edge);
                }
            }
        }
    }

    struct SuffixTreeSpan:IEnumerable<char>
    {
        private readonly IList<char> _charset;
        private readonly int? _to;

        public int Length => To - From;

        public int From { get; }

        public int To => _to == null ? _charset.Count : _to.Value;

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
            if (position < 0 || position >= Length)
            {
                throw new IndexOutOfRangeException();
            }

            var head = new SuffixTreeSpan(_charset, From, position);
            var tail = new SuffixTreeSpan(_charset, position, _to);

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

    class SuffixTreeEdge
    {
        public SuffixTreeEdge()
        {
            Children = new List<SuffixTreeEdge>();
        }

        internal SuffixTreeSpan Span { get; set; }
        internal List<SuffixTreeEdge> Children { get; set; }

        public SuffixTreeEdge Subdivide(int position)
        {
            var (head, tail) = Span.Split(position);

            var tailEdge = new SuffixTreeEdge()
            {
                Span = tail,
                Children = Children
            };

            Span = head;
            Children = new List<SuffixTreeEdge>() { tailEdge };

            return tailEdge;
        }
    }

    class SuffixTreeNode
    {
        private SuffixTreeNode[] _children;

        public SuffixTreeNode(int index, SuffixTreeNode parent, SuffixTreeNode child = null)
        {
            Index = index;
            Parent = parent;

            if(child!=null)
            {
                _children = new SuffixTreeNode[1] { child };
            }
        }

        SuffixTreeNode[] Children => _children;
        SuffixTreeNode Parent { get; }
        int Index { get; }

        public void AddChild(SuffixTreeNode child)
        {
            if(Children == null)
            {
                throw new ArgumentException("This node is a leaf");
            }

            Array.Resize(ref _children, _children.Length + 1);

            _children[_children.Length - 1] = child;
        }
    }
}
