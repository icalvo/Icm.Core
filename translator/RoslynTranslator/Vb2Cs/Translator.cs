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

using System.Linq;
using VB = Microsoft.CodeAnalysis.VisualBasic;
using VBS = Microsoft.CodeAnalysis.VisualBasic.Syntax;
using CS = Microsoft.CodeAnalysis.CSharp;
using CSS = Microsoft.CodeAnalysis.CSharp.Syntax;

namespace Gekka.Roslyn.Translator.Vb2Cs
{
    public static partial class Translator
    {
        public static CS.CSharpSyntaxNode Translate(string sourceCode)
        {
            var csTree = VB.VisualBasicSyntaxTree.ParseText(sourceCode);
            var csRoot = (VB.VisualBasicSyntaxNode)csTree.GetRoot();

            return Convert(csRoot);
        }


        public static CS.CSharpSyntaxNode Convert(this VB.VisualBasicSyntaxNode node)
        {
            if (node == null)
            {
                return null;
            }

            switch (node.Kind())
            {
            case VB.SyntaxKind.CompilationUnit:
                return ((VBS.CompilationUnitSyntax)node).ConvertCompilationUnit();

            case VB.SyntaxKind.ImportsStatement:
                return ((VBS.ImportsStatementSyntax)node).ConvertUsingDirective();

            #region Block
            case VB.SyntaxKind.NamespaceDeclaration:
                return ((VBS.NamespaceDeclarationSyntax)node).ConvertNamespaceDeclaration();

            case VB.SyntaxKind.ClassDeclaration:
            case VB.SyntaxKind.InterfaceDeclaration:
            case VB.SyntaxKind.StructDeclaration:
                return ((VBS.BaseTypeDeclarationSyntax)node).ConvertTypeDeclaration();

            case VB.SyntaxKind.EnumDeclaration:
                return ((VBS.EnumDeclarationSyntax)node).ConvertEnumDeclaration();

            case VB.SyntaxKind.MethodDeclaration:
                return ((VBS.MethodDeclarationSyntax)node).ConvertMethodDeclaration();

            case VB.SyntaxKind.ConstructorDeclaration:
                return ((VBS.ConstructorDeclarationSyntax)node).ConvertConstructor();

            case VB.SyntaxKind.BaseConstructorInitializer:
            case VB.SyntaxKind.ThisConstructorInitializer:
                return ((VBS.ConstructorInitializerSyntax)node).ConvertConstructorInitializer();

            case VB.SyntaxKind.DestructorDeclaration:
                return ((VBS.DestructorDeclarationSyntax)node).ConvertDestructor();

            case VB.SyntaxKind.Block:
                return ConvertBlock((VBS.BlockSyntax)node);

            #region Field, Property
            case VB.SyntaxKind.EventFieldDeclaration:
                return ((VBS.EventFieldDeclarationSyntax)node).ConvertEventFieldDeclaration();

            case VB.SyntaxKind.EventDeclaration:
                return ((VBS.EventDeclarationSyntax)node).ConvertEventBlock();

            case VB.SyntaxKind.AddAccessorDeclaration:
            case VB.SyntaxKind.RemoveAccessorDeclaration:
                return ConvertEventAccessor((VBS.AccessorDeclarationSyntax)node);

            case VB.SyntaxKind.IndexerDeclaration:
                return ((VBS.IndexerDeclarationSyntax)node).ConvertIndexer();
            case VB.SyntaxKind.PropertyDeclaration:
                return ((VBS.PropertyDeclarationSyntax)node).ConvertProperty();
            case VB.SyntaxKind.GetAccessorDeclaration:
            case VB.SyntaxKind.SetAccessorDeclaration:
                return ((VBS.AccessorDeclarationSyntax)node).ConvertGetSet();
            case VB.SyntaxKind.AccessorList:
                //Need call ConvertAccessorList
                return null;

            case VB.SyntaxKind.FieldDeclaration:
                return ConvertFieldDeclaration((VBS.FieldDeclarationSyntax)node);

            case VB.SyntaxKind.VariableDeclaration:
                return ConvertVariableDeclaration((VBS.VariableDeclarationSyntax)node);

            case VB.SyntaxKind.VariableDeclarator:
                return ConvertVariableDeclarator((VBS.VariableDeclaratorSyntax)node);

            case VB.SyntaxKind.LocalDeclarationStatement:
                return ((VBS.LocalDeclarationStatementSyntax)node).ConvertLocalDeclaration();

            case VB.SyntaxKind.EnumMemberDeclaration:
                return ((VBS.EnumMemberDeclarationSyntax)node).ConvertEnumMemberDeclaration();

            case VB.SyntaxKind.DelegateDeclaration:
                return ((VBS.DelegateDeclarationSyntax)node).ConvertDelegateDeclaration();

            case VB.SyntaxKind.OperatorDeclaration:
                return ((VBS.OperatorDeclarationSyntax)node).ConvertOperator();


            #endregion

            #endregion

            #region Flow
            case VB.SyntaxKind.IfStatement:
                return ((VBS.IfStatementSyntax)node).ConvertIfStatement();

            case VB.SyntaxKind.ElseClause:
                return ((VBS.ElseClauseSyntax)node).ConvertElseClauseStatement();

            case VB.SyntaxKind.SwitchStatement:
                return ((VBS.SwitchStatementSyntax)node).ConvertSwitchStatementAndRewriteGoto();

            case VB.SyntaxKind.SwitchSection:
                return ((VBS.SwitchSectionSyntax)node).ConvertSwitchSelection();

            case VB.SyntaxKind.CaseSwitchLabel:
                return ((VBS.CaseSwitchLabelSyntax)node).ConvertCaseSwitchLabel();

            case VB.SyntaxKind.DefaultSwitchLabel:
                return CS.SyntaxFactory.ElseCaseClause();

            case VB.SyntaxKind.GotoCaseStatement:
            case VB.SyntaxKind.GotoDefaultStatement:
                    //Need call ConvertSwitchStatementAndRewriteGoto
                    return null;

            case VB.SyntaxKind.GotoStatement:
                    return ((VBS.GotoStatementSyntax)node).ConvertGoto();

            case VB.SyntaxKind.LabeledStatement:
                    return ((VBS.LabeledStatementSyntax)node).ConvertLabel();

            case VB.SyntaxKind.BreakStatement:
                return ((VBS.BreakStatementSyntax)node).ConvertBreak();

            case VB.SyntaxKind.ContinueStatement:
                return ((VBS.ContinueStatementSyntax)node).ConvertContinue();

            case VB.SyntaxKind.ForStatement:
                return ((VBS.ForStatementSyntax)node).ConvertFor();

            case VB.SyntaxKind.ForEachStatement:
                return ((VBS.ForEachStatementSyntax)node).ConvertForEach();

            case VB.SyntaxKind.DoStatement:
                return ((VBS.DoStatementSyntax)node).ConvertDo();

            case VB.SyntaxKind.WhileStatement:
                return ((VBS.WhileStatementSyntax)node).ConvertWhile();

            case VB.SyntaxKind.ReturnStatement:
                return ((VBS.ReturnStatementSyntax)node).ConvertReturn();

            case VB.SyntaxKind.YieldBreakStatement:
                return CS.SyntaxFactory.ExitFunctionStatement();

            case VB.SyntaxKind.YieldReturnStatement:
                return ((VBS.YieldStatementSyntax)node).ConvertYield();

          
            #endregion

            #region  try-catch-finally
            case VB.SyntaxKind.ThrowStatement:
                return ((VBS.ThrowStatementSyntax)node).ConvertThrow();

            case VB.SyntaxKind.TryStatement:
                return ((VBS.TryStatementSyntax)node).ConvertTry();

            case VB.SyntaxKind.CatchClause:
                return ((VBS.CatchClauseSyntax)node).ConvertCatchClause();

            case VB.SyntaxKind.CatchDeclaration:
                return ((VBS.CatchDeclarationSyntax)node).ConvertCatchDeclaration();

            case VB.SyntaxKind.CatchFilterClause:
                return ((VBS.CatchFilterClauseSyntax)node).ConvertCatchFilter();

            case VB.SyntaxKind.FinallyClause:
                return ((VBS.FinallyClauseSyntax)node).ConvertFinally();

                #endregion

                #region Attribute

                case VB.SyntaxKind.AttributesStatement:
                    var vbAttributeStatement = (VBS.AttributesStatementSyntax) node;
                    var csAttributes = vbAttributeStatement.AttributeLists


            case VB.SyntaxKind.AttributeList:
                {
                    var csAttributeList = (VBS.AttributeListSyntax)node;
                    var vbAttributes = csAttributeList.Attributes.ConvertSyntaxNodes<CSS.AttributeSyntax>().ToArray();

                    var vbTarget = (CSS.AttributeTargetSyntax)csAttributeList.Target.Convert();

                    if (vbAttributes != null && vbTarget != null)
                    {
                        for (int i = 0; i < vbAttributes.Length; i++)
                        {
                            vbAttributes[i] = vbAttributes[i].WithTarget(vbTarget);
                        }
                    }
                    return CS.SyntaxFactory.AttributeList().AddAttributes(vbAttributes);
                }
            case VB.SyntaxKind.AttributeTargetSpecifier:
                {
                    // https://msdn.microsoft.com/en-us/library/z0w1kczw.aspx
                    var csTarget = (VBS.AttributeTargetSpecifierSyntax)node;
                    var targetText = csTarget.Identifier.ValueText;

                    switch (targetText)
                    {
                    case "assembly":
                        return CS.SyntaxFactory.AttributeTarget(CS.SyntaxFactory.Token(CS.SyntaxKind.AssemblyKeyword));
                    case "module":
                        return CS.SyntaxFactory.AttributeTarget(CS.SyntaxFactory.Token(CS.SyntaxKind.ModuleKeyword));
                    case "field":
                        return CS.SyntaxFactory.AttributeTarget(CS.SyntaxFactory.Token(CS.SyntaxKind.FieldDeclaration));
                    case "event":
                    case "method":
                    case "param":
                    case "property":
                    case "return":
                    case "Type":
                        break;
                    }
                    return null;
                }
            case VB.SyntaxKind.Attribute:
                {
                    var csAttribute = (VBS.AttributeSyntax)node;
                    var vbName = (CSS.NameSyntax)csAttribute.Name.Convert();
                    var vbArgs = (CSS.ArgumentListSyntax)csAttribute.ArgumentList.Convert();
                    var vbAttribute = CS.SyntaxFactory.Attribute(vbName);
                    vbAttribute = vbAttribute.WithArgumentList(vbArgs);
                    return vbAttribute;
                }
            case VB.SyntaxKind.AttributeArgumentList:
                {
                    var csArguments = (VBS.AttributeArgumentListSyntax)node;
                    var vbArgs = csArguments.Arguments.Convert<VBS.AttributeArgumentSyntax, CSS.ArgumentSyntax>().ToArray();
                    return CS.SyntaxFactory.ArgumentList().AddArguments(vbArgs);

                }
            case VB.SyntaxKind.AttributeArgument:
                {
                    var csAttributeArgument = (VBS.AttributeArgumentSyntax)node;
                    var vbExpression = (CSS.ExpressionSyntax)csAttributeArgument.Expression.ConvertAssignRightExpression();
                    var vbNameEQ = (CSS.NameColonEqualsSyntax)csAttributeArgument.NameEquals.Convert();

                    var vbNameColon = (CSS.NameColonEqualsSyntax)csAttributeArgument.NameColon.Convert();
                    if (vbNameColon != null)
                    {
                    }
                    return CS.SyntaxFactory.SimpleArgument(vbExpression).WithNameColonEquals(vbNameEQ);
                }


            case VB.SyntaxKind.NameColon:
                {
                    var csNameColon = (VBS.NameColonSyntax)node;
                    var vbName = (CSS.IdentifierNameSyntax)csNameColon.Name.Convert();
                    return CS.SyntaxFactory.NameColonEquals(vbName);
                }
            #endregion

            #region Type
            case VB.SyntaxKind.TypeParameterList://genericの型
                return ((VBS.TypeParameterListSyntax)node).ConvertTypeParameterList();

            case VB.SyntaxKind.TypeParameter:
                return ((VBS.TypeParameterSyntax)node).ConvertTypeParameter();

            case VB.SyntaxKind.BaseList://継承の型一覧
                return ((VBS.BaseListSyntax)node).ConvertBaseList();

            case VB.SyntaxKind.SimpleBaseType://単純な型
                return ((VBS.SimpleBaseTypeSyntax)node).ConvertSimpleBaseType();

            case VB.SyntaxKind.TypeArgumentList://ジェネリックの型引数
                return ((VBS.TypeArgumentListSyntax)node).ConvertTypeArgumentList();

            case VB.SyntaxKind.PredefinedType://標準型？
                return ((VBS.PredefinedTypeSyntax)node).ConvertPredefinedTypeType();

            case VB.SyntaxKind.ArrayType://配列
                return ((VBS.ArrayTypeSyntax)node).ConvertArrayType();

            case VB.SyntaxKind.ArrayRankSpecifier://配列型の次元
                return ((VBS.ArrayRankSpecifierSyntax)node).ConvertArrayRank();

            case VB.SyntaxKind.NullableType://Null許容型
                return ((VBS.NullableTypeSyntax)node).ConvertNullableType();

            #endregion

            #region Parameter

            case VB.SyntaxKind.ParameterList://関数等の引数の一覧
                return ((VBS.ParameterListSyntax)node).ConvertParameterList();

            case VB.SyntaxKind.Parameter://関数等の引数
                return ConvertParameter((VBS.ParameterSyntax)node);

            case VB.SyntaxKind.TypeParameterConstraintClause:
                return ((VBS.TypeParameterConstraintClauseSyntax)node).ConvertTypeParameterConstraintClauseSyntax();

            case VB.SyntaxKind.ClassConstraint:
                return CS.SyntaxFactory.ClassConstraint(CS.SyntaxFactory.Token(CS.SyntaxKind.ClassKeyword));

            case VB.SyntaxKind.StructConstraint:
                return CS.SyntaxFactory.StructureConstraint(CS.SyntaxFactory.Token(CS.SyntaxKind.StructureKeyword));

            case VB.SyntaxKind.ConstructorConstraint:
                return CS.SyntaxFactory.NewConstraint(CS.SyntaxFactory.Token(CS.SyntaxKind.NewKeyword));

            case VB.SyntaxKind.TypeConstraint:
                {
                    var csTypeParameterConsraint = (VBS.TypeParameterConstraintSyntax)node;
                    var csTypeConstraint = (VBS.TypeConstraintSyntax)node;
                    var vbType = (CSS.TypeSyntax)csTypeConstraint.Type.Convert();
                    return CS.SyntaxFactory.TypeConstraint(vbType);
                }
            #endregion

            #region Name

            case VB.SyntaxKind.IdentifierName://ユニークな名前
                return ((VBS.IdentifierNameSyntax)node).ConvertIdentifierName();

            case VB.SyntaxKind.GenericName://ジェネリック型の型引数を除いた名前
                return ((VBS.GenericNameSyntax)node).ConvertGenericName();

            case VB.SyntaxKind.QualifiedName://NameSpaceや型名付の名前
                return ((VBS.QualifiedNameSyntax)node).ConvertQualifiedName();

            case VB.SyntaxKind.AliasQualifiedName:
                return ((VBS.AliasQualifiedNameSyntax)node).ConvertAliasQualifiedName();

            #endregion

            #region Expression
            case VB.SyntaxKind.ThisExpression:
                return CS.SyntaxFactory.MeExpression();

            case VB.SyntaxKind.ExpressionStatement:
                return ((VBS.ExpressionStatementSyntax)node).ConvertExpressionStatement();
            
            case VB.SyntaxKind.DefaultExpression: //default(T)
                return ((VBS.DefaultExpressionSyntax)node).ConvertDefaultExpression();

            case VB.SyntaxKind.CastExpression:
                return ((VBS.CastExpressionSyntax)node).ConvertCast(); 

            case VB.SyntaxKind.AsExpression:
                return ((VBS.BinaryExpressionSyntax)node).ConvertAs();

            case VB.SyntaxKind.SimpleAssignmentExpression:
                return ((VBS.AssignmentExpressionSyntax)node).ConvertSimpleAssignment();

            case VB.SyntaxKind.AddAssignmentExpression: // +=
            case VB.SyntaxKind.SubtractAssignmentExpression: // -=
            case VB.SyntaxKind.MultiplyAssignmentExpression: // *=
            case VB.SyntaxKind.DivideAssignmentExpression: // /=
            case VB.SyntaxKind.LeftShiftAssignmentExpression: // <<=
            case VB.SyntaxKind.RightShiftAssignmentExpression: // >>=
            case VB.SyntaxKind.ModuloAssignmentExpression: // Mod
            case VB.SyntaxKind.AndAssignmentExpression: // &=
            case VB.SyntaxKind.OrAssignmentExpression: // |=
            case VB.SyntaxKind.ExclusiveOrAssignmentExpression: // ^=
                return ConvertAssignmentExpression((VBS.AssignmentExpressionSyntax)node);

            case VB.SyntaxKind.NameEquals:
                return ((VBS.NameEqualsSyntax)node).ConvertNameEquals();

            case VB.SyntaxKind.EqualsValueClause:
                return ((VBS.EqualsValueClauseSyntax)node).ConvertEqualsValue();

            case VB.SyntaxKind.ObjectCreationExpression:
                return ((VBS.ObjectCreationExpressionSyntax)node).ConvertObjectCreation();

            case VB.SyntaxKind.ArrayInitializerExpression://int[] array ={1,2,3} 
                return ((VBS.InitializerExpressionSyntax)node).ConvertArrayInitializer();

            case VB.SyntaxKind.ArrayCreationExpression:
                return ((VBS.ArrayCreationExpressionSyntax)node).ConvertArrayCreation();

            case VB.SyntaxKind.ArgumentList://引数一覧
            case VB.SyntaxKind.BracketedArgumentList://accessing argument for array or indexer
                return ((VBS.BaseArgumentListSyntax)node).ConvertBaeArgumentList();

            case VB.SyntaxKind.BracketedParameterList:// [int a, int b]
                return ((VBS.BracketedParameterListSyntax)node).ConvertBracketedParameter();

            case VB.SyntaxKind.OmittedArraySizeExpression:
                return null;// VB.SyntaxFactory.OmittedArgument();

            case VB.SyntaxKind.Argument:
                return ((VBS.ArgumentSyntax)node).ConvertArgument();

            case VB.SyntaxKind.InvocationExpression:
                return ((VBS.InvocationExpressionSyntax)node).ConvertInvocationExpretion();

            case VB.SyntaxKind.SimpleMemberAccessExpression:
                return ((VBS.MemberAccessExpressionSyntax)node).ConvertMemberAccess();

            case VB.SyntaxKind.ElementAccessExpression:
                return ((VBS.ElementAccessExpressionSyntax)node).ConvertElementAccess();

            case VB.SyntaxKind.ParenthesizedExpression:
                return ((VBS.ParenthesizedExpressionSyntax)node).ConvertParenthesized();

            case VB.SyntaxKind.NullLiteralExpression://null
                return ConvertNullLiteral();

            case VB.SyntaxKind.TrueLiteralExpression:// true
                return ConvertTrueLiteral();

            case VB.SyntaxKind.FalseLiteralExpression:// false
                return ConvertFalseLiteral();

            case VB.SyntaxKind.UnaryPlusExpression:
                return ((VBS.PrefixUnaryExpressionSyntax)node).ConvertUnraryPlus();

            case VB.SyntaxKind.UnaryMinusExpression:
                return ((VBS.PrefixUnaryExpressionSyntax)node).ConvertUnraryMinus();

            case VB.SyntaxKind.NumericLiteralExpression:
                return ((VBS.LiteralExpressionSyntax)node).ConvertLiteral();
            case VB.SyntaxKind.CharacterLiteralExpression:
                return ((VBS.LiteralExpressionSyntax)node).ConvertLiteralChar();
            case VB.SyntaxKind.StringLiteralExpression:
                return ((VBS.LiteralExpressionSyntax)node).ConvertLiteralString();

            case VB.SyntaxKind.TypeOfExpression:
                return ((VBS.TypeOfExpressionSyntax)node).ConvertTypeOf();

            case VB.SyntaxKind.IsExpression: // Is
            case VB.SyntaxKind.AddExpression:// +
            case VB.SyntaxKind.SubtractExpression:// -
            case VB.SyntaxKind.MultiplyExpression:// *
            case VB.SyntaxKind.DivideExpression:// /
            case VB.SyntaxKind.ModuloExpression:// %
            case VB.SyntaxKind.LeftShiftExpression: // <<
            case VB.SyntaxKind.RightShiftExpression: // >>
            case VB.SyntaxKind.BitwiseAndExpression: //&
            case VB.SyntaxKind.BitwiseOrExpression:  //|
            case VB.SyntaxKind.ExclusiveOrExpression: // ^
            case VB.SyntaxKind.LogicalAndExpression: //&&
            case VB.SyntaxKind.LogicalOrExpression: //||
            case VB.SyntaxKind.EqualsExpression: // ==
            case VB.SyntaxKind.NotEqualsExpression: // !=
            case VB.SyntaxKind.GreaterThanExpression: // >
            case VB.SyntaxKind.GreaterThanOrEqualExpression: // >=
            case VB.SyntaxKind.LessThanExpression: // <
            case VB.SyntaxKind.LessThanOrEqualExpression: // <=
            case VB.SyntaxKind.CoalesceExpression: // ??
                return ConvertBinaryExpression((VBS.BinaryExpressionSyntax)node);

            case VB.SyntaxKind.BitwiseNotExpression: // !
            case VB.SyntaxKind.LogicalNotExpression: // ~
                return ((VBS.PrefixUnaryExpressionSyntax)node).ConvertNot();

            case VB.SyntaxKind.PreIncrementExpression: // ++A
                return ((VBS.PrefixUnaryExpressionSyntax)node).ConvertPreIncrement();
            case VB.SyntaxKind.PreDecrementExpression: // --A
                return ((VBS.PrefixUnaryExpressionSyntax)node).ConvertPreDecrement();
            case VB.SyntaxKind.PostIncrementExpression: // A++
                return ((VBS.PostfixUnaryExpressionSyntax)node).ConvertPostIncrement();
            case VB.SyntaxKind.PostDecrementExpression: // A--
                return ((VBS.PostfixUnaryExpressionSyntax)node).ConvertPostDecrement();

            case VB.SyntaxKind.ConditionalExpression: // ?:
                return ((VBS.ConditionalExpressionSyntax)node).ConvertConditional();

            #endregion

            case VB.SyntaxKind.ExplicitInterfaceSpecifier:
                return ((VBS.ExplicitInterfaceSpecifierSyntax)node).ConvertExplicitInterfaceSpecifier();

            case VB.SyntaxKind.UsingStatement:
                return ((VBS.UsingStatementSyntax)node).ConvertUsingBlock();

            case VB.SyntaxKind.LockStatement:
                return ((VBS.LockStatementSyntax)node).ConvertLock();

            case VB.SyntaxKind.EmptyStatement:
                return CS.SyntaxFactory.EmptyStatement();

            #region Lambda

            case VB.SyntaxKind.ConversionOperatorDeclaration:
                return ((VBS.ConversionOperatorDeclarationSyntax)node).ConvertConversionOperator();
              
            case VB.SyntaxKind.ParenthesizedLambdaExpression:
            case VB.SyntaxKind.SimpleLambdaExpression:
                return ((VBS.LambdaExpressionSyntax)node).ConvertLambdaExpression();

            #endregion

            case VB.SyntaxKind.InterpolatedStringExpression:
                if (true)
                {
                    //Convert to flat Interpolate 
                    return ((VBS.InterpolatedStringExpressionSyntax)node).ConvertInterpolatedStringExpression();
                }
                else
                {
                    //Convert to nested Interpolate
                    var nodex = (VBS.InterpolatedStringExpressionSyntax)node;
                    var vbContents = nodex.Contents.ConvertSyntaxNodes<CSS.InterpolatedStringContentSyntax>();
                    return CS.SyntaxFactory.InterpolatedStringExpression().AddContents(vbContents.ToArray());
                }
            case VB.SyntaxKind.InterpolationAlignmentClause:
                return ((VBS.InterpolationAlignmentClauseSyntax)node).ConvertInterpolationAlignmentClause();

            case VB.SyntaxKind.InterpolatedStringText:
                return ((VBS.InterpolatedStringTextSyntax)node).ConvertInterpolatedStringText();

            case VB.SyntaxKind.Interpolation:
                return ((VBS.InterpolationSyntax)node).ConvertInterpolation();

            case VB.SyntaxKind.InterpolationFormatClause:
                return ((VBS.InterpolationFormatClauseSyntax)node).ConvertInterpolationFormatClause();
            default:
                System.Diagnostics.Debug.WriteLine(node.Kind());
                break;

            }
            return null;
        }

        public static void ResetCounter()
        {
            ContinueForRewriter.ResetCounter();
            GotoCaseRewriter.ResetCounter();
        }
    }

}
