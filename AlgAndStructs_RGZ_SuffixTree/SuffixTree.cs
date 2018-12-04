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

        private (SuffixTreeEdge edge, char startChar, int length) startPoint;
        private int remainder;

        private int checkSum = 0;

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

            if(_string.Count == 62)
            {
                var a = 0;
            }
            var currentCharIndex = _string.Count;
            SuffixTreeEdge tempEdge = null;

            if (remainder == 0)
            {
                startPoint = (_root, character, 0);
            }

            checkSum++;
            remainder++;


            _string.Add(character);

            while (remainder > 0)
            {
                if(currentCharIndex == 65)
                {
                    var a = 0;
                }
                SuffixTreeEdge currentEdge = null;
                foreach (var item in startPoint.edge.Children)
                {
                    if (item.Span[0] == startPoint.startChar)
                    {
                        currentEdge = item;
                        break;
                    }
                }


                if (currentEdge == null)
                {

                    currentEdge = new SuffixTreeEdge()
                    {
                        Span = new SuffixTreeSpan(_string, currentCharIndex, null)
                    };


                    startPoint.edge.Children.Add(currentEdge);

                    if(startPoint.edge != _root)
                    {
                        if (tempEdge != null)
                        {
                            //Правило 2
                            tempEdge.SuffixLink = startPoint.edge;
                        }

                        tempEdge = startPoint.edge;
                    }


                    startPoint.edge = startPoint.edge.SuffixLink ?? _root;

                    remainder--;

                    continue;
                }

                if (startPoint.length >= currentEdge.Span.Length)
                {
                    var deltaLength = startPoint.length - currentEdge.Span.Length;
                    var newCharIndex = currentCharIndex - deltaLength;
                    startPoint = (currentEdge, _string[newCharIndex], deltaLength);
                    continue;
                }

                if (currentEdge.Span[startPoint.length] == character)
                {
                    startPoint.length++;
                    return;
                }



                var tail = currentEdge.Subdivide(startPoint.length);

                var newEdge = new SuffixTreeEdge()
                {
                    Span = new SuffixTreeSpan(_string, currentCharIndex, null),
                };

                currentEdge.Children.Add(newEdge);

                if(tempEdge != null)
                {
                    //Правило 2
                    tempEdge.SuffixLink = currentEdge;
                }

                tempEdge = currentEdge;

                
                if (startPoint.edge == _root)
                {
                    //Правило 1
                    startPoint.length--;
                    startPoint.startChar = _string[currentCharIndex - startPoint.length];
                }
                else
                {

                    if(startPoint.edge.SuffixLink != null)
                    {
                        var a = 0;
                    }
                    //Правило 3
                    startPoint.edge = startPoint.edge.SuffixLink ?? _root;
                }

                remainder--;
            }
        }

        public void NewAdd(char character)
        {
            if(remainder == 0)
            {
                startPoint = (_root, character, 0);
            }

            var currentCharIndex = _string.Count;
            _string.Add(character);
            SuffixTreeEdge tempSuffixLink = null;
            remainder++;

            while (remainder>0)
            {
                SuffixTreeEdge currentEdge = null;
                var currentEdgeIndex = 0;

                for (int i = 0; i < startPoint.edge.Children.Count; i++)
                {
                    SuffixTreeEdge item = startPoint.edge.Children[i];
                    if (item.Span[0] == startPoint.startChar)
                    {
                        currentEdge = item;
                        currentEdgeIndex = i;
                        break;
                    }
                }

                if(currentEdge == null)
                {
                    var addedEdge = new SuffixTreeEdge()
                    {
                        Span = new SuffixTreeSpan(_string, currentCharIndex, null),
                    };

                    startPoint.edge.Children.Add(addedEdge);
                    
                    if (startPoint.edge != _root)
                    {
                        startPoint.edge = startPoint.edge.SuffixLink ?? _root;
                    }

                    remainder--;

                    continue;
                }

                if(startPoint.length >= currentEdge.Span.Length)
                {
                    startPoint.length -= currentEdge.Span.Length;
                    startPoint.startChar = _string[currentCharIndex - startPoint.length];
                    startPoint.edge = currentEdge;

                    continue;
                }

                if(currentEdge.Span[startPoint.length] == character)
                {
                    startPoint.length++;
                    return;
                }

                //Если ничего из предыдущего не вышло - делаем подразделение
                var tail = new SuffixTreeSpan(_string, startPoint.length + currentEdge.Span.From, null);
                var head = new SuffixTreeSpan(_string, currentEdge.Span.From, startPoint.length + currentEdge.Span.From);
                var newSpan = new SuffixTreeSpan(_string, currentCharIndex, null);

                var tailEdge = new SuffixTreeEdge()
                {
                    Children = currentEdge.Children,
                    Span = tail,
                };

                var newEdge = new SuffixTreeEdge()
                {
                    Span = newSpan,
                };

                currentEdge.Span = head;
                currentEdge.Children = new List<SuffixTreeEdge>(2)
                {
                    tailEdge,
                    newEdge
                };

                if (tempSuffixLink!=null)
                {
                    tempSuffixLink.SuffixLink = currentEdge;
                }
                tempSuffixLink = currentEdge;

                if(startPoint.edge == _root)
                {
                    startPoint.length--;
                    startPoint.startChar = _string[currentCharIndex - startPoint.length];
                }
                else
                {
                    startPoint.edge = startPoint.edge.SuffixLink ?? _root;
                }

                remainder--;
            }
        }

        public void AddRange(IEnumerable<char> characters)
        {
            foreach (var item in characters)
            {
                NewAdd(item);
            }
        }

        public bool IsSuffixExist(string str)
        {
            var startPoint = _root;
            var localPosition = 0;

            foreach (var item in startPoint.Children)
            {
                if (item.Span[0] == 'е')
                {
                    var a = 0;
                }
            }

            for (int i = 0; i < str.Length; i++)
            {
                if(startPoint.Span.Length == localPosition)
                {
                    foreach (var item in startPoint.Children)
                    {
                        if(item.Span[0] == str[i])
                        {
                            startPoint = item;
                            localPosition = 0;
                            break;
                        }
                    }
                }
                if (startPoint.Span[localPosition] != str[i])
                {
                    return false;
                }
                localPosition++;
            }
            return true;
        }

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
}
