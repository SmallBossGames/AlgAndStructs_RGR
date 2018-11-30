using System;
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
                    //var tail = _tempAddEdge.Subdivide(_tempAddIndex + 1);

                    if(_tempAddDeep == 1)
                    {
                        SplitTreeRoot(_tempAddEdge, _tempAddIndex, _string, charIndex);
                    }

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

        private void SplitTreeRoot(SuffixTreeEdge edge, int position, IList<char> charset, int currentCharIndex)
        {
            SuffixTreeEdge tail = null;
            SuffixTreeEdge tempEdge = null;

            foreach (var item in _root.Children)
            {
                var newPos = position - (item.Span.From - edge.Span.From); 
                if(newPos > 0)
                {
                    var newEdge = new SuffixTreeEdge()
                    {
                        Span = new SuffixTreeSpan(charset, currentCharIndex, null),
                    };

                    var newTail = item.Subdivide(newPos);
                    item.Children.Add(newEdge);

                    if (tail!= null)
                    {
                        tail.SuffixLink = newTail;
                        tempEdge.SuffixLink = newEdge;
                    }

                    tempEdge = newEdge;
                    tail = newTail;
                }
            }
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

    class SuffixTreeEdge
    {
        public SuffixTreeEdge()
        {
            Children = new List<SuffixTreeEdge>();
        }

        internal SuffixTreeSpan Span { get; set; }
        internal List<SuffixTreeEdge> Children { get; set; }
        internal SuffixTreeEdge SuffixLink { get; set; }

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
