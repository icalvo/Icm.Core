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
using VB = Microsoft.CodeAnalysis.VisualBasic;
using CS = Microsoft.CodeAnalysis.CSharp;

namespace Gekka.Roslyn.Translator.Vb2Cs
{
    public static partial class Translator
    {
        static readonly KeywordPair[] ModifirePairs =
        {
            new KeywordPair(VB.SyntaxKind.PrivateKeyword, CS.SyntaxKind.PrivateKeyword),
            new KeywordPair(VB.SyntaxKind.ProtectedKeyword, CS.SyntaxKind.ProtectedKeyword),
            new KeywordPair(VB.SyntaxKind.FriendKeyword, CS.SyntaxKind.InternalKeyword),
            new KeywordPair(VB.SyntaxKind.PublicKeyword, CS.SyntaxKind.PublicKeyword),
            new KeywordPair(VB.SyntaxKind.SharedKeyword, CS.SyntaxKind.StaticKeyword),
            new KeywordPair(VB.SyntaxKind.OverridesKeyword, CS.SyntaxKind.OverrideKeyword),
            new KeywordPair(VB.SyntaxKind.OverridableKeyword, CS.SyntaxKind.VirtualKeyword),
            //new KeywordPair(CS.SyntaxKind.None, VB.SyntaxKind.OverloadsKeyword),
            new KeywordPair(VB.SyntaxKind.MustInheritKeyword, CS.SyntaxKind.AbstractKeyword, AttributeTargets.Class),
            new KeywordPair(VB.SyntaxKind.MustOverrideKeyword, CS.SyntaxKind.AbstractKeyword, AttributeTargets.All & ~AttributeTargets.Class),
            new KeywordPair(VB.SyntaxKind.NotInheritableKeyword, CS.SyntaxKind.SealedKeyword, AttributeTargets.Class),
            new KeywordPair(VB.SyntaxKind.NotOverridableKeyword, CS.SyntaxKind.SealedKeyword, AttributeTargets.All & ~AttributeTargets.Class),
            new KeywordPair(VB.SyntaxKind.ShadowsKeyword, CS.SyntaxKind.NewKeyword, AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Event),
            new KeywordPair(VB.SyntaxKind.ConstKeyword, CS.SyntaxKind.ConstKeyword, AttributeTargets.Field),
            new KeywordPair(VB.SyntaxKind.PartialKeyword, CS.SyntaxKind.PartialKeyword),

            new KeywordPair(VB.SyntaxKind.WideningKeyword, CS.SyntaxKind.ImplicitKeyword),
            new KeywordPair(VB.SyntaxKind.NarrowingKeyword, CS.SyntaxKind.ExplicitKeyword),

            new KeywordPair(VB.SyntaxKind.AsyncKeyword,CS.SyntaxKind.AsyncKeyword),
            new KeywordPair(VB.SyntaxKind.AwaitKeyword,CS.SyntaxKind.AwaitKeyword),

            new KeywordPair(VB.SyntaxKind.None, CS.SyntaxKind.VolatileKeyword, (AttributeTargets)0)

        };
        static KeywordPair[] TypePairs =
        {
            new KeywordPair(VB.SyntaxKind.NothingKeyword, CS.SyntaxKind.NullKeyword),
            new KeywordPair(VB.SyntaxKind.StringKeyword, CS.SyntaxKind.StringKeyword),
            new KeywordPair(VB.SyntaxKind.CharKeyword, CS.SyntaxKind.CharKeyword),
            new KeywordPair(VB.SyntaxKind.ObjectKeyword, CS.SyntaxKind.ObjectKeyword),

            new KeywordPair(VB.SyntaxKind.BooleanKeyword, CS.SyntaxKind.BoolKeyword),
            new KeywordPair(VB.SyntaxKind.ByteKeyword, CS.SyntaxKind.ByteKeyword),
            new KeywordPair(VB.SyntaxKind.SByteKeyword, CS.SyntaxKind.SByteKeyword),
            new KeywordPair(VB.SyntaxKind.ShortKeyword, CS.SyntaxKind.ShortKeyword),
            new KeywordPair(VB.SyntaxKind.UShortKeyword, CS.SyntaxKind.UShortKeyword),
            new KeywordPair(VB.SyntaxKind.IntegerKeyword, CS.SyntaxKind.IntKeyword),
            new KeywordPair(VB.SyntaxKind.UIntegerKeyword, CS.SyntaxKind.UIntKeyword),
            new KeywordPair(VB.SyntaxKind.LongKeyword, CS.SyntaxKind.LongKeyword),
            new KeywordPair(VB.SyntaxKind.ULongKeyword, CS.SyntaxKind.ULongKeyword),
            new KeywordPair(VB.SyntaxKind.SingleKeyword, CS.SyntaxKind.FloatKeyword),
            new KeywordPair(VB.SyntaxKind.DoubleKeyword, CS.SyntaxKind.DoubleKeyword),
            new KeywordPair(VB.SyntaxKind.DecimalKeyword, CS.SyntaxKind.DecimalKeyword),           
        };


        static BreakPair[] BreakPairs =
        {
            new BreakPair(VB.SyntaxKind.SelectBlock, CS.SyntaxKind.SwitchStatement, CS.SyntaxKind.CloseBraceToken),
            new BreakPair(VB.SyntaxKind.ForStatement, CS.SyntaxKind.ForStatement, CS.SyntaxKind.ForKeyword),
            new BreakPair(VB.SyntaxKind.ForEachStatement, CS.SyntaxKind.ForEachStatement, CS.SyntaxKind.ForKeyword),
            new BreakPair(VB.SyntaxKind.DoStatement, CS.SyntaxKind.DoStatement, CS.SyntaxKind.DoKeyword),
            new BreakPair(VB.SyntaxKind.WhileStatement, CS.SyntaxKind.ExitWhileStatement, CS.SyntaxKind.WhileKeyword),

            //Blocking
            new BreakPair(VB.SyntaxKind.MethodDeclaration, CS.SyntaxKind.None, CS.SyntaxKind.None),
        };
        static BreakPair[] ContinuePairs =
        {
            //new BreakPair (CS.SyntaxKind.SwitchStatement,VB.SyntaxKind.ExitSelectStatement, VB.SyntaxKind.SelectKeyword),
            new BreakPair (VB.SyntaxKind.ForStatement,CS.SyntaxKind.ContinueForStatement, CS.SyntaxKind.ForKeyword),
            new BreakPair (VB.SyntaxKind.ForEachStatement,CS.SyntaxKind.ContinueForStatement, CS.SyntaxKind.ForKeyword),
            new BreakPair (VB.SyntaxKind.DoStatement,CS.SyntaxKind.ContinueDoStatement, CS.SyntaxKind.DoKeyword),
            new BreakPair (VB.SyntaxKind.WhileStatement,CS.SyntaxKind.ContinueWhileStatement, CS.SyntaxKind.WhileKeyword),

            //Blocking
            new BreakPair(VB.SyntaxKind.MethodDeclaration, CS.SyntaxKind.None, CS.SyntaxKind.None),
        };



        static string[,] ControlCharPairs =
                {
                    {"\0","vbNullChar"},
                    {"\t","vbTab"},
                    {"\b","vbBack"},
                    {"\n","vblf"},
                    {"\v","vbVerticalTab"},
                    {"\f","vbFormFeed"},
                    {"\r","vbCr"},
                };

        static KeywordPair[] OperatorPairs =
        {
            /* +  */ new KeywordPair(VB.SyntaxKind.PlusToken, CS.SyntaxKind.PlusToken),
            /* -  */ new KeywordPair(VB.SyntaxKind.MinusToken, CS.SyntaxKind.MinusToken),
            /* *  */ new KeywordPair(VB.SyntaxKind.AsteriskToken, CS.SyntaxKind.AsteriskToken),
            /* /  */ new KeywordPair(VB.SyntaxKind.SlashToken, CS.SyntaxKind.SlashToken),
            /* %  */ new KeywordPair(VB.SyntaxKind.ModKeyword, CS.SyntaxKind.PercentToken),
            /* ~  */ new KeywordPair(VB.SyntaxKind.NotKeyword, CS.SyntaxKind.TildeToken),

            /* << */ new KeywordPair(VB.SyntaxKind.LessThanLessThanToken, CS.SyntaxKind.LessThanLessThanToken),
            /* >> */ new KeywordPair(VB.SyntaxKind.GreaterThanGreaterThanToken, CS.SyntaxKind.GreaterThanGreaterThanToken),

            /* !  */ new KeywordPair(VB.SyntaxKind.NotKeyword, CS.SyntaxKind.ExclamationToken),
            /* &  */ new KeywordPair(VB.SyntaxKind.AmpersandToken, CS.SyntaxKind.AmpersandToken),
            /* |  */ new KeywordPair(VB.SyntaxKind.OrKeyword, CS.SyntaxKind.BarToken),
            /* ^  */ new KeywordPair(VB.SyntaxKind.XorKeyword, CS.SyntaxKind.CaretToken),

            /* == */ new KeywordPair(VB.SyntaxKind.EqualsToken, CS.SyntaxKind.EqualsEqualsToken),
            /* != */ new KeywordPair(VB.SyntaxKind.LessThanGreaterThanToken, CS.SyntaxKind.ExclamationEqualsToken),

            /* <  */ new KeywordPair(VB.SyntaxKind.LessThanToken,CS.SyntaxKind.LessThanToken),
            /* <= */ new KeywordPair(VB.SyntaxKind.LessThanEqualsToken ,CS.SyntaxKind.LessThanEqualsToken ),
            /* > */ new KeywordPair(VB.SyntaxKind.GreaterThanToken,CS.SyntaxKind.GreaterThanToken),
            /* >= */ new KeywordPair(VB.SyntaxKind.GreaterThanEqualsToken ,CS.SyntaxKind.GreaterThanEqualsToken),
        };

        static bool IsVBKeyword(string id)
        {
            if (_vbKeywordStrings == null)
            {
                _vbKeywordStrings = new List<string>();
                foreach (CS.SyntaxKind kind in CS.SyntaxFacts.GetKeywordKinds())
                {
                    string s = CS.SyntaxFactory.Token(kind).ToString().ToLower();
                    if (!_vbKeywordStrings.Contains(s))
                    {
                        _vbKeywordStrings.Add(s);
                    }
                }
                if (!_vbKeywordStrings.Contains("finalize"))
                {
                    _vbKeywordStrings.Add("finalize");
                }
            }

            return _vbKeywordStrings.Contains(id.ToLower());
        }

        private static List<string> _vbKeywordStrings;

        private class KeywordPair
        {
            public readonly VB.SyntaxKind Cs;
            public readonly CS.SyntaxKind Vb;

            public readonly AttributeTargets Targets;

            public KeywordPair(VB.SyntaxKind cs, CS.SyntaxKind vb, AttributeTargets target = AttributeTargets.All)
            {
                Cs = cs;
                Vb = vb;
                Targets = target;
            }
        }

        class BreakPair : KeywordPair
        {
            public readonly CS.SyntaxKind ExitTargetKeyword;

            public BreakPair(VB.SyntaxKind cs, CS.SyntaxKind vb, CS.SyntaxKind exitTargetKeyword, AttributeTargets target = AttributeTargets.All)
                : base(cs, vb, target)
            {
                ExitTargetKeyword = exitTargetKeyword;
            }
        }
    }
}
