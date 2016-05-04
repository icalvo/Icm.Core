// Copyright 2015 gekka.
//
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
//    http://www.apache.org/licenses/LICENSE-2.0
//
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.CodeAnalysis;
using VB = Microsoft.CodeAnalysis.VisualBasic;
using VBS = Microsoft.CodeAnalysis.VisualBasic.Syntax;
using CS = Microsoft.CodeAnalysis.CSharp;
using CSS = Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Gekka.Roslyn.Translator.Vb2Cs
{
    public static partial class Translator
    {
        #region

        static SyntaxList<T> ConvertToSyntaxList<T>(this IEnumerable<T> source) where T : SyntaxNode
        {
            SyntaxList<T> list = new SyntaxList<T>();
            return list.AddRange(source);
        }

        static SyntaxList<TV> ConvertSyntaxNodes<TV>(this IEnumerable<VB.VisualBasicSyntaxNode> cs)
            where TV : CS.CSharpSyntaxNode
        {
            return Convert<VB.VisualBasicSyntaxNode, TV>(cs);
        }

        static SyntaxList<TV> Convert<TC, TV>(this IEnumerable<TC> cs)
            where TC : VB.VisualBasicSyntaxNode
            where TV : CS.CSharpSyntaxNode
        {
            SyntaxList<TV> retval = new SyntaxList<TV>();
            retval.AddRange(cs.Select(c => (TV)c.Convert()).Where(b => b != null));
            return retval;
        }

        static SyntaxList<TV> Convert<TC, TV>(this SyntaxList<TC> cs)
            where TC : VB.VisualBasicSyntaxNode
            where TV : CS.CSharpSyntaxNode
        {
            return Convert<TC, TV>(cs.AsEnumerable());
        }

        static SyntaxTokenList ConvertModifiers(SyntaxTokenList csmodifiers)
        {
            SyntaxTokenList retvals = CS.SyntaxFactory.TokenList();

            foreach (SyntaxToken csmodifier in csmodifiers)
            {
                foreach (KeywordPair pair in ModifirePairs)
                {
                    if (csmodifier.IsKind(pair.Cs))
                    {
                        if (pair.Targets != AttributeTargets.All)
                        {
                            SyntaxNode parent = csmodifier.Parent;
                            if (((pair.Targets & AttributeTargets.Class) == AttributeTargets.Class)
                                ^ parent.IsKind(VB.SyntaxKind.ClassBlock))
                            {
                                continue;
                            }
                        }

                        retvals = retvals.Add(CS.SyntaxFactory.Token(pair.Vb));
                        break;
                    }
                }
            }
            return retvals;
        }
        static SyntaxToken ConvertModifier(SyntaxToken csmodifier)
        {
            foreach (KeywordPair pair in ModifirePairs)
            {
                if (csmodifier.IsKind(pair.Cs))
                {
                    if (pair.Targets != AttributeTargets.All)
                    {
                        SyntaxNode parent = csmodifier.Parent;
                        if (((pair.Targets & AttributeTargets.Class) == AttributeTargets.Class)
                            ^ parent.IsKind(VB.SyntaxKind.ClassBlock))
                        {
                            continue;
                        }
                    }

                    return CS.SyntaxFactory.Token(pair.Vb);
                }
            }
            return default(SyntaxToken);
        }

        static SyntaxTokenList ConvertModifiersWithDefault(SyntaxTokenList csmodifiers, SyntaxToken? defaultModifire = null)
        {
            SyntaxTokenList retvals = ConvertModifiers(csmodifiers);
            if (retvals.Count == 0 && defaultModifire != null)
            {
                retvals = retvals.Add(defaultModifire.Value);
            }
            return retvals;

        }

        static SyntaxList<CS.CSharpSyntaxNode> ConvertSyntaxNodes(this IEnumerable<VB.VisualBasicSyntaxNode> cs)
        {
            SyntaxList<CS.CSharpSyntaxNode> retval = new SyntaxList<CS.CSharpSyntaxNode>();
            foreach (var c in cs)
            {
                CS.CSharpSyntaxNode b = (CS.CSharpSyntaxNode)c.Convert();
                if (b != null)
                {
                    retval = retval.Add(b);
                }
            }
            return retval;
        }
        
        static SyntaxList<TV> ConvertStatements<TV>(this SyntaxList<VBS.StatementSyntax> csStatements) where TV : CSS.StatementSyntax
        {
            return Convert<VBS.StatementSyntax, TV>(SplitLabeledStatement(csStatements));
        }
        private static IEnumerable<VBS.StatementSyntax> SplitLabeledStatement(IEnumerable<VBS.StatementSyntax> source)
        {
            if (source != null)
            {
                foreach (VBS.StatementSyntax csStatement in source)
                {
                    if (csStatement.IsKind(VB.SyntaxKind.LabeledStatement))
                    {
                        //Label in CSharp has connected to the next statement.
                        //But, Label in VB is Separated.
                        var csLabel = (VBS.LabeledStatementSyntax)csStatement;
                        yield return csLabel;
                        yield return csLabel.Statement;
                    }
                    else
                    {
                        yield return csStatement;
                    }
                }
            }

        }
        
        static SplitInheritResult SplitInherit(this CSS.InheritsStatementSyntax vbInheritTypes, bool interfaceIsStartWithI)
        {
            SplitInheritResult retval = new SplitInheritResult();
            SyntaxList<CSS.InheritsStatementSyntax> vbInherits = new SyntaxList<CSS.InheritsStatementSyntax>();
            SyntaxList<CSS.ImplementsStatementSyntax> vbImplements = new SyntaxList<CSS.ImplementsStatementSyntax>();

            if (vbInheritTypes != null && vbInheritTypes.Types.Count > 0)
            {
                CSS.TypeSyntax vbType0 = vbInheritTypes.Types[0];
                string name = string.Empty;
                if (vbType0.IsKind(CS.SyntaxKind.QualifiedName))
                {
                    name = ((CSS.QualifiedNameSyntax)vbType0).Right.Identifier.Text;
                }
                else if (vbType0.IsKind(CS.SyntaxKind.PredefinedType))
                {
                    name = ((CSS.PredefinedTypeSyntax)vbType0).ToFullString();
                }
                else if (vbType0.IsKind(CS.SyntaxKind.IdentifierName))
                {
                    name = ((CSS.IdentifierNameSyntax)vbType0).Identifier.Text;
                }
                else if (vbType0.IsKind(CS.SyntaxKind.GenericName))
                {
                    name = ((CSS.GenericNameSyntax)vbType0).Identifier.Text;
                }

                bool firstIsInterface = interfaceIsStartWithI && name.StartsWith("I");

                if (!firstIsInterface)
                {
                    retval.Inherits = vbInherits.Add(CS.SyntaxFactory.InheritsStatement(vbInheritTypes.Types[0]));
                }

                int start = firstIsInterface ? 0 : 1;
                foreach (CSS.TypeSyntax vbType in vbInheritTypes.Types.Skip(start))
                {
                    var i = CS.SyntaxFactory.ImplementsStatement(vbType);
                    vbImplements = vbImplements.Add(i);
                }
                retval.Implements = vbImplements;
            }

            return retval;
        }

        class SplitInheritResult
        {
            public SyntaxList<CSS.InheritsStatementSyntax> Inherits = new SyntaxList<CSS.InheritsStatementSyntax>();
            public SyntaxList<CSS.ImplementsStatementSyntax> Implements = new SyntaxList<CSS.ImplementsStatementSyntax>();
        }

        static MergeElseIfResult MergeElseIf(CSS.ElseBlockSyntax vbElseBlock, MergeElseIfResult retval = null)
        {
            if (retval == null)
            {
                retval = new MergeElseIfResult();
            }
            retval.ElseBlock = vbElseBlock;
            if (vbElseBlock != null && vbElseBlock.Statements != null && vbElseBlock.Statements.Count > 0)
            {
                SyntaxList<CSS.StatementSyntax> vbStatements = vbElseBlock.Statements;
                var vbStatement = vbStatements[0];
                if (vbStatement.IsKind(CS.SyntaxKind.MultiLineIfBlock))
                {
                    var vbIf = (CSS.MultiLineIfBlockSyntax)vbStatement;
                    var vbElseIfStatement = CS.SyntaxFactory.ElseIfStatement(vbIf.IfStatement.Condition);
                    var vbElseIfBlock = CS.SyntaxFactory.ElseIfBlock(vbElseIfStatement).WithStatements(vbIf.Statements);
                    retval.ElseIfBlocks.Add(vbElseIfBlock);

                    for (int i = 0; i < vbIf.ElseIfBlocks.Count; i++)
                    {
                        retval.ElseIfBlocks.Add(vbIf.ElseIfBlocks[i]);
                    }

                    retval = MergeElseIf(vbIf.ElseBlock, retval);
                }
                else
                {
                    retval.ElseBlock = vbElseBlock;
                }
            }
            return retval;
        }

        class MergeElseIfResult
        {
            public List<CSS.ElseIfBlockSyntax> ElseIfBlocks = new List<CSS.ElseIfBlockSyntax>();
            public CSS.ElseBlockSyntax ElseBlock;
        }


        static CSS.WithBlockSyntax CreateWithNothing()
        {
            var vbNothing = CS.SyntaxFactory.NothingLiteralExpression(CS.SyntaxFactory.Token(CS.SyntaxKind.NothingKeyword));
            return CS.SyntaxFactory.WithBlock(CS.SyntaxFactory.WithStatement(vbNothing));
        }

        static SyntaxToken GetDefaultModifiers(this VB.VisualBasicSyntaxNode node)
        {
            //SyntaxToken vbDefaultModifire;
            if (node.Parent == null || node.Parent.IsKind(VB.SyntaxKind.CompilationUnit) || node.Parent.IsKind(VB.SyntaxKind.NamespaceDeclaration))
            {
                return CS.SyntaxFactory.Token(CS.SyntaxKind.FriendKeyword);
            }
            else
            {
                return CS.SyntaxFactory.Token(CS.SyntaxKind.PrivateKeyword);
            }
        }


        static bool HasKindChildNode(this SyntaxNode startNode, CS.SyntaxKind kind)
        {
            foreach (SyntaxNode node in startNode.ChildNodes())
            {
                if (node.IsKind(kind))
                {
                    return true;
                }
                if (HasKindChildNode(node, kind))
                {
                    return true;
                }
            }
            return false;
        }

        #endregion
    }
}
