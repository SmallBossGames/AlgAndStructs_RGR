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

        private (SuffixTreeEdge edge, char startChar, int length) activePoint;
        private int remainder;

        ///Add opertion tempData
        ///

        /* private int _tempAddIndex;
         private int _tempAddDeep;
         private SuffixTreeEdge _tempAddEdge;*/
        /// Edd temp data

        public SuffixTree()
        {
            _root = new SuffixTreeEdge()
            {
                Span = SuffixTreeSpan.Zero
            };
            _string = new List<char>(16);

            remainder = 0;

            //ResetAddTemp();
        }

        public void Add(char character)
        {
            var currentCharIndex = _string.Count;
            SuffixTreeEdge tempEdge = null;

            if (remainder == 0)
            {
                remainder = 1;
                activePoint = (_root, character, 0);
            }

            _string.Add(character);

            while (remainder > 0)
            {
                SuffixTreeEdge currentEdge = null;
                foreach (var item in activePoint.edge.Children)
                {
                    if (item.Span[0] == activePoint.startChar)
                    {
                        currentEdge = item;
                    }
                }

                if(currentEdge == null)
                {
                    activePoint.edge.Children.Add(new SuffixTreeEdge()
                    {
                        Span = new SuffixTreeSpan(_string, currentCharIndex, null)
                    });

                    activePoint.startChar = character;
                    activePoint.length--;

                    remainder--;

                    continue;
                }

                if (activePoint.length == currentEdge.Span.Length)
                {
                    activePoint = (currentEdge, character, 0);
                }

                if (currentEdge.Span[activePoint.length] == character)
                {
                    activePoint.length++;
                    remainder++;
                    return;
                }

                var tail = currentEdge.Subdivide(activePoint.length);

                var newEdge = new SuffixTreeEdge()
                {
                    Span = new SuffixTreeSpan(_string, currentCharIndex, null),
                };

                currentEdge.Children.Add(newEdge);

                if(tempEdge != null)
                {
                    tempEdge.SuffixLink = activePoint.edge;
                    tempEdge = activePoint.edge;
                }

                activePoint.edge = activePoint.edge.SuffixLink ?? _root;
                //activePoint.startChar = 
                remainder--;
            }
        }

        public void AddRange(IEnumerable<char> characters)
        {
            foreach (var item in characters)
            {
                Add(item);
            }
        }

        public bool IsSuffixExist(string str) => SlowScan(_root, str);

        /*private void ResetAddTemp()
        {
            _tempAddIndex = 0;
            _tempAddDeep = 0;
            _tempAddEdge = _root;
        }*/

        /*private void SplitTreeRoot(SuffixTreeEdge edge, int position, IList<char> charset, int currentCharIndex)
        {
            SuffixTreeEdge tempcontinueEdge = null;
            SuffixTreeEdge tempNewEdge = null;

            for (int i = 0; i < _root.Children.Count; i++)
            {
                var item = _root.Children[i];
                var absPos = position + edge.Span.From;

                if (absPos > 0)
                {
                    var newEdge = new SuffixTreeEdge()
                    {
                        Span = new SuffixTreeSpan(charset, currentCharIndex, null),
                    };

                    (item, absPos) = FastScan(item, edge.Span, absPos);

                    if(absPos+1 != item.Span.Length)
                    {
                        var continueEdge = item.Subdivide(absPos + 1);

                        if (tempcontinueEdge != null)
                        {
                            tempcontinueEdge.SuffixLink = continueEdge;
                        }

                        tempcontinueEdge = continueEdge;
                    }

                    item.Children.Add(newEdge);

                    if (tempNewEdge != null)
                    {
                        tempNewEdge.SuffixLink = newEdge;
                    }

                    tempNewEdge = newEdge;
                }
            }
        }*/

        /*private void SplitTreeSuffixLink(SuffixTreeEdge edge, int position, IList<char> charset, int currentCharIndex)
        {
            SuffixTreeEdge tempcontinueEdge = null;
            SuffixTreeEdge tempNewEdge = null;

            while (edge!=null)
            {
                var newEdge = new SuffixTreeEdge()
                {
                    Span = new SuffixTreeSpan(charset, currentCharIndex, null),
                };

                var (item, newPos) = FastScan(edge, edge.Span, position);

                if (newPos + 1 != item.Span.Length)
                {
                    var continueEdge = item.Subdivide(newPos + 1);

                    if (tempcontinueEdge != null)
                    {
                        tempcontinueEdge.SuffixLink = continueEdge;
                    }

                    tempcontinueEdge = continueEdge;
                }

                item.Children.Add(newEdge);

                if (tempNewEdge != null)
                {
                    tempNewEdge.SuffixLink = newEdge;
                }

                tempNewEdge = newEdge;


                edge = edge.SuffixLink;
            }
        }*/

        /*private (SuffixTreeEdge edge, int position) FastScan(SuffixTreeEdge startEdge, SuffixTreeSpan span, int absPos)
        {
            while (absPos >= startEdge.Span.Length)
            {
                foreach (var item in startEdge.Children)
                {
                    if(item.Span[0] == span[startEdge.Span.Length])
                    {
                        absPos -= startEdge.Span.To;
                        startEdge = item;
                        break;
                    }
                }
               // throw new ArgumentException("Символ не найден");
            }
            return (startEdge, absPos);
        }*/

        private bool SlowScan(SuffixTreeEdge startEdge, string str)
        {
            var spanIndex = 0;

            for (int i = 0; i < str.Length; i++)
            {
                if (spanIndex + 1 >= startEdge.Span.Length)
                {
                    foreach (var item in startEdge.Children)
                    {
                        if (item.Span[0] == str[i])
                        {
                            startEdge = item;
                            spanIndex = 0;
                            break;
                        }
                    }
                }

                if (startEdge.Span[spanIndex] != str[i])
                {
                    return false;
                }

                spanIndex++;
            }

            return true;
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
