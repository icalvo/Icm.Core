using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using CS = Microsoft.CodeAnalysis.CSharp;
using VB = Microsoft.CodeAnalysis.VisualBasic;

namespace Gekka.Roslyn.TranslateViewer
{
    enum TargetLanguage
    {
        CS, VB,
    }
    enum ItemType
    {
        Token, Node, Trivia
    }

    class Worker : System.ComponentModel.INotifyPropertyChanged, IDisposable
    {
        public Worker()
        {
            doConvert = new System.Threading.ManualResetEvent(true);
            background = new System.ComponentModel.BackgroundWorker();
            background.WorkerReportsProgress = true;
            background.WorkerSupportsCancellation = true;
            background.DoWork += (s, e) =>
            {
                Work();
            };
            background.RunWorkerAsync();
            Languages = (TargetLanguage[])Enum.GetValues(typeof(TargetLanguage)).Cast<TargetLanguage>().ToArray();

            dispatcher = System.Windows.Threading.Dispatcher.CurrentDispatcher;
        }
        System.Windows.Threading.Dispatcher dispatcher;

        private bool changed;
        private System.Threading.ManualResetEvent doConvert;
        private System.ComponentModel.BackgroundWorker background;

        public TargetLanguage[] Languages { get; private set; }

        public TargetLanguage InputLanguage
        {
            get { return _InputLanguage; }
            set
            {
                _InputLanguage = value;
                OnPropertyChanged("InputLanguage");

                changed = true;
                doConvert.Set();
            }
        }
        private TargetLanguage _InputLanguage;

        public string InputText
        {
            get { return _InputText; }
            set
            {
                _InputText = value;
                OnPropertyChanged("InputText");
                changed = true;
                doConvert.Set();
            }
        }
        private string _InputText;

        public string OutputText
        {
            get { return _OutputText; }
            private set
            {
                _OutputText = value;
                OnPropertyChanged("OutputText");
            }
        }
        private string _OutputText;

        public Item Result
        {
            get { return _Result; }
            private set
            {
                _Result = value;
                OnPropertyChanged("Result");
            }
        }
        private Item _Result;

        public int ConvertStartDelay
        {
            get
            {
                return _ConvertStartDelay;
            }
            set
            {
                _ConvertStartDelay = value;
                OnPropertyChanged("ConvertDelay");
            }
        }
        private int _ConvertStartDelay = 1000;

        private void Work()
        {
            string text = string.Empty;
            TargetLanguage language = TargetLanguage.VB; ;

            while (!background.CancellationPending)
            {
                doConvert.WaitOne();
                if (background.CancellationPending)
                {
                    break;
                }
                doConvert.Reset();
                if (doConvert.WaitOne(TimeSpan.FromMilliseconds(_ConvertStartDelay)))
                {
                    continue;
                }
                if (background.CancellationPending)
                {
                    break;
                }
                text = InputText;
                OutputText = "";
                language = InputLanguage;
                changed = false;
                doConvert.Reset();
                if (string.IsNullOrWhiteSpace(text))
                {
                    Result = null;
                    continue;
                }
                
                try
                {
                    Item item;
                    if (InputLanguage == TargetLanguage.VB)
                    {
                        var vbTree = VB.VisualBasicSyntaxTree.ParseText(text);
                        var vbRoot = (VB.VisualBasicSyntaxNode)vbTree.GetRoot();

                        var vbRoot2 = vbRoot.NormalizeWhitespace();
                        OutputText = vbRoot2.ToFullString();

                        item = Item.Create(vbRoot);
                    }
                    else
                    {
                        var csTree = CS.CSharpSyntaxTree.ParseText(InputText);
                        var csRoot = (CS.CSharpSyntaxNode)csTree.GetRoot();

                        Translator.CS2VB.ResetCounter();
                        var vbRoot = Translator.CS2VB.Translate(text).NormalizeWhitespace();
                        OutputText = vbRoot.ToFullString();
                        item = Item.Create(csRoot);
                    }

                    Result = item;

                    GenerateItems(item);
                }
                catch (Exception ex)
                {
                    OutputText = ex.Message;
                }
            }
        }

        private void GenerateItems(Item item)
        {
            Stack<Pack> stack = new Stack<Pack>();
            Pack pack;
            pack = new Pack(item);
            stack.Push(pack);
            while (stack.Count > 0 && !changed)
            {
                pack = stack.Peek();
                if (pack.ie == null || !pack.ie.MoveNext())
                {
                    stack.Pop();
                }
                else
                {
                    object o = pack.ie.Current;
                    item = Item.Create(o);
                    if (item != null)
                    {
                        dispatcher.Invoke(() =>
                        {
                            pack.item.Items.Add(item);
                        }, System.Windows.Threading.DispatcherPriority.Background);

                        pack = new Pack(item);
                        stack.Push(pack);
                    }
                }
            }
        }

        struct Pack
        {
            public Item item;
            public IEnumerator<object> ie;

            public Pack(Item item)
            {
                this.item = item;
                ie = null;
                var x = item.GetItemObjects();
                if (x != null)
                {
                    ie = x.GetEnumerator();
                }
            }
        }

        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            var pc = PropertyChanged;
            if (pc != null)
            {
                pc(this, new System.ComponentModel.PropertyChangedEventArgs(name));
            }
        }

        public void Dispose()
        {
            background.CancelAsync();
            doConvert.Set();
        }
    }

    class Item
    {
        #region Constructor

        public static Item Create(object o)
        {
            if (o is SyntaxNode)
            {
                return new Item((SyntaxNode)o);
            }
            else if (o is SyntaxNodeOrToken)
            {
                return new Item((SyntaxNodeOrToken)o);
            }
            if (o is SyntaxTrivia)
            {
                return new Item((SyntaxTrivia)o);
            }
            return null;
        }

        //public Item(Diagnostic d)
        //{
        //    this.ItemType = WpfApplication2.ItemType.Diagnostic;
        //    this.Value = d;
        //    this.Text = d.ToString();
        //}

        private Item(SyntaxTrivia t)
        {
            ItemType = ItemType.Trivia;
            Value = t;
            Text = t.ToFullString();
            bool isVB = IsVB(t.Language);
            Language = isVB ? TargetLanguage.VB : TargetLanguage.CS;
            if (Language == TargetLanguage.VB)
            {
                foreach (var k in kindsVB)
                {
                    if (t.IsKind(k))
                    {
                        Text = k.ToString();
                        break;
                    }
                }
            }
            else
            {
                foreach (var k in kindsCS)
                {
                    if (t.IsKind(k))
                    {
                        Text = k.ToString();
                        break;
                    }
                }
            }
        }
        private Item(SyntaxNodeOrToken x)
        {
            SyntaxNodeOrToken = x;
            ChildList = x.ChildNodesAndTokens();
            Language = IsVB(x.Language) ? TargetLanguage.VB : TargetLanguage.CS;
            if (SyntaxNodeOrToken.IsToken)
            {
                Value = Token = x.AsToken();
                Text = Token.Text;

                if (Language == TargetLanguage.VB)
                {
                    foreach (var k in kindsVB)
                    {
                        if (Token.IsKind(k))
                        {
                            Kind = k;
                            break;
                        }
                    }
                }
                else
                {
                    foreach (var k in kindsCS)
                    {
                        if (Token.IsKind(k))
                        {
                            Kind = k;
                            break;
                        }
                    }
                }
                ItemType = ItemType.Token;
            }
            else
            {
                Value = Node = x.AsNode();

                if (Node is VB.VisualBasicSyntaxNode)
                {
                    var node = (VB.VisualBasicSyntaxNode)Node;
                    Kind = node.Kind();
                    Text = Kind.ToString();
                }
                else
                {
                    Text = ((CS.CSharpSyntaxNode)Node).Kind().ToString();
                    //Text = this.Node.ToString();
                }
                ItemType = ItemType.Node;
            }

        }

        #endregion

        #region

        public ItemType ItemType { get; set; }

        public object Kind { get; set; }

        public SyntaxNodeOrToken SyntaxNodeOrToken { get; set; }

        [System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
        public object Value { get; set; }

        [System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
        public SyntaxToken Token { get; private set; }

        public SyntaxNode Node { get; private set; }

        [System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
        public IEnumerable<SyntaxTrivia> LeadingTrivia
        {
            get
            {
                if (ItemType == ItemType.Token)
                {

                    return ConvertTrivia(Token.LeadingTrivia);

                }
                else if (ItemType == ItemType.Node)
                {

                    return ConvertTrivia(Node.GetLeadingTrivia());
                }
                return new SyntaxTrivia[0];
            }
        }

        [System.ComponentModel.TypeConverter(typeof(System.ComponentModel.ExpandableObjectConverter))]
        public IEnumerable<SyntaxTrivia> TrailingTrivia
        {
            get
            {
                if (ItemType == ItemType.Token)
                {
                    return ConvertTrivia(Token.TrailingTrivia);
                }
                else if (ItemType == ItemType.Node)
                {
                    return ConvertTrivia(Node.GetTrailingTrivia());
                }
                return new SyntaxTrivia[0];
            }
        }

        internal ChildSyntaxList ChildList { get; set; }

        internal IEnumerable<object> GetItemObjects()
        {
            if (ItemType == ItemType.Trivia)
            {
                SyntaxTrivia t = (SyntaxTrivia)Value;
                foreach (Diagnostic d in t.GetDiagnostics())
                {
                    yield return d;
                }
            }
            else
            {
                if (ChildList != null)
                {
                    foreach (SyntaxNodeOrToken c in ChildList)
                    {
                        if (c.IsKind(CS.SyntaxKind.EmptyStatement) || c.IsKind(VB.SyntaxKind.EmptyStatement))
                        {
                            continue;
                        }
                        yield return c;
                    }
                }
            }
        }

        public System.Collections.ObjectModel.ObservableCollection<Item> Items
        {
            get
            {
                return _Items;
            }
        }
        private System.Collections.ObjectModel.ObservableCollection<Item> _Items = new System.Collections.ObjectModel.ObservableCollection<Item>();

        public TargetLanguage Language { get; set; }
        private static bool IsVB(string s)
        {
            return s == "Visual Basic";
        }

        static VB.SyntaxKind[] kindsVB = Enum.GetValues(typeof(VB.SyntaxKind)).Cast<VB.SyntaxKind>().ToArray();
        static CS.SyntaxKind[] kindsCS = Enum.GetValues(typeof(CS.SyntaxKind)).Cast<CS.SyntaxKind>().ToArray();

        #endregion

        public string Text { get; set; }

        public override string ToString()
        {
            return Text;
        }

        private static IEnumerable<SyntaxTrivia> ConvertTrivia(SyntaxTriviaList list)
        {
            foreach (SyntaxTrivia t in list)
            {
                if (!t.IsKind(CS.SyntaxKind.None))
                {
                    yield return t;
                }
            }
        }
    }
}
